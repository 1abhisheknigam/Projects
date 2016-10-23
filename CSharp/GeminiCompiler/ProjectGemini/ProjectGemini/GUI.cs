using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

//Author: Abhishek Nigam
namespace ProjectGemini
{
    public partial class GUI : Form
    {
        /// <summary>
        /// The GPU Class which is responsible
        /// for the main user interface. Updates
        /// according to the state of the CPU.
        /// </summary>
        public CPU CPU { get; set; }
        private Boolean compiled = false;
        private Boolean firstTime = true;

        public GUI(ProjectGemini.CPU CPU1)
        {
            CPU = CPU1;
            InitializeComponent();
            showRegisters();
        }

        /// <summary>
        /// Event for Open button. Allows user to select a file.
        /// Then calls the IPE to start compiling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFile(object sender, EventArgs e)
        {
            if (!firstTime) reset();
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var str = ofd.FileName;
                        textBox1.Text = str;
                        String[] allText = System.IO.File.ReadAllLines(str);
                        if (CPU.IPE.inFile(allText)) { compiled = true; firstTime = false; infoBox.ForeColor = Color.Green; }
                        else infoBox.ForeColor = Color.Red;
                        infoBox.Text = CPU.IPE.Err;
                    }
                    catch (Exception err)
                    {
                        infoBox.Text = err.Message;
                        infoBox.ForeColor = Color.Red;
                    }
                }
            }
        }

        /// <summary>
        /// Helper function that resets all the textboxes
        /// with current register values.
        /// </summary>
        private void showRegisters()
        {
            aBox.Text = CPU._a.ToString();
            bBox.Text = CPU._b.ToString();
            accBox.Text = CPU._acc.ToString();
            zBox.Text = CPU._ZERO.ToString();
            oBox.Text = CPU._ONE.ToString();
            pcBox.Text = CPU._pc.ToString();
            marBox.Text = CPU._mar.ToString();
            mdrBox.Text = CPU._mdr.ToString();
            tempBox.Text = CPU._temp.ToString();
            irBox.Text = CPU._ir;
            ccBox.Text = CPU._cc.ToString();
        }

        /// <summary>
        /// Event handler for Execute Next Instruction button.
        /// Calls the CPU.exectueNext() to execute the next line
        /// of the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executeNext(object sender, EventArgs e)
        {
            if (compiled)
            {
                if (CPU.executeNext()) showRegisters();
                else { if (!(CPU.Err == "Run Complete.")) infoBox.ForeColor = Color.Red; }
                instructionBox.Text = CPU.currentInstruction;
                instructionLabel.Text = CPU.getInstructionIndex();
                progressBar1.Value = CPU.getInstructionPercentageInt();
                instructionPLabel.Text = CPU.getInstructionPercentage();
                showStack();
                infoBox.Text = CPU.Err;
            }
            else infoBox.Text = "File was not Compiled, Check console.";
        }

        /// <summary>
        /// Event handler for Show Stack button that calls showStack method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stackButton_Click(object sender, EventArgs e)
        {
            showStack();
        }

        /// <summary>
        /// This helper method shows a Stack element at index "i".
        /// It is a helper function because it is called in different 
        /// methods.
        /// </summary>
        private void showStack()
        {
            if (!String.IsNullOrEmpty(stackIndexBox.Text))
                if (Regex.IsMatch(stackIndexBox.Text, @"^[0-9]+$"))
                    StackBox.Text = CPU.getStackElementAt(Convert.ToInt32(stackIndexBox.Text));
                else StackBox.Text = "Numbers Only";
        }

        /// <summary>
        /// Event handler for Execute All button that executes all remaining
        /// instructions automatically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executeAll(object sender, EventArgs e)
        {
            if (compiled)
            {
                while (CPU.executeNext())
                {
                    showRegisters();
                    instructionBox.Text = CPU.currentInstruction;
                    instructionLabel.Text = CPU.getInstructionIndex();
                    instructionPLabel.Text = CPU.getInstructionPercentage();
                    progressBar1.Value = CPU.getInstructionPercentageInt();
                    showStack();
                    infoBox.Text = CPU.Err;
                }
                if (!(CPU.Err.Equals("Run Complete")))
                {
                    infoBox.ForeColor = Color.Red;
                    instructionBox.Text = CPU.currentInstruction;
                    instructionLabel.Text = CPU.getInstructionIndex();
                    instructionPLabel.Text = CPU.getInstructionPercentage();
                    progressBar1.Value = CPU.getInstructionPercentageInt();
                    showStack();
                }
                infoBox.Text = CPU.Err;
            }
            else infoBox.Text = "File was not Compiled. check console.";
        }

        /// <summary>
        /// Event handleer for Restart button which allows the user to rerun
        /// the current program without having to reload the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reset(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            CPU.resetAll();
            showRegisters();
            showStack();
            infoBox.ForeColor = Color.Green;
            instructionBox.Text = CPU.currentInstruction;
            instructionLabel.Text = CPU.getInstructionIndex();
            instructionPLabel.Text = CPU.getInstructionPercentage();
            progressBar1.Value = CPU.getInstructionPercentageInt();
            infoBox.Text = CPU.Err;
        }
        /// <summary>
        /// Event handler for Next Cycle button. Method stub.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executeNextCycle(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Not Implemented Yet.");
        }
    }
}
