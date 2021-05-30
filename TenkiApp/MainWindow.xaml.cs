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
        //App.PogodaContext db = new App.PogodaContext();
        static string save = "saveinfo.txt", savedata = "", link = ""; string[] c = new string[70]; int i = 0;
        //FileInfo f = new FileInfo(save);
        StreamReader str = new StreamReader(save);
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*try
            {
                cb1.Items.Clear();
                using (var db = new App.PogodaContext())
                {
                    /*
                     * var persons = from per in loginconnect.persons
                                  join co in loginconnect.countrys on per.id_country equals co.id
                                  orderby per.id
                                  join pr in loginconnect.professions on per.id_prof equals pr.id
                                  join ma in loginconnect.machines on per.id_m equals ma.id into mash
                                  from submash in mash.DefaultIfEmpty()
                                  select new
                                  {
                                      id = per.id,
                                      name = per.name,
                                      familia = per.familia,
                                      otchestvo = per.otchestvo,
                                      bday = per.bday,
                                      gender = per.gender,
                                      inn = per.inn,
                                      id_country = co.country,
                                      telefon = per.telefon,
                                      id_prof = pr.name_prof,
                                      id_m = submash.machine,
                                      date_in = per.date_in,
                                      salary = per.salary
                                  };
                    
                    var c1 = db.countrys.ToList();
                    foreach (var countr in c1)
                    { cb1.Items.Add(countr.name); }

                }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message + " | Ошибка на этапе запуска, пожалуйста, передайте это разработчику", "Окошко надежды"); }
*/
            try
            {
                using (var db = new App.PogodaContext())
                {
                    var c1 = from element in db.countrys select element.name;
                    foreach (var countr in c1)
                    { cb1.Items.Add(countr); }
                }

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
                /*try
                {
                using (SQLiteConnection Connect = new SQLiteConnection("Data Source = MyProject.sqlite;"))
                {
                    Connect.Open();
                    SQLiteCommand command = new SQLiteCommand(@"SELECT name FROM countrys", Connect);
                    SQLiteDataReader dann = command.ExecuteReader();
                    while (dann.Read())
                    {
                        object d = dann[0];
                        cb1.Items.Add(d);
                    }
                    Connect.Close();
                }



                 }
                 catch (Exception exp) { MessageBox.Show(exp.Message + " | Ошибка на этапе запуска, пожалуйста, передайте это разработчику", "Окошко надежды"); }
            */
                if (cb1.Text != "")
                    cb2.Visibility = Visibility.Visible;
                if (cb2.Text == "")
                    cb2.Visibility = Visibility.Hidden;
                if (cb3.Text == "")
                    cb3.Visibility = Visibility.Hidden;
            }
            catch (Exception exp) { MessageBox.Show(exp.Message + " | Ошибка на этапе запуска, пожалуйста, передайте это разработчику", "Окошко надежды"); }
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cb2.Items.Clear();
                cb2.Visibility = Visibility.Visible;
                cb3.Visibility = Visibility.Hidden;
                using (var db = new App.PogodaContext())
                {
                    var area = from element in db.areas
                               where element.id_country == (cb1.SelectedIndex + 1)
                               select element.name;
                    foreach (var countr in area)
                    { cb2.Items.Add(countr); }
                }

            }
            catch (Exception) { MessageBox.Show("Ошибка на первом этапе, пожалуйста, передайте это разработчику", "Окошко надежды"); }
            /*try
            {
                cb2.Items.Clear(); cb3.Items.Clear();
                cb2.Visibility = Visibility.Visible;
                cb3.Visibility = Visibility.Hidden;
                //
                using (SQLiteConnection Connect = new SQLiteConnection("Data Source = MyProject.sqlite;"))
                {
                    Connect.Open();
                    SQLiteCommand command = new SQLiteCommand(@"SELECT name FROM areas WHERE id_country ='" + (cb1.SelectedIndex + 1).ToString() + "';", Connect);
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
*/
        }
        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*try
            {
                cb3.Items.Clear();
                using(App.PogodaContext ddb = new App.PogodaContext())
                {
                    //var arealink =  
                }
                if (cb3.Items.Count != 0) cb3.Visibility = Visibility.Visible;
                Info();
            }
            catch (Exception) { MessageBox.Show("Ошибка на втором этапе, пожалуйста, передайте это разработчику", "Окошко надежды"); }
