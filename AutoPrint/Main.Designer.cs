namespace AutoPrint
{
    partial class Main
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
            this.oTimer_Get = new System.Windows.Forms.Timer(this.components);
            this.btn_printURL = new System.Windows.Forms.Button();
            this.lab_warning = new System.Windows.Forms.Label();
            this.tim_waiting = new System.Windows.Forms.Timer(this.components);
            this.tim_GetCookies = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_Lockall
            // 
            this.btn_Lockall.Location = new System.Drawing.Point(218, 529);
            this.btn_Lockall.Name = "btn_Lockall";
            this.btn_Lockall.Size = new System.Drawing.Size(75, 23);
            this.btn_Lockall.TabIndex = 0;
            this.btn_Lockall.Text = "Lock";
            this.btn_Lockall.UseVisualStyleBackColor = true;
            this.btn_Lockall.Visible = false;
            this.btn_Lockall.Click += new System.EventHandler(this.button1_Click);
            // 
            // oTimer_Get
            // 
            this.oTimer_Get.Interval = 10000;
            this.oTimer_Get.Tick += new System.EventHandler(this.oTimer_Get_Tick);
            // 
            // btn_printURL
            // 
            this.btn_printURL.Location = new System.Drawing.Point(316, 529);
            this.btn_printURL.Name = "btn_printURL";
            this.btn_printURL.Size = new System.Drawing.Size(75, 23);
            this.btn_printURL.TabIndex = 1;
            this.btn_printURL.Text = "PrintALL";
            this.btn_printURL.UseVisualStyleBackColor = true;
            this.btn_printURL.Visible = false;
            this.btn_printURL.Click += new System.EventHandler(this.btn_printURL_Click);
            // 
            // lab_warning
            // 
            this.lab_warning.AutoSize = true;
            this.lab_warning.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_warning.ForeColor = System.Drawing.Color.Red;
            this.lab_warning.Location = new System.Drawing.Point(12, 102);
            this.lab_warning.Name = "lab_warning";
            this.lab_warning.Size = new System.Drawing.Size(1638, 72);
            this.lab_warning.TabIndex = 2;
            this.lab_warning.Text = "正在自动打印,已屏蔽键盘鼠标操作,请耐心等待!!";
            this.lab_warning.Visible = false;
            // 
            // tim_waiting
            // 
            this.tim_waiting.Interval = 10000;
            this.tim_waiting.Tick += new System.EventHandler(this.tim_waiting_Tick);
            // 
            // tim_GetCookies
            // 
            this.tim_GetCookies.Tick += new System.EventHandler(this.tim_GetCookies_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1682, 261);
            this.Controls.Add(this.lab_warning);
            this.Controls.Add(this.btn_printURL);
            this.Controls.Add(this.btn_Lockall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoPrinter";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Lockall;
        private System.Windows.Forms.Timer oTimer_Get;
        private System.Windows.Forms.Button btn_printURL;
        private System.Windows.Forms.Label lab_warning;
        private System.Windows.Forms.Timer tim_waiting;
        private System.Windows.Forms.Timer tim_GetCookies;
    }
}

