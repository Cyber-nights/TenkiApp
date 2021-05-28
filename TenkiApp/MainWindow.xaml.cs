using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;//проверка
using HtmlAgilityPack;
using System.IO;///шщ
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Media.Animation;

namespace TenkiApp
{
    public partial class MainWindow : Window
    {
        static string save = "saveinfo.txt", savedata = "", link = ""; string[] c = new string[70]; int i = 0;
        //FileInfo f = new FileInfo(save);
        StreamReader str = new StreamReader(save);
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
            try
            {
                while (!str.EndOfStream)
                {
                    try
                    {
                        c[i] += str.ReadLine();
                    }
                    catch (NullReferenceException) { }
                    i++;
                }
                str.Close();
                cb1.SelectedItem = c[0];
                cb2.SelectedItem = c[1];
                cb3.SelectedItem = c[2];
                link = c[3];
                Info();
                using (SQLiteConnection Connect = new SQLiteConnection("Data Source = MyProject.sqlite;"))
                {
                    Connect.Open();
                    SQLiteCommand command = new SQLiteCommand(@"SELECT countryname FROM Pogodas WHERE countryname <> ''", Connect);
                    SQLiteDataReader dann = command.ExecuteReader();
                    while (dann.Read())
                    {
                        object d = dann[0];
                        cb1.Items.Add(d);
                    }
                    Connect.Close();
                }
                if (cb1.Text != "")
                    cb2.Visibility = Visibility.Visible;
                if (cb2.Text == "")
                    cb2.Visibility = Visibility.Hidden;
                if (cb3.Text == "")
                    cb3.Visibility = Visibility.Hidden;
            }
            catch (Exception) { MessageBox.Show("Ошибка на этапе запуска, пожалуйста, передайте это разработчику", "Окошко надежды"); }
        }
        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cb2.Items.Clear(); cb3.Items.Clear();
                cb2.Visibility = Visibility.Visible;
                cb3.Visibility = Visibility.Hidden;
                //
                using (SQLiteConnection Connect = new SQLiteConnection("Data Source = MyProject.sqlite;"))
                {
                    Connect.Open();
                    SQLiteCommand command = new SQLiteCommand(@"SELECT areaname FROM Pogodas WHERE countryid ='" + (cb1.SelectedIndex + 1).ToString() + "' AND areaname <> '';", Connect);
                    SQLiteDataReader dann = command.ExecuteReader();
                    while (dann.Read())
                    {
                        object d = dann[0];
                        cb2.Items.Add(d);
                    }
                    Connect.Close();
                }
            }
            catch (Exception) { MessageBox.Show("Ошибка на первом этапе, пожалуйста, передайте это разработчику", "Окошко надежды"); }
        }
        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cb3.Items.Clear();
                using (SQLiteConnection Connect = new SQLiteConnection("Data Source = MyProject.sqlite;"))
                {
                    Connect.Open();
                    //SELECT arealink FROM Pogodas WHERE countryid = '154' AND areaname <> '';
                    SQLiteCommand command = new SQLiteCommand(@"SELECT cityname FROM Pogodas WHERE countryid ='" + (cb1.SelectedIndex + 1).ToString() + "' AND areaid = '" + (cb2.SelectedIndex + 1).ToString() + "' AND cityname <> ''", Connect);
                    SQLiteCommand commandlink = new SQLiteCommand(@"SELECT arealink FROM Pogodas WHERE countryid ='" + (cb1.SelectedIndex + 1).ToString() + "' AND areaid = '" + (cb2.SelectedIndex + 1).ToString() + "' AND arealink <> ''", Connect);
                    SQLiteDataReader dannlink = commandlink.ExecuteReader();
                    SQLiteDataReader dann = command.ExecuteReader();
                    while (dann.Read())
                    {
                        object d = dann[0];
                        cb3.Items.Add(d);
                    }
                    while (dannlink.Read())
                    {
                        object dlink = dannlink[0];
                        if (cb3.Items.Count == 0) link = dlink.ToString();
                    }
                    Connect.Close();
                }
                if (cb3.Items.Count != 0) cb3.Visibility = Visibility.Visible;
                Info();
            }
            catch (Exception) { MessageBox.Show("Ошибка на втором этапе, пожалуйста, передайте это разработчику", "Окошко надежды"); }
        }
        private void cb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (SQLiteConnection Connect = new SQLiteConnection("Data Source = MyProject.sqlite;"))
                {
                    Connect.Open();
                    SQLiteCommand commandlink = new SQLiteCommand(@"SELECT citylink FROM Pogodas WHERE countryid ='" + (cb1.SelectedIndex + 1).ToString() + "' AND areaid = '" + (cb2.SelectedIndex + 1).ToString() + "' AND cityid = '" + (cb3.SelectedIndex + 1).ToString() + "'", Connect);
                    SQLiteDataReader dannlink = commandlink.ExecuteReader();
                    while (dannlink.Read())
                    {
                        object dlink = dannlink[0];
                        if (cb3.Items.Count != 0) link = dlink.ToString();
                    }
                    Connect.Close();
                }
                Info();
            }
            catch (Exception) { MessageBox.Show("Ошибка на третьем этапе, пожалуйста, передайте это разработчику", "Окошко надежды"); }
        }
        private void b3_Click(object sender, RoutedEventArgs e)//проверка данных
        {
            //list1.Items.Clear(); 
            i = 0;
            //string teg = tb1.Text;
            try
            {
                HtmlWeb webinfo = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument docwebinfo = webinfo.Load(link);
                HtmlNode[] temperature;
                temperature = docwebinfo.DocumentNode.SelectNodes("//span[@class='temp__value temp__value_with-unit']").ToArray();
                foreach (HtmlNode t in temperature)
                {
                    i++;
                    if (i == 2) { l1.Content = t.InnerText; i = 0; break; }
                }
                //{ list1.Items.Add(t.InnerText); }

                HtmlNode[] wind = docwebinfo.DocumentNode.SelectNodes("//span[@class='wind-speed']").ToArray();
                foreach (HtmlNode w in wind)
                { l2.Content = w.InnerText; break; }
                //{ list1.Items.Add(w.InnerText); }
                ////span[@class='temp__value']
            }
            catch { MessageBox.Show("Не вышло", "Ошибка"); }
        }
        private void brefresh_Click(object sender, RoutedEventArgs e)
        {
            Info();
        }
        public void saveinfo()
        {
            try
            {
                savedata = cb1.Text + "\r" + cb2.Text + "\r" + cb3.Text;
                if (link != "" || link != null) savedata += "\r" + link;
                using (StreamWriter saver = new StreamWriter(save, false))
                {
                    saver.WriteLine(savedata);
                    saver.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\rСохранение не произошло", "Окошко надежды"); }
        }
        private void btest_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();//Window1 w1 = new Window1();
            //w1.Visibility = Visibility.Hidden;
            //w1.Show();
        }

        private void btest_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                HtmlWeb webinfo = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument docwebinfo = webinfo.Load(link);
                HtmlNode[] pressure = docwebinfo.DocumentNode.SelectNodes(tbtest.Text).ToArray();
                foreach (HtmlNode p in pressure)
                { lbtest.Items.Add(p.InnerText); }
            }
            catch (Exception) { MessageBox.Show("Ошибка", "Не получилось"); }
        }

        /*private void Window_Deactivated(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
                noti.Visible = true;
                noti.Icon = new Icon(@"refresh.ico");
                noti.DoubleClick += notidouble;
                noti.Text = "Погода :З";
                noti.ShowBalloonTip(5000, "Title", "Text", WInForms.ToolTipIcon.Info);
            }
        }*/
        /*void notidouble(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                noti.Visible = false;
                ShowInTaskbar = true;
                WindowState = WindowState.Normal;
                Activate();
            }
        }*/

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveinfo();
            //
            e.Cancel = true;
            WindowState = WindowState.Minimized;
        }
        public void Info()
        {
            try
            {
                i = 0;
                try
                {
                    HtmlWeb webinfo = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument docwebinfo = webinfo.Load(link);
                    HtmlNode[] temperature;
                    temperature = docwebinfo.DocumentNode.SelectNodes("//div [@class='fact__temp-wrap'] //span[@class='temp__value temp__value_with-unit']").ToArray();
                    foreach (HtmlNode t in temperature)
                    { l1.Content = "Текущая температура " + t.InnerText; i = 0; break; }
                    HtmlNode[] wind = docwebinfo.DocumentNode.SelectNodes("//div[@class='term__value'] //span [@class='wind-speed']").ToArray();
                    foreach (HtmlNode w in wind)
                    { l2.Content = "Скорость ветра " + w.InnerText + " м/с"; break; }
                    HtmlNode[] pressure = docwebinfo.DocumentNode.SelectNodes("//div [@class='term__value']").ToArray();
                    foreach (HtmlNode p in pressure)
                    { i++; if (i == 5) { l3.Content = p.InnerText; i = 0; break; } }
                    HtmlNode[] humidity = docwebinfo.DocumentNode.SelectNodes("//div [@class='term__value']").ToArray();
                    foreach (HtmlNode h in humidity)
                    { i++; if (i == 4) { l4.Content = "Влажность " + h.InnerText; i = 0; break; } }
                    // температура днем //div[@class='temp forecast-briefly__temp forecast-briefly__temp_day']
                }
                catch { l1.Content = "Ошибка"; l2.Content = "Ошибка"; }
            }
            catch (Exception) { MessageBox.Show("Данные не получены", "Окошко надежды"); }
        }
    }
}