using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace AutoPrint
{
    public partial class Main : Form
    {
        //引入API函数
        [DllImport("user32 ")]
        public static extern bool LockWorkStation();//这个是调用windows的系统锁定
        [DllImport("user32.dll")]
        static extern void BlockInput(bool Block);
        public static string strPrintURL = "file:///" + Directory.GetCurrentDirectory() + "/PrintTemp/print.html";
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lockAll();
            tim_Unlock.Enabled = true;
        }
        private void lockAll()
        {
            BlockInput(true);//锁定鼠标及键盘
        }

        private void tim_Unlock_Tick(object sender, EventArgs e)
        {
            BlockInput(false);
        }

        private void btn_printURL_Click(object sender, EventArgs e)
        {
            // Create a WebBrowser instance. 
            WebBrowser webBrowserForPrinting = new WebBrowser();
            // Add an event handler that prints the document after it loads.
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);
            // Set the Url property to load the document.   
            strPrintURL = "http://121.196.216.179:70/tlwms/api/print/psd_auto?print_num=1";
            webBrowserForPrinting.Url = new Uri(strPrintURL);
        }
        private void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Document.Encoding = "UTF-8";
            ((WebBrowser)sender).Print();
            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }
    }
}
