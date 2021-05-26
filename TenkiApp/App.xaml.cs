using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//
using WInForms = System.Windows.Forms;
using System.Drawing;
//
namespace TenkiApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        WInForms.NotifyIcon noti = new WInForms.NotifyIcon();
        WInForms.ContextMenuStrip contextmenu = new WInForms.ContextMenuStrip();
        WInForms.ToolStripMenuItem menuitem = new WInForms.ToolStripMenuItem();
        public App()
        {
            menuitem.MergeIndex = 0;
            menuitem.Text = "Выход";
            menuitem.Click += menuitemexit;
            contextmenu.Items.Add(menuitem);
            //contextmenu.Items.AddRange(new WInForms.ContextMenuStrip[]{ menuitem});
            noti.Icon = new Icon(@"refresh.ico");
            noti.DoubleClick += notidouble;
            noti.Text = "Погода :З";
            noti.ContextMenuStrip = contextmenu;
            noti.ShowBalloonTip(5000, "Title", "Text", WInForms.ToolTipIcon.Info);
        }

        private void Application_Deactivated(object sender, EventArgs e)
        {
            if (MainWindow.WindowState == WindowState.Minimized)
            {
                MainWindow.ShowInTaskbar = false;
                noti.Visible = true;
            }
        }
        void notidouble(object sender, EventArgs e)
        {
            if (MainWindow.WindowState == WindowState.Minimized)
            {
                noti.Visible = false;
                MainWindow.ShowInTaskbar = true;
                MainWindow.WindowState = WindowState.Normal;
                MainWindow.Activate();
            }
        }
        void menuitemexit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
