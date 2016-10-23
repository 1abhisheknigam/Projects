using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//Author: Marlin Blue
namespace ProjectGemini
{
    /// <summary>
    /// Instruction Parser. Will be developed by Marlin.
    /// </summary>
    public class IPE
    {
        char[] delims = new char[] { ' ', '\t', ':' };
        String[] splitLn = new String[25];
        String[] cmds = { "lda", "sta", "add", "sub", 
                                "and", "or", "nota", "be", 
                                "bl", "bg", "ba", "nop" };
        Dictionary<string, int> labels { get; set; }
        List<string> instructions { get; set; }
        public string Err = "";

        public Memory Memory { get; set; }

        public IPE(Memory m)
        {
            Memory = m;
            instructions = new List<string>();
            labels = new Dictionary<string, int>();
            Memory.Instructions = instructions;
            Memory.Labels = labels;
        }


        public Boolean inFile(String[] Text)
        {
            String[] lines = Text;
            //In case there are previous instructions and labels, these are cleared.
            instructions.Clear();
            labels.Clear();

            try
            {
                    int sub = 0;
                    // Checking if the line contains a label
                    // If so, then the label is placed in a map
                    for (int j = 0; j < lines.Count(); j++)
                    {

                        if (isLabel(lines[j]))
                        {
                            labels.Add(lines[j].Split(delims, StringSplitOptions.RemoveEmptyEntries)[0], j - sub);
                            lines[j] = j.ToString();
                            sub++;
                        }  
                        else if(String.IsNullOrWhiteSpace(lines[j]))sub++;
                    }
                    // Placing instructions into memory list
                    for (int k = 0; k < lines.Count(); k++)
                    {
                        if (lines[k].IndexOf('\t') != -1)
                            lines[k] = Regex.Replace(lines[k], @"\t", "");

                        // Checking if the line contains an instruction
                        if (isCommand(lines[k]))
                        {
                            if (lines[k].IndexOf("!") != -1)
                            {
                                // This removes comments from the instructions
                                lines[k] = lines[k].Split('!')[0];
                            }
                            if (isValid(lines[k]))
                            {
                                String[] temp = lines[k].Split(new char[] { ' ' }, 
                                    StringSplitOptions.RemoveEmptyEntries);
                               

                                if (temp.Count() < 2)
                                    instructions.Add(temp[0]);
                                else
                                    instructions.Add(temp[0] + " " + temp[1]);

                            }
                            else 
                            {
                                Err = "There was a compile error at Line " + k.ToString() + ":" + lines[k];
                                Console.WriteLine(Err);
                                return false;
                            }  
                        } 
                    }

                List<string> tempList = new List<string>();
                foreach(String l in instructions){
                    String[] words = l.Split(' ');
                    switch(words[0][0]){
                        case 'b':
                            {
                                tempList.Add(words[0] + " " + labels[words[1]].ToString());
                                System.Console.Write(words[1] + "= Line " + labels[words[1]].ToString() + "\n");
                                break;
                            }
                        default:
                            {
                                tempList.Add(l);
                                break;
                            }
                    }
                }
                instructions = tempList;
                foreach (String l in instructions) System.Console.WriteLine(l);
                Memory.Instructions = tempList;
                Err = "Compile Successful";
                    return true;                
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }

        } // end of inFile()



        // Tests whether line contains a label
        public Boolean isLabel(String value)
        {

            int subIndex = value.IndexOf(":");
            if (subIndex != -1)
                return true;

            return false;
        }

        // Tests if line contains a command, which command,
        // and if any errors exist
        public Boolean isCommand(String value)
        {
            int subIndex;
            for (int i = 0; i < cmds.Count(); i++)
            {
                subIndex = value.IndexOf(cmds[i]);
                if (subIndex != -1)
                {
                    return true;
                }
            }

            return false;
        } // end of isCommand()

        // Checks format/correctness of given instruction
        public Boolean isValid(String value)
        {
            String[] inst1 = { "lda", "add", "sub", "and", "or" };
            String[] inst2 = { "ba", "bl", "bg", "be", " " };       // <--filling space to combat 
            String[] instLog = { "nota", "nop", " ", " ", " " };    // <--'out of bounds exception'
            int selector = 0;

            // Splits the current line into separate "words"
            // and stores them into the 'splitLn' array
            splitLn = value.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Checks to see if instruction/param format is 
            // correct. There should only be 2 entries: instruction, param.
            // Any more will result in error
            if (splitLn.Count() != 2)
            {
                if (splitLn.Count() == 1)
                {
                    if(splitLn[0] != "nota" && splitLn[0] != "nop")
                        return false;
                    else //if (splitLn[0] == "nota" && splitLn[0] == "nop")
                    {
                        return true;
                    }
                }
                return false;
            }

            // Checking which type of command is being read
            // This will help with checking for correct params
            for (int i = 0; i < inst1.Count(); i++)
            {
                int index1 = splitLn[0].IndexOf(inst1[i]);
                int index2 = splitLn[0].IndexOf(inst2[i]);
                int index3 = splitLn[0].IndexOf(instLog[i]);

                // Arithmetic operations
                if (index1 != -1)
                {
                    selector = 1;
                    break;
                }
                // Branch operations
                else if (index2 != -1)
                {
                    selector = 2;
                    break;
                }
                // 'NOP' and 'NOTA' operations
                else if (index3 != -1)
                {
                    selector = 3;
                    break;
                }
                // 'STA' operation
                else if (splitLn[0] == "sta")
                {
                    selector = 4;
                    break;
                }

            }

            // Based on the selector, the following cases will 
            // check if the given params correctly correspond to
            // the given instruction
            switch (selector)
            {
                case 1:
                    {
                        if ((splitLn[1].Count() >= 2) &&
                            (splitLn[1][0] == '$') && (Char.IsDigit(splitLn[1][1])))
                        {
                            return true;
                        }
                        else if ((splitLn[1].Count() >= 3) && (splitLn[1][0] == '#') &&
                            (splitLn[1][1] == '$') && (Char.IsNumber(splitLn[1][2])))
                        {
                            return true;
                        }
                        else
                        {
                            //Console.WriteLine("error");
                            return false;
                        }
                    }
                case 2:
                    foreach (KeyValuePair<string, int> pair in labels)
                    {
                        if (splitLn[1].IndexOf(pair.Key) != -1)
                        {
                            return true;
                        }
                    }
                    Console.WriteLine("invalid branch argument");
                    break;
                case 3:
                    if (splitLn.Count() > 1)
                    {
                        Console.WriteLine("error: this instruction takes no arguments");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case 4:
                    if ((splitLn[1].Count() >= 2) &&
                        (splitLn[1][0] == '$') && (Char.IsDigit(splitLn[1][1])))
                    {
                        //Console.WriteLine("no error");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("error");
                        return false;
                    }


            }


            return false;
        }
    }
}
