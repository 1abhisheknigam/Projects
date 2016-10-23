using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Abhishek Nigam
namespace ProjectGemini
{
    public class CPU
    {
        /// <summary>
        /// CPU class handles all execution of instrucitons,
        /// and maintainance of registers.
        /// </summary>
        /// 
        public string source;
        public IPE IPE { get; set; }
        private Memory Memory;

        //Registers
        public int _ZERO = 0;
        public int _ONE = 1;
        public int _a { get; set; }
        public int _b { get; set; }
        public int _acc { get; set; }
        public int _pc { get; set; }
        public int _mar { get; set; }
        public int _mdr { get; set; }
        public int _temp { get; set; }
        public String _ir { get; set; }//Changed for now.
        public int _cc { get; set; }

        //Strings that help the GUI
        public string Err { get; set; }
        public string currentInstruction { get; set; }

        //Trackers

        int additions;
        int subtractions;
        int nots;
        int ands;
        int ors;
        int stores;
        int moves;
        int total;

        //Private variable to help progression.
        private bool branchFlag;

        //Constructor
        public CPU()
        {
            Memory = new Memory();
            IPE = new IPE(Memory);
            resetTracks();
            _pc = 0;
            _cc = 0;
            Err = "Instruction Complete.";
        }

        /// <summary>
        /// Restarts the CPU state of the program to restart using the current 
        /// instructions.
        /// </summary>
        public void resetAll()
        {
            Memory.stack = new int[256];
            _pc = 0;
            _cc = 0;
            resetTracks();

            _a = 0;
            _b = 0;
            _ir = "";
            _acc = 0;
            _mar = 0;
            _mdr = 0;
            _temp = 0;

            currentInstruction = "";
            Err = "User Reset. Start at Line 1.";
        }

        /// <summary>
        /// Updates _cc register depending on the _acc.
        /// </summary>
        public void updateCC()
        {
            if (_acc == 0) _cc = 0;
            else if (_acc > 0) _cc = 1;
            else _cc = -1;
        }

        /// <summary>
        /// Helper function that resets all
        /// operation trackers.
        /// </summary>
        public void resetTracks()
        {
            subtractions = 0;
            additions = 0;
            stores = 0;
            total = 0;
            moves = 0;
            nots = 0;
            ands = 0;
            ors = 0;
        }

        /// <summary>
        /// Executes the next command in the Instructions.
        /// 
        /// Returns true if executed suffessfully.
        /// Returns false if error or there are no more instructions.
        /// </summary>
        /// <returns></returns>
        public Boolean executeNext()
        {
            try
            {
                if (_pc < Memory.Instructions.Count)
                {
                    currentInstruction = Memory.Instructions[_pc]; // From Marlin's List.
                    _ir = currentInstruction;
                    branchFlag = false;
                    execute(currentInstruction);
                    Err = "Instuction Complete";
                }
                else { Err = "Run Complete"; return false; }
                return true;
            }
            catch (IndexOutOfRangeException e) { Err = "Segmentation Fault at Line " + _pc.ToString(); }
            catch (Exception e) { Err = e.Message + "Line " + _pc.ToString(); }
            return false;
        }

        /// <summary>
        /// The method that actually parses and executes a single command
        /// by deciding which method to call.
        /// </summary>
        /// <param name="line"></param>
        public void execute(String line)
        {
            if (line.Equals("nota")) { not(); updateCC(); nots++; }
            else if (line.Equals("nop")) { nop(); additions++; }
            else
            {
                String[] words = line.Split(' ');
                switch (words[0])
                {
                    case "lda":
                        {
                            if (words[1][0] == '$') loadAccMem(Convert.ToInt32(words[1].Substring(1)));
                            else loadAcc(Convert.ToInt32(words[1].Substring(2)));
                            moves++;
                            break;
                        }
                    case "sta":
                        {
                            storeAcc(Convert.ToInt32(words[1].Substring(1)));
                            stores++;
                            break;
                        }
                    case "add":
                        {
                            if (words[1][0] == '$') addMem(Convert.ToInt32(words[1].Substring(1)));
                            else add(Convert.ToInt32(words[1].Substring(2)));
                            updateCC();
                            additions++;
                            break;
                        }
                    case "sub":
                        {
                            if (words[1][0] == '$') subMem(Convert.ToInt32(words[1].Substring(1)));
                            else sub(Convert.ToInt32(words[1].Substring(2)));
                            updateCC();
                            subtractions++;
                            break;
                        }
                    case "and":
                        {
                            if (words[1][0] == '$') andAccMem(Convert.ToInt32(words[1].Substring(1)));
                            else andAcc(Convert.ToInt32(words[1].Substring(2)));
                            updateCC();
                            ands++;
                            break;
                        }
                    case "or":
                        {
                            if (words[1][0] == '$') orAccMem(Convert.ToInt32(words[1].Substring(1)));
                            else orAcc(Convert.ToInt32(words[1].Substring(2)));
                            updateCC();
                            ors++;
                            break;
                        }
                    case "ba":
                        {
                            branch(Convert.ToInt32(words[1]));
                            branchFlag = true;
                            break;
                        }
                    case "bg":
                        {
                            branchGreater(Convert.ToInt32(words[1]));
                            break;
                        }
                    case "bl":
                        {
                            branchLess(Convert.ToInt32(words[1]));
                            break;
                        }
                    case "be":
                        {
                            branchEqual(Convert.ToInt32(words[1]));
                            break;
                        }
                }
            }
            if (!branchFlag) { _pc++; total++; }
        }


