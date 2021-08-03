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
using Microsoft.EntityFrameworkCore;

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
        WInForms.ToolStripMenuItem menuitem2 = new WInForms.ToolStripMenuItem();
        WInForms.ToolStripSeparator separator = new WInForms.ToolStripSeparator();
        public App()
        {
            menuitem.MergeIndex = 1;
            menuitem.Text = "Выход";
            menuitem.Click += menuitemexit;

            menuitem2.MergeIndex = 0;
            menuitem2.Text = "Меню";
            menuitem2.Click += mainmenu;

            contextmenu.Items.Add(menuitem2);
            contextmenu.Items.Add(separator);
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
                MainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                double height = SystemParameters.FullPrimaryScreenHeight;
                double width = SystemParameters.FullPrimaryScreenWidth;
                MainWindow.Top = height - MainWindow.Height+13;
                MainWindow.Left = width - MainWindow.Width-10;
            }
        }
        void menuitemexit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        void mainmenu(object sender, EventArgs e)
        {
            if (MainWindow.WindowState == WindowState.Minimized)
            {
                noti.Visible = false;
                MainWindow.ShowInTaskbar = true;
                MainWindow.WindowState = WindowState.Normal;
                MainWindow.Activate();
                MainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                double height = SystemParameters.FullPrimaryScreenHeight;
                double width = SystemParameters.FullPrimaryScreenWidth;
                MainWindow.Top = height - MainWindow.Height + 13;
                MainWindow.Left = width - MainWindow.Width - 10;
            }
        }

        public class PogodaContext : DbContext
        {
            public DbSet<Country> countrys { get; set; }
            public DbSet<Area> areas{ get; set; }
            public DbSet<City> citys { get; set; }
            /*protected override void OnModelCreating(ModelBuilder modelbuilder)
            {
                //modelbuilder.Entity<TableCountry>().HasNoKey();
            }*/
            protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
            {
                optionsbuilder.UseSqlite("Data Source=MyProject.sqlite");
                //optionsbuilder.UseModel(MyCompiledModels.BlogsContextModel.Instance);
            }

        }
        public class Country
        {
            public int id { get; set; }
            public string name { get; set; }
            public string link { get; set; }
        }
        public class Area
        {
            public int id { get; set; }
            public string name { get; set; }
            public string link { get; set; }
            public int id_country { get; set; }
        }
        public class City
        {
            public int id { get; set; }
            public string name { get; set; }
            public string link { get; set; }
            public int id_area { get; set; }
        }
        public class TableCountry
        {
            public int countryid { get; set; }
            public string countryname { get; set; }
            public string countrylink { get; set; }
            public int areaid { get; set; }
            public string areaname { get; set; }
            public string arealink { get; set; }
            public int cityid { get; set; }
            public string cityname { get; set; }
            public string citylink { get; set; }
        }
    }
}
