namespace AutoPrint
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btn_Lockall = new System.Windows.Forms.Button();
            this.tim_Unlock = new System.Windows.Forms.Timer(this.components);
            this.btn_printURL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Lockall
            // 
            this.btn_Lockall.Location = new System.Drawing.Point(110, 407);
            this.btn_Lockall.Name = "btn_Lockall";
            this.btn_Lockall.Size = new System.Drawing.Size(75, 23);
            this.btn_Lockall.TabIndex = 0;
            this.btn_Lockall.Text = "Lock";
            this.btn_Lockall.UseVisualStyleBackColor = true;
            this.btn_Lockall.Click += new System.EventHandler(this.button1_Click);
            // 
            // tim_Unlock
            // 
            this.tim_Unlock.Interval = 10000;
            this.tim_Unlock.Tick += new System.EventHandler(this.tim_Unlock_Tick);
            // 
            // btn_printURL
            // 
            this.btn_printURL.Location = new System.Drawing.Point(348, 407);
            this.btn_printURL.Name = "btn_printURL";
            this.btn_printURL.Size = new System.Drawing.Size(75, 23);
            this.btn_printURL.TabIndex = 1;
            this.btn_printURL.Text = "PrintALL";
            this.btn_printURL.UseVisualStyleBackColor = true;
            this.btn_printURL.Click += new System.EventHandler(this.btn_printURL_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 583);
            this.Controls.Add(this.btn_printURL);
            this.Controls.Add(this.btn_Lockall);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Lockall;
        private System.Windows.Forms.Timer tim_Unlock;
        private System.Windows.Forms.Button btn_printURL;
    }
}

