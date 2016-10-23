using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Abhishek Nigam
namespace ProjectGemini
{
    public class Memory
    {
        /// <summary>
        /// Memory Class which contains the sections
        /// that store all data. Instructions store 
        /// the line by line instructions after parsing.
        /// Stack is for the CPU and run time memory.
        /// Labels is to help resolve labels in the 
        /// program.
        /// </summary>
        public int[] stack { get; set; }
        public List<string> Instructions{ get; set;}
        public Dictionary<string, int> Labels { get; set; }
        
        public Memory()
        {
            stack = new int[256];
            Instructions = new List<string>();
            Labels = new Dictionary<string, int>();

            //For Debugging.
            #region HardCoded Tests     
            #region test1

            //Instructions.Add("lda #$1");
            //Instructions.Add("sta $0");
            //Instructions.Add("sta $255");
            //Instructions.Add("add $0");
            //Instructions.Add("lda $0");
            //Instructions.Add("sub $0");
            //Instructions.Add("lda $0");
            //Instructions.Add("and $255");
            //Instructions.Add("lda #$0");
            //Instructions.Add("or $0");
            //Instructions.Add("lda #$0");
            //Instructions.Add("nota");
            //Instructions.Add("nop");
            
            #endregion
            #region test2

            //Instructions.Add("lda #$5");
            //Instructions.Add("sta $2");
            //Instructions.Add("lda #$0");
            //Instructions.Add("sta $0");
            //Instructions.Add("lda #$10");
            //Instructions.Add("sta $1");

            //Instructions.Add("lda $1");
            //Instructions.Add("sub $0");
            //Instructions.Add("bl 16");

            //Instructions.Add("lda $0");
            //Instructions.Add("add #$1");
            //Instructions.Add("sta $0");

            //Instructions.Add("lda $2");
            //Instructions.Add("add #$2");
            //Instructions.Add("and $0");
            //Instructions.Add("ba 6");

            //Instructions.Add("lda $2");
            #endregion
            #region test4
            //Instructions.Add("lda #$32");
            //Instructions.Add("sta $392");
            #endregion
            #region test5
            //Instructions.Add("lda #$0");
            //Instructions.Add("sta $2");
            //Instructions.Add("lda #$4");
            //Instructions.Add("sta $3");
            //Instructions.Add("lda #$1");
            //Instructions.Add("sta $4");
            ////loop
            //Instructions.Add("lda $2");
            //Instructions.Add("sub #$13");   //if(x - 13 > 0) exit loop.     
            //Instructions.Add("bg 16");

            //Instructions.Add("lda $3");
            //Instructions.Add("add $2");
            //Instructions.Add("sub $4");
            //Instructions.Add("sta $3");

            //Instructions.Add("sub #$9");
            //Instructions.Add("be 16");
            //Instructions.Add("ba 20");
            ////rehash 16
            //Instructions.Add("lda $2");
            //Instructions.Add("and $4");
            //Instructions.Add("or $3");
            //Instructions.Add("sta $3");
            ////out 20
            //Instructions.Add("lda $3");
            #endregion
            #endregion
        }
    }
}
