namespace RobotServerGUI
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NormalPictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MaskedPictureBox = new System.Windows.Forms.PictureBox();
            this.redScrollBar = new System.Windows.Forms.TrackBar();
            this.blueScrollBar = new System.Windows.Forms.TrackBar();
            this.connectionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NormalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaskedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redScrollBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueScrollBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Velocity: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label2.Location = new System.Drawing.Point(16, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "Friend/Foe: ";
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.textBox1.Location = new System.Drawing.Point(227, 43);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(201, 45);
            this.textBox1.TabIndex = 2;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.textBox2.Location = new System.Drawing.Point(227, 130);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(201, 45);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(16, 315);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "Video Feed: ";
            // 
            // NormalPictureBox
            // 
            this.NormalPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NormalPictureBox.Location = new System.Drawing.Point(212, 217);
            this.NormalPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.NormalPictureBox.Name = "NormalPictureBox";
            this.NormalPictureBox.Size = new System.Drawing.Size(460, 356);
            this.NormalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.NormalPictureBox.TabIndex = 4;
            this.NormalPictureBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.Location = new System.Drawing.Point(392, 608);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Normal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label5.Location = new System.Drawing.Point(875, 608);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 29);
            this.label5.TabIndex = 8;
            this.label5.Text = "Masked";
            // 
            // MaskedPictureBox
            // 
            this.MaskedPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MaskedPictureBox.Location = new System.Drawing.Point(688, 217);
            this.MaskedPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.MaskedPictureBox.Name = "MaskedPictureBox";
            this.MaskedPictureBox.Size = new System.Drawing.Size(460, 356);
            this.MaskedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MaskedPictureBox.TabIndex = 9;
            this.MaskedPictureBox.TabStop = false;
            // 
            // redScrollBar
            // 
            this.redScrollBar.BackColor = System.Drawing.Color.Red;
            this.redScrollBar.LargeChange = 10;
            this.redScrollBar.Location = new System.Drawing.Point(749, 153);
            this.redScrollBar.Margin = new System.Windows.Forms.Padding(4);
            this.redScrollBar.Maximum = 90;
            this.redScrollBar.Name = "redScrollBar";
            this.redScrollBar.Size = new System.Drawing.Size(139, 56);
            this.redScrollBar.SmallChange = 5;
            this.redScrollBar.TabIndex = 10;
            this.redScrollBar.TickFrequency = 10;
            this.redScrollBar.Value = 50;
            this.redScrollBar.ValueChanged += new System.EventHandler(this.redScrollBar_ValueChanged);
            // 
            // blueScrollBar
            // 
            this.blueScrollBar.BackColor = System.Drawing.Color.Blue;
            this.blueScrollBar.LargeChange = 10;
            this.blueScrollBar.Location = new System.Drawing.Point(950, 153);
            this.blueScrollBar.Margin = new System.Windows.Forms.Padding(4);
            this.blueScrollBar.Maximum = 90;
            this.blueScrollBar.Name = "blueScrollBar";
            this.blueScrollBar.Size = new System.Drawing.Size(139, 56);
            this.blueScrollBar.SmallChange = 5;
            this.blueScrollBar.TabIndex = 11;
            this.blueScrollBar.TickFrequency = 10;
            this.blueScrollBar.Value = 50;
            this.blueScrollBar.ValueChanged += new System.EventHandler(this.blueScrollBar_ValueChanged);
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionLabel.ForeColor = System.Drawing.Color.Red;
            this.connectionLabel.Location = new System.Drawing.Point(763, 58);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(236, 25);
            this.connectionLabel.TabIndex = 12;
            this.connectionLabel.Text = "Waiting for a connection...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1164, 670);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.blueScrollBar);
            this.Controls.Add(this.redScrollBar);
            this.Controls.Add(this.MaskedPictureBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NormalPictureBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Robot Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.NormalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaskedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redScrollBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueScrollBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox NormalPictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox MaskedPictureBox;
        private System.Windows.Forms.TrackBar redScrollBar;
        private System.Windows.Forms.TrackBar blueScrollBar;
        private System.Windows.Forms.Label connectionLabel;
    }
}

