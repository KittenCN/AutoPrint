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
        public static string strCookies = "";
        public static string RemoteCookiesInterface = "http://121.196.216.179:70/tlwms/api/chart/harkzt?JSESSIONID=";
        public static int GetCookiesRate = 1440;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SW("button1_Click...");
            lockAll();
            oTimer_Get.Enabled = true;
        }
        private void lockAll()
        {
            SW("lockAll...");
            //BlockInput(true);//锁定鼠标及键盘
        }
        private void oTimer_Get_Tick(object sender, EventArgs e)
        {
            SW("oTimer_Get_Tick...");
            try
            {
                if (!boolMain)
                {
                    //BlockInput(false);
                    boolMain = true;
                    string strSQL = "select * from " + BaseView + " where print_num = " + intDebug;
                    DataSet DSSql = MySqlHelper.MySqlHelper.Query(strSQL, LinkString);
                    SW("SQL::" + strSQL + "||SQL count::" + DSSql.Tables[0].Rows.Count.ToString());
                    if (DSSql.Tables[0].Rows.Count > 0)
                    {
                        SW("Block Input");
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
                SW("Error::" + ex.Message.ToString());
                lab_warning.Visible = false;
                this.TopMost = false;
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
                BlockInput(false);
                boolMain = false;
            }
        }
        private void btn_printURL_Click(object sender, EventArgs e)
        {
            SW("btn_printURL_Click...");
            PrintURL();
        }
        private void PrintURL()
        {
            SW("PrintURL...");
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
            SW("PrintDcument...");
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Document.Encoding = "UTF-8";
            ((WebBrowser)sender).Print();
            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            SW("Main_Load...");
            try
            {
                SW("init...");
                this.Visible = false;
                XmlDocument xmlCon = new XmlDocument();
                xmlCon.Load(strLocalAdd);
                XmlNode xnCon = xmlCon.SelectSingleNode("Config");
                LinkString = xnCon.SelectSingleNode("LinkString").InnerText;
                RemoteInterface = xnCon.SelectSingleNode("RemoteInterface").InnerText;
                DBCacheRate = int.Parse(xnCon.SelectSingleNode("DBCacheRate").InnerText);
                BaseView = xnCon.SelectSingleNode("BaseView").InnerText;
                RemoteCookiesInterface = xnCon.SelectSingleNode("RemoteCookiesInterface").InnerText;
                GetCookiesRate = int.Parse(xnCon.SelectSingleNode("GetCookiesRate").InnerText);
                int intDebugMode = int.Parse(xnCon.SelectSingleNode("DebugMode").InnerText);
                if (intDebugMode == 1)
                {
                    SW("Debug Mode!");
                    intDebug = 1;
                    oTimer_Get_Tick(null, null);
                    GetCookies();
                }
                SW("oTimer Start");
                oTimer_Get.Enabled = true;
                oTimer_Get.Interval = DBCacheRate * 1000 * 60;
                tim_GetCookies.Interval = GetCookiesRate * 1000 * 60;
                tim_GetCookies.Enabled = true;
                SW("Main_Load Success");
            }
            catch (Exception ex)
            {
                SW("Error::" + ex.Message);
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
                MessageBox.Show("Error!::" + ex.Message.ToString());
            }
        }
        private void tim_waiting_Tick(object sender, EventArgs e)
        {
            SW("tim_waiting_Tick...");
            lab_warning.Visible = false;
            this.TopMost = false;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            SW("Release Input");
            BlockInput(false);
            tim_waiting.Enabled = false;
        }
        #region GetCookies
        public static string ResultHtml
        {
            get;
            set;
        }
        /// <summary>
        /// 下一次请求的Url
        /// </summary>
        public static string NextRequestUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 若要从远程调用中获取COOKIE一定要为request设定一个CookieContainer用来装载返回的cookies
        /// </summary>
        public static CookieContainer CookieContainer
        {
            get;
            set;
        }
        /// <summary>
        /// Cookies 字符创
        /// </summary>
        public static string CookiesString
        {
            get;
            set;
        }
        /// <summary>
        /// 用户登陆指定的网站
        /// </summary>
        /// <param name="loginUrl"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public void PostLogin(string loginUrl, string account, string password)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                string postdata = "loginName=" + account + "&password=" + password + "&factoryId=1000&time=" + DateTime.Now.ToString("yyyyMMddHHmmss");//模拟请求数据，数据样式可以用FireBug插件得到。
                                                                                                                                                       // string LoginUrl = "http://www.renren.com/PLogin.do";
                request = (HttpWebRequest)WebRequest.Create(loginUrl);//实例化web访问类  
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";//数据提交方式为POST  
                request.ContentType = "application/x-www-form-urlencoded";    //模拟头  
                request.AllowAutoRedirect = false;   // 不用需自动跳转
                //必须设置CookieContainer存储请求返回的Cookies
                if (CookieContainer != null)
                {
                    request.CookieContainer = CookieContainer;
                }
                else
                {
                    request.CookieContainer = new CookieContainer();
                    CookieContainer = request.CookieContainer;
                }
                request.KeepAlive = true;
                //提交请求  
                byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabytes.Length;
                Stream stream;
                stream = request.GetRequestStream();
                //设置POST 数据
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();
                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                //保存返回cookie  
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookieCollection cook = response.Cookies;
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                CookiesString = strcrook;
                //取下一次GET跳转地址  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string content = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL
                string[] substr = content.Split(new char[] { '"' });
                //NextRequestUrl = substr[1];
                NextRequestUrl = "http://les.zotye.com/role_getMainMenu.do";
            }
            catch (WebException ex)
            {
                SW(string.Format("登陆时出错，详细信息：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 获取用户登陆后下一次请求返回的内容
        /// </summary>
        public void GetPage()
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(NextRequestUrl);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.KeepAlive = true;
                request.Headers.Add("Cookie:" + CookiesString);
                request.CookieContainer = CookieContainer;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();
                //设置cookie  
                CookiesString = request.CookieContainer.GetCookieHeader(request.RequestUri);
                strCookies = CookiesString;
                //取再次跳转链接  
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string ss = sr.ReadToEnd();
                sr.Close();
                request.Abort();
                response.Close();
                //依据登陆成功后返回的Page信息，求出下次请求的url
                //每个网站登陆后加载的Url和顺序不尽相同，以下两步需根据实际情况做特殊处理，从而得到下次请求的URL
                string[] substr = ss.Split(new char[] { '"' });
                NextRequestUrl = substr[1];
                ResultHtml = ss;
            }
            catch (WebException ex)
            {
                SW(string.Format("获取页面HTML信息出错，详细信息：{0}", ex.Message));
            }
        }
        #endregion
        public static string Post(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }
        private void tim_GetCookies_Tick(object sender, EventArgs e)
        {
            GetCookies();
        }
        private void GetCookies()
        {
            SW("GetCookies...");
            PostLogin("http://les.zotye.com/user_login.do", "humengnan", "hmn123123");
            GetPage();
            string strLen = "JSESSIONID=";
            if (strCookies.Substring(0, strLen.Length) == strLen)
            {
                string strResults = strCookies.Split('@')[1].ToString();
                if (strResults != null && strResults.Length > 0)
                {
                    string param = "";
                    string strCallBask = Post(RemoteCookiesInterface + strResults, param);
                    SW("Complete::POST::" + RemoteCookiesInterface + "::" + strResults + "::" + strCallBask);
                }
            }
        }
    }
}
