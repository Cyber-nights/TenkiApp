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