        #region load
        /// <summary>
        /// methods for Load. One for memory
        /// and one for constant.
        /// </summary>
        /// <param name="i"></param>
        public void loadAccMem(int i)
        {
            _acc = Memory.stack[i];
        }
        public void loadAcc(int val)
        {
            _acc = val;
        }
        #endregion
        #region store
        /// <summary>
        /// Store command.
        /// </summary>
        /// <param name="i"></param>
        public void storeAcc(int i)
        {
            Memory.stack[i] = _acc;
        }
        #endregion
        #region add
        /// <summary>
        /// Add methods, one for memory
        /// address, one for constant.
        /// </summary>
        /// <param name="i"></param>
        public void addMem(int i)
        {
            _acc += Memory.stack[i];
        }

        public void add(int val)
        {
            _acc += val;
        }

        public void nop()
        {
            _acc += _ZERO;
        }


        #endregion
        #region subtract
        /// <summary>
        /// Subtract methods, one for memory, 
        /// one for constant.
        /// </summary>
        /// <param name="i"></param>
        public void subMem(int i)
        {
            _acc -= Memory.stack[i];
        }

        public void sub(int val)
        {
            _acc -= val;
        }
        #endregion
        #region and
        /// <summary>
        /// And commands, one for memory address,
        /// one for constant.
        /// </summary>
        /// <param name="i"></param>
        public void andAccMem(int i)
        {
            _acc &= Memory.stack[i];
        }
        public void andAcc(int val)
        {
            _acc &= val;
        }
        #endregion
        #region or
        /// <summary>
        /// Or methods, one for memory address,
        /// one for constant.
        /// </summary>
        /// <param name="i"></param>
        public void orAccMem(int i)
        {
            _acc |= Memory.stack[i];
        }
        public void orAcc(int val)
        {
            _acc |= val;
        }
        #endregion
        #region not
        /// <summary>
        /// Not command. Directly works with _acc.
        /// </summary>
        public void not()
        {
            _acc = -_acc;
        }
        #endregion
        #region branch
        /// <summary>
        /// Branch commands that change the _pc
        /// depending on the _cc.
        /// </summary>
        /// <param name="l"></param>
        public void branch(int l)
        {
            _pc = l;
        }
        public void branchGreater(int l)
        {
            if (_cc > 0) { _pc = l; branchFlag = true; }
        }
        public void branchLess(int l)
        {
            if (_cc < 0) { _pc = l; branchFlag = true; }
        }
        public void branchEqual(int l)
        {
            if (_cc == 0) { _pc = l; branchFlag = true; }
        }
        #endregion

        #region GUI Helpers
        /// <summary>
        /// Helper functions that the GUI can call 
        /// to update itself for the user.
        /// </summary>
        /// <returns></returns>
        public String getInstructionIndex()
        {
            return _pc.ToString() + "/" + Memory.Instructions.Count;
        }

        public int getInstructionPercentageInt()
        {
            if (Memory.Instructions.Count == 0) return 0;
            return (int)((float)_pc / Memory.Instructions.Count * 100.0);
        }
        public String getInstructionPercentage()
        {
            return getInstructionPercentageInt().ToString() + "%";
        }

        public String getStackElementAt(int i)
        {
            try { return Memory.stack[i].ToString(); }
            catch (IndexOutOfRangeException e) { return "Invalid Index"; }
        }
        #endregion
    }
}
