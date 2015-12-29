namespace PeriapsisQuests
{
    partial class fEditNode
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.psReward = new PeriapsisQuests.PlayerStatCounter();
            this.psCompletedReq = new PeriapsisQuests.PlayerStatCounter();
            this.psPreReq = new PeriapsisQuests.PlayerStatCounter();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Comment";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(45, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(15, 52);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 69);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(181, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 241);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "PreRequisites";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "CompletedReqs";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(612, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Rewards";
            // 
            // psReward
            // 
            this.psReward.AutoScroll = true;
            this.psReward.BackColor = System.Drawing.SystemColors.ControlLight;
            this.psReward.Location = new System.Drawing.Point(615, 22);
            this.psReward.Name = "psReward";
            this.psReward.Size = new System.Drawing.Size(208, 192);
            this.psReward.TabIndex = 2;
            // 
            // psCompletedReq
            // 
            this.psCompletedReq.AutoScroll = true;
            this.psCompletedReq.BackColor = System.Drawing.SystemColors.ControlLight;
            this.psCompletedReq.Location = new System.Drawing.Point(401, 22);
            this.psCompletedReq.Name = "psCompletedReq";
            this.psCompletedReq.Size = new System.Drawing.Size(208, 192);
            this.psCompletedReq.TabIndex = 2;
            // 
            // psPreReq
            // 
            this.psPreReq.AutoScroll = true;
            this.psPreReq.BackColor = System.Drawing.SystemColors.ControlLight;
            this.psPreReq.Location = new System.Drawing.Point(187, 22);
            this.psPreReq.Name = "psPreReq";
            this.psPreReq.Size = new System.Drawing.Size(208, 192);
            this.psPreReq.TabIndex = 2;
            // 
            // fEditNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 259);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.psReward);
            this.Controls.Add(this.psCompletedReq);
            this.Controls.Add(this.psPreReq);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fEditNode";
            this.Text = "Edit - ";
            this.Load += new System.EventHandler(this.fEditNode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private PlayerStatCounter psPreReq;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private PlayerStatCounter psCompletedReq;
        private System.Windows.Forms.Label label5;
        private PlayerStatCounter psReward;
    }
}