*/

        
            /*try
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
            catch (Exception) { MessageBox.Show("Ошибка на втором этапе, пожалуйста, передайте это разработчику", "Окошко надежды"); } */       }
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
            lbtest.Items.Clear();
            try
            {
                HtmlWeb webinfo = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument docwebinfo = webinfo.Load("https://yandex.ru/pogoda/region?via=brd");
                HtmlNode[] pressure = docwebinfo.DocumentNode.SelectNodes(tbtest.Text).ToArray();
                foreach (HtmlNode p in pressure)
                { lbtest.Items.Add(p.InnerText); }
            }
            catch (Exception exp) { MessageBox.Show("Ошибка" + exp.Message, "Не получилось"); }
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

        private void addbd_Click(object sender, RoutedEventArgs e)
        {
            adddatabase();
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
        public async void adddatabase()
        {
            try
            {
                bool citys = true;
                int i = 0, j = 0, g = 0;
                string com = "https://yandex.ru/pogoda/region?via=brd";
                string tegcountryarea = "//li[@class='place-list__item place-list__item_region_yes']//a";
                //string tegcountryarea = "//span[@itemprop='name']";
                ////li[@class='place-list__item place-list__item_region_yes']
                App.PogodaContext db = new App.PogodaContext();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                HtmlWeb webcountry = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doccountry = webcountry.Load(com);
                HtmlNode[] nodescountry = doccountry.DocumentNode.SelectNodes(tegcountryarea).ToArray();
                foreach (HtmlNode itemcountry in nodescountry)
                {
                    await db.countrys.AddRangeAsync(new App.Country { id = (i + 1), name = itemcountry.InnerText, link = "https://yandex.ru" + itemcountry.GetAttributeValue("href", "") });
                    await db.SaveChangesAsync();
                    HtmlAgilityPack.HtmlDocument docarea = new HtmlWeb().Load("https://yandex.ru" + itemcountry.GetAttributeValue("href", ""));//получение ссылки
                    HtmlNode[] nodesarea = docarea.DocumentNode.SelectNodes(tegcountryarea).ToArray();
                    foreach (HtmlNode item in nodesarea)
                    {
                        await db.areas.AddRangeAsync(new App.Area { id = (j + 1), name = item.InnerText, link = "https://yandex.ru" + item.GetAttributeValue("href", ""), id_country = (i + 1) });
                        await db.SaveChangesAsync();
                        HtmlNode[] nodescity = null;
                        if (citys == true)
                        {
                            try
                            {
                                HtmlAgilityPack.HtmlDocument doccity = new HtmlWeb().Load("https://yandex.ru" + item.GetAttributeValue("href", ""));
                                nodescity = doccity.DocumentNode.SelectNodes(tegcountryarea).ToArray();
                                foreach (HtmlNode city in nodescity)
                                {
                                    await db.citys.AddRangeAsync(new App.City { id_area = (j + 1), id = (g + 1), name = city.InnerText, link = "https://yandex.ru" + city.GetAttributeValue("href", "") });
                                    await db.SaveChangesAsync(); g++;
                                }
                            }
                            catch (ArgumentNullException) { }
                        }
                        /*g = 0*/; j++; if (nodescity == null) citys = false;
                    }
                    /*j = 0;*/ i++; citys = true;

                }
            }
            catch(Exception exp) { MessageBox.Show(exp.Message+"","Информация"); }
        }
    }
}

/*
 HtmlWeb webcountry = new HtmlWeb();

HtmlAgilityPack.HtmlDocument doccountry = webcountry.Load(c);

HtmlNode[] nodescountry = doccountry.DocumentNode.SelectNodes(tegcountryarea).ToArray();

foreach (HtmlNode itemcountry in nodescountry)

{

await db.BDPogodas.AddRangeAsync(new BDPogoda { countryid = (i + 1).ToString(), countryname = itemcountry.InnerText, countrylink = "https://yandex.ru" + itemcountry.GetAttributeValue("href", "") });

await db.SaveChangesAsync();

HtmlAgilityPack.HtmlDocument docarea = new HtmlWeb().Load("https://yandex.ru" + itemcountry.GetAttributeValue("href", ""));//получение ссылки

HtmlNode[] nodesarea = docarea.DocumentNode.SelectNodes(tegcountryarea).ToArray();

foreach (HtmlNode item in nodesarea)

{

await db.BDPogodas.AddRangeAsync(new BDPogoda { countryid = (i + 1).ToString(), areaid = (j + 1).ToString(), areaname = item.InnerText, arealink = "https://yandex.ru" + item.GetAttributeValue("href", "") });

await db.SaveChangesAsync();

HtmlNode[] nodescity = null;

if (citys == true)

{

try

{

HtmlAgilityPack.HtmlDocument doccity = new HtmlWeb().Load("https://yandex.ru" + item.GetAttributeValue("href", ""));

nodescity = doccity.DocumentNode.SelectNodes(tegcountryarea).ToArray();

foreach (HtmlNode city in nodescity)

{

await db.BDPogodas.AddRangeAsync(new BDPogoda { countryid = (i + 1).ToString(), areaid = (j + 1).ToString(), cityid = (g + 1).ToString(), cityname = city.InnerText, citylink = "https://yandex.ru" + city.GetAttributeValue("href", "") });

await db.SaveChangesAsync(); g++;

}

}

catch (ArgumentNullException) { }

}

g = 0; j++; if (nodescity == null) citys = false;

}

j = 0; i++; citys = true;

}

}
*/