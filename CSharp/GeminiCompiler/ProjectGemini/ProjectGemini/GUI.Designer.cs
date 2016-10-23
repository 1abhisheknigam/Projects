namespace ProjectGemini
{
    partial class GUI
    {
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StackBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ccBox = new System.Windows.Forms.TextBox();
            this.irBox = new System.Windows.Forms.TextBox();
            this.tempBox = new System.Windows.Forms.TextBox();
            this.mdrBox = new System.Windows.Forms.TextBox();
            this.marBox = new System.Windows.Forms.TextBox();
            this.pcBox = new System.Windows.Forms.TextBox();
            this.oBox = new System.Windows.Forms.TextBox();
            this.zBox = new System.Windows.Forms.TextBox();
            this.accBox = new System.Windows.Forms.TextBox();
            this.bBox = new System.Windows.Forms.TextBox();
            this.aBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.stackIndexBox = new System.Windows.Forms.TextBox();
            this.stackButton = new System.Windows.Forms.Button();
            this.instructionPLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.cycleLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.instructionBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source File:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(418, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.openFile);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StackBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.ccBox);
            this.groupBox1.Controls.Add(this.irBox);
            this.groupBox1.Controls.Add(this.tempBox);
            this.groupBox1.Controls.Add(this.mdrBox);
            this.groupBox1.Controls.Add(this.marBox);
            this.groupBox1.Controls.Add(this.pcBox);
            this.groupBox1.Controls.Add(this.oBox);
            this.groupBox1.Controls.Add(this.zBox);
            this.groupBox1.Controls.Add(this.accBox);
            this.groupBox1.Controls.Add(this.bBox);
            this.groupBox1.Controls.Add(this.aBox);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(366, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 308);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registers";
            // 
            // StackBox
            // 
            this.StackBox.Enabled = false;
            this.StackBox.Location = new System.Drawing.Point(81, 279);
            this.StackBox.Name = "StackBox";
            this.StackBox.ReadOnly = true;
            this.StackBox.Size = new System.Drawing.Size(100, 20);
            this.StackBox.TabIndex = 23;
            this.StackBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 282);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "Stack Element";
            // 
            // ccBox
            // 
            this.ccBox.Enabled = false;
            this.ccBox.Location = new System.Drawing.Point(81, 258);
            this.ccBox.Name = "ccBox";
            this.ccBox.ReadOnly = true;
            this.ccBox.Size = new System.Drawing.Size(100, 20);
            this.ccBox.TabIndex = 21;
            this.ccBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // irBox
            // 
            this.irBox.Enabled = false;
            this.irBox.Location = new System.Drawing.Point(81, 236);
            this.irBox.Name = "irBox";
            this.irBox.ReadOnly = true;
            this.irBox.Size = new System.Drawing.Size(100, 20);
            this.irBox.TabIndex = 20;
            this.irBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tempBox
            // 
            this.tempBox.Enabled = false;
            this.tempBox.Location = new System.Drawing.Point(81, 213);
            this.tempBox.Name = "tempBox";
            this.tempBox.ReadOnly = true;
            this.tempBox.Size = new System.Drawing.Size(100, 20);
            this.tempBox.TabIndex = 19;
            this.tempBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mdrBox
            // 
            this.mdrBox.Enabled = false;
            this.mdrBox.Location = new System.Drawing.Point(81, 191);
            this.mdrBox.Name = "mdrBox";
            this.mdrBox.ReadOnly = true;
            this.mdrBox.Size = new System.Drawing.Size(100, 20);
            this.mdrBox.TabIndex = 18;
            this.mdrBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // marBox
            // 
            this.marBox.Enabled = false;
            this.marBox.Location = new System.Drawing.Point(81, 165);
            this.marBox.Name = "marBox";
            this.marBox.ReadOnly = true;
            this.marBox.Size = new System.Drawing.Size(100, 20);
            this.marBox.TabIndex = 17;
            this.marBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pcBox
            // 
            this.pcBox.Enabled = false;
            this.pcBox.Location = new System.Drawing.Point(81, 139);
            this.pcBox.Name = "pcBox";
            this.pcBox.ReadOnly = true;
            this.pcBox.Size = new System.Drawing.Size(100, 20);
            this.pcBox.TabIndex = 16;
            this.pcBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // oBox
            // 
            this.oBox.Enabled = false;
            this.oBox.Location = new System.Drawing.Point(81, 113);
            this.oBox.Name = "oBox";
            this.oBox.ReadOnly = true;
            this.oBox.Size = new System.Drawing.Size(100, 20);
            this.oBox.TabIndex = 15;
            this.oBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // zBox
            // 
            this.zBox.Enabled = false;
            this.zBox.Location = new System.Drawing.Point(81, 87);
            this.zBox.Name = "zBox";
            this.zBox.ReadOnly = true;
            this.zBox.Size = new System.Drawing.Size(100, 20);
            this.zBox.TabIndex = 14;
            this.zBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // accBox
            // 
            this.accBox.Enabled = false;
            this.accBox.Location = new System.Drawing.Point(81, 61);
            this.accBox.Name = "accBox";
            this.accBox.ReadOnly = true;
            this.accBox.Size = new System.Drawing.Size(100, 20);
            this.accBox.TabIndex = 13;
            this.accBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bBox
            // 
            this.bBox.Enabled = false;
            this.bBox.Location = new System.Drawing.Point(81, 38);
            this.bBox.Name = "bBox";
            this.bBox.ReadOnly = true;
            this.bBox.Size = new System.Drawing.Size(100, 20);
            this.bBox.TabIndex = 12;
            this.bBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // aBox
            // 
            this.aBox.Enabled = false;
            this.aBox.Location = new System.Drawing.Point(81, 16);
            this.aBox.Name = "aBox";
            this.aBox.ReadOnly = true;
            this.aBox.Size = new System.Drawing.Size(100, 20);
            this.aBox.TabIndex = 11;
            this.aBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 261);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "CC";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 239);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "IR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "TEMP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "MDR";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "MAR";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "PC";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "One";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Zero";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "ACC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "B";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "A";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.instructionPLabel);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.cycleLabel);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.instructionLabel);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(17, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 295);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 38);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(251, 20);
            this.progressBar1.TabIndex = 8;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.stackIndexBox);
            this.groupBox5.Controls.Add(this.stackButton);
            this.groupBox5.Location = new System.Drawing.Point(6, 183);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(313, 50);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Stack";
            // 
            // stackIndexBox
            // 
            this.stackIndexBox.Location = new System.Drawing.Point(6, 19);
            this.stackIndexBox.Name = "stackIndexBox";
            this.stackIndexBox.Size = new System.Drawing.Size(145, 20);
            this.stackIndexBox.TabIndex = 7;
            this.stackIndexBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // stackButton
            // 
            this.stackButton.Location = new System.Drawing.Point(157, 14);
            this.stackButton.Name = "stackButton";
            this.stackButton.Size = new System.Drawing.Size(150, 28);
            this.stackButton.TabIndex = 6;
            this.stackButton.Text = "View Stack Element";
            this.stackButton.UseVisualStyleBackColor = true;
            this.stackButton.Click += new System.EventHandler(this.stackButton_Click);
            // 
            // instructionPLabel
            // 
            this.instructionPLabel.AutoSize = true;
            this.instructionPLabel.Location = new System.Drawing.Point(269, 41);
            this.instructionPLabel.Name = "instructionPLabel";
            this.instructionPLabel.Size = new System.Drawing.Size(19, 13);
            this.instructionPLabel.TabIndex = 5;
            this.instructionPLabel.Text = "<>";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.infoBox);
            this.groupBox4.Location = new System.Drawing.Point(6, 239);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(313, 44);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(223, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Restart";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.reset);
            // 
            // infoBox
            // 
            this.infoBox.ForeColor = System.Drawing.Color.Green;
            this.infoBox.Location = new System.Drawing.Point(6, 19);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(211, 20);
            this.infoBox.TabIndex = 0;
            // 
            // cycleLabel
            // 
            this.cycleLabel.AutoSize = true;
            this.cycleLabel.Location = new System.Drawing.Point(269, 64);
            this.cycleLabel.Name = "cycleLabel";
            this.cycleLabel.Size = new System.Drawing.Size(19, 13);
            this.cycleLabel.TabIndex = 3;
            this.cycleLabel.Text = "<>";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Instruction Cycle:";
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Location = new System.Drawing.Point(269, 23);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(19, 13);
            this.instructionLabel.TabIndex = 1;
            this.instructionLabel.Text = "<>";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Instruction Index:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.instructionBox);
            this.groupBox3.Location = new System.Drawing.Point(17, 370);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 57);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Current Instruction";
            // 
            // instructionBox
            // 
            this.instructionBox.Location = new System.Drawing.Point(12, 19);
            this.instructionBox.Name = "instructionBox";
            this.instructionBox.ReadOnly = true;
            this.instructionBox.Size = new System.Drawing.Size(307, 20);
            this.instructionBox.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(366, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "Next Instruction";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.executeNext);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(470, 370);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 28);
            this.button3.TabIndex = 7;
            this.button3.Text = "Next Cycle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.executeNextCycle);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(366, 404);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(202, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Run All";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.executeAll);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(-1, 440);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Designed By: Abhishek Nigam";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(578, 454);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.Text = "GeminiX";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ccBox;
        private System.Windows.Forms.TextBox irBox;
        private System.Windows.Forms.TextBox tempBox;
        private System.Windows.Forms.TextBox mdrBox;
        private System.Windows.Forms.TextBox marBox;
        private System.Windows.Forms.TextBox pcBox;
        private System.Windows.Forms.TextBox oBox;
        private System.Windows.Forms.TextBox zBox;
        private System.Windows.Forms.TextBox accBox;
        private System.Windows.Forms.TextBox bBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox aBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox instructionBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label cycleLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label instructionLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.Label instructionPLabel;
        private System.Windows.Forms.TextBox StackBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox stackIndexBox;
        private System.Windows.Forms.Button stackButton;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label15;
    }
}

