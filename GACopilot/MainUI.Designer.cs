namespace YourFlightInstructor
{
    partial class MainUI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lableAircraft = new System.Windows.Forms.Label();
            this.btnTop = new System.Windows.Forms.Button();
            this.textSimData = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 25);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // textOutput
            // 
            this.textOutput.Location = new System.Drawing.Point(12, 96);
            this.textOutput.Name = "textOutput";
            this.textOutput.Size = new System.Drawing.Size(776, 223);
            this.textOutput.TabIndex = 2;
            this.textOutput.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(223, 25);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lableAircraft
            // 
            this.lableAircraft.AutoSize = true;
            this.lableAircraft.Location = new System.Drawing.Point(12, 64);
            this.lableAircraft.Name = "lableAircraft";
            this.lableAircraft.Size = new System.Drawing.Size(87, 15);
            this.lableAircraft.TabIndex = 4;
            this.lableAircraft.Text = "Aircraft: ";
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.Control;
            this.btnTop.Location = new System.Drawing.Point(646, 25);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(142, 23);
            this.btnTop.TabIndex = 5;
            this.btnTop.Text = "Always On Top";
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // textSimData
            // 
            this.textSimData.Location = new System.Drawing.Point(15, 336);
            this.textSimData.Name = "textSimData";
            this.textSimData.Size = new System.Drawing.Size(773, 680);
            this.textSimData.TabIndex = 6;
            this.textSimData.Text = "";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1031);
            this.Controls.Add(this.textSimData);
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.lableAircraft);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.Name = "MainUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GACopilot - PREVIEW VERSION";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RichTextBox textOutput;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lableAircraft;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.RichTextBox textSimData;
    }
}

