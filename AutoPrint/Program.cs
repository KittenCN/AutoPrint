using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPrint
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "AutoPrint");
            bool Running = !mutex.WaitOne(0, false);
            if (!Running)
            {
                MessageBox.Show("程序启动中...");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            else
            {
                MessageBox.Show("程序已启动,请勿重复操作！");
                Application.Exit();
            }
        }
    }
}
