namespace WindowsFormsApp1
{
    partial class ProgressForm
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
            this.pgbMain = new System.Windows.Forms.ProgressBar();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.btnProcessStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgbMain
            // 
            this.pgbMain.Location = new System.Drawing.Point(-1, 30);
            this.pgbMain.Name = "pgbMain";
            this.pgbMain.Size = new System.Drawing.Size(316, 31);
            this.pgbMain.TabIndex = 0;
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Location = new System.Drawing.Point(129, 9);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(35, 12);
            this.lblProcessStatus.TabIndex = 1;
            this.lblProcessStatus.Text = "0/100";
            // 
            // btnProcessStop
            // 
            this.btnProcessStop.Location = new System.Drawing.Point(111, 61);
            this.btnProcessStop.Name = "btnProcessStop";
            this.btnProcessStop.Size = new System.Drawing.Size(75, 23);
            this.btnProcessStop.TabIndex = 2;
            this.btnProcessStop.Text = "终止操作";
            this.btnProcessStop.UseVisualStyleBackColor = true;
            this.btnProcessStop.Click += new System.EventHandler(this.btnProcessStop_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(315, 85);
            this.Controls.Add(this.btnProcessStop);
            this.Controls.Add(this.lblProcessStatus);
            this.Controls.Add(this.pgbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProgressForm";
            this.Text = "执行中，请耐心等待....";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbMain;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.Button btnProcessStop;
    }
}