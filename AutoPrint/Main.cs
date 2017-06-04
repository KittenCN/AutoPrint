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
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Net;
using System.Xml;

namespace AutoPrint
{
    public partial class Main : Form
    {
        //引入API函数
        [DllImport("user32 ")]
        public static extern bool LockWorkStation();//这个是调用windows的系统锁定
        [DllImport("user32.dll")]
        static extern void BlockInput(bool Block);
        //public static string strPrintURL = "file:///" + Directory.GetCurrentDirectory() + "/PrintTemp/print.html";
        public static string strLocalAdd = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
        public static string LinkString = "Server = 121.196.216.179;Port = 9980;Database = tlwms;User = tlwms;Password = tlwms;";
        public static string RemoteInterface = "http://121.196.216.179:70/tlwms/api/print/psd_auto?print_num=";
        public static int DBCacheRate = 1;
        public static string BaseView = "view_fcd_detail";
        public static int intDebug = 0;
        public static Boolean boolWaiting = false;
        public static Boolean boolMain = false;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lockAll();
            oTimer_Get.Enabled = true;
        }
        private void lockAll()
        {
            //BlockInput(true);//锁定鼠标及键盘
        }

        private void oTimer_Get_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!boolMain)
                {
                    //BlockInput(false);
                    boolMain = true;
                    string strSQL = "select * from " + BaseView + " where print_num = " + intDebug;
                    DataSet DSSql = MySqlHelper.MySqlHelper.Query(strSQL, LinkString);
                    if (DSSql.Tables[0].Rows.Count > 0)
                    {
                        BlockInput(true);
                        lab_warning.Visible = true;
                        this.Visible = true;
                        this.WindowState = FormWindowState.Normal;
                        this.TopMost = true;
                        boolWaiting = false;
                        PrintURL();
                        tim_waiting.Enabled = true;
                        //lab_warning.Visible = false;
                        //this.TopMost = false;
                        //this.WindowState = FormWindowState.Minimized;
                        //this.Visible = false;
                        //BlockInput(false);
                    }
                    boolMain = false;
                }
            }
            catch(Exception ex)
            {
                lab_warning.Visible = false;
                this.TopMost = false;
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
                BlockInput(false);
                boolMain = false;
            }
        }
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            
        }
        private void btn_printURL_Click(object sender, EventArgs e)
        {
            PrintURL();
        }
        private void PrintURL()
        {
            // Create a WebBrowser instance. 
            WebBrowser webBrowserForPrinting = new WebBrowser();
            // Add an event handler that prints the document after it loads.
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);
            // Set the Url property to load the document.               
            webBrowserForPrinting.Url = new Uri(RemoteInterface + intDebug);
        }
        private void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Document.Encoding = "UTF-8";
            ((WebBrowser)sender).Print();
            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SW("Service Start.");
            try
            {
                this.Visible = false;
                XmlDocument xmlCon = new XmlDocument();
                xmlCon.Load(strLocalAdd);
                XmlNode xnCon = xmlCon.SelectSingleNode("Config");
                LinkString = xnCon.SelectSingleNode("LinkString").InnerText;
                RemoteInterface = xnCon.SelectSingleNode("RemoteInterface").InnerText;
                DBCacheRate = int.Parse(xnCon.SelectSingleNode("DBCacheRate").InnerText);
                BaseView = xnCon.SelectSingleNode("BaseView").InnerText;
                int intDebugMode = int.Parse(xnCon.SelectSingleNode("DebugMode").InnerText);

                if (intDebugMode == 1)
                {
                    SW("Debug Mode!");
                    intDebug = 1;
                    oTimer_Get_Tick(null, null);
                }
                oTimer_Get.Enabled = true;
                oTimer_Get.Interval = DBCacheRate * 1000 * 60;
                SW("MainEvent Success");
            }
            catch (Exception ex)
            {
                SW(ex.Source + "。" + ex.Message);
            }
        }
        private void SW(string strT)
        {
            try
            {
                string str_logName = DateTime.Now.ToString("yyyyMMdd") + "_log.txt";
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + str_logName, true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + strT);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tim_waiting_Tick(object sender, EventArgs e)
        {
            lab_warning.Visible = false;
            this.TopMost = false;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            BlockInput(false);
            tim_waiting.Enabled = false;
        }
    }
}
