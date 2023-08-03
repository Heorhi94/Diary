using System;
using System.Windows;
using System.Windows.Controls;
using Weekly_Diary.Service;
using Weekly_Diary.ParseWeather;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Input;

namespace Weekly_Diary
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow : Window
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        string Path { get { return $"{Environment.CurrentDirectory}\\dataDiary\\data{CreateDate:yyyy/MM/dd/HH-mm-ss}.rtf"; } }

        readonly List<string> PathList = new List<string>();
        readonly PWeather weatherP = new PWeather();
        private readonly List<RichTextBox> listDiary=new List<RichTextBox>();
        int pageCount=1;
        readonly SaveLoad saveLoad = new SaveLoad();
        bool istools = false;


       
        public MainWindow()
        {
            InitializeComponent();
            pTools.Visibility = Visibility.Hidden;
        }

        private void LoadPage()
        {
            btnDel.Visibility = Visibility.Collapsed;
            bEdit.Visibility = Visibility.Collapsed;
            try
            {
                OpenWeather open = JsonConvert.DeserializeObject<OpenWeather>(weatherP.Parse());
                imageWeather.Source = open.weather[0].Icon;
                condition.Content = open.weather[0].main;
                conditionWeather.Content = open.weather[0].description;
                temperature.Content = open.main.temp.ToString("0.##") + " °C";
                speedWind.Content = "Wiatr " + open.wind.speed.ToString() + " m/s";
                humidity.Content = "Wilgotność " + open.main.humidity.ToString() + "%";
                pressure.Content = "Nacisk " + ((int)open.main.pressure).ToString() + " mm";
            }
            catch
            {
                imageWeather.Source = null;
                condition.Content = null;
                conditionWeather.Content = null;
                temperature.Content = null;
                speedWind.Content = null;
                humidity.Content = null;
                pressure.Content = null;
            }

            DateTime mainDate = DateTime.Now;
            date.Content = mainDate.ToString("yyyy/MM/dd");
            textDiary = saveLoad.LoadLastPage(listDiary, pageCount - 1, textDiary, PathList);
            pageCount = listDiary.Count + 1;
            labelPage.Content = $"Page: {pageCount}";
            textDiary.Document.Blocks.Clear();
            textDiary.Focus();
        }

        private void AddPage()
        {
            UpdatePath();
            saveLoad.Save(listDiary, textDiary, Path, PathList);
            textDiary.Document.Blocks.Clear();
            pageCount++;
            labelPage.Content = $"Page: {pageCount}";
            textDiary.Focus();
        }
        private void DeletePage()
        {
            string[] files = Directory.GetFiles($"{Environment.CurrentDirectory}\\dataDiary", "*.rtf");
            File.Delete(files[pageCount - 1]);
            listDiary.Clear();
            PathList.Clear();
            pageCount = 1;
            textDiary = saveLoad.LoadLastPage(listDiary, pageCount - 1, textDiary, PathList);
            textDiary.Document.Blocks.Clear();
            pageCount = listDiary.Count + 1;
            labelPage.Content = $"Page: {pageCount}";
            textDiary.Focus();
            textDiary.IsReadOnly = true;
            NextPage();
            if(pageCount == 1)
            {
                bEdit.Visibility = Visibility.Collapsed;
            }
        }
        private void NextPage()
        {
            pageCount++;
            if (pageCount > listDiary.Count)
            {
                pageCount = listDiary.Count + 1;
                textDiary.Document.Blocks.Clear();
                labelPage.Content = $"Page: {pageCount}";
                btnDel.Visibility = Visibility.Collapsed;
                bEdit.Visibility = Visibility.Collapsed;
                textDiary.IsReadOnly = false;
            }
            else
            {
                VisibilityTools();
                labelPage.Content = $"Page: {pageCount}";
                textDiary.Document = saveLoad.LoadPage(listDiary, pageCount - 1, PathList).Document;
                textDiary.IsReadOnly = true;
            }
            textDiary.Focus();
        }

        private void BackPage()
        {
            VisibilityTools();
            pageCount--;
            if (pageCount < 1)
            {
                pageCount = 1;
            }
            else
            {
                labelPage.Content = $"Page: {pageCount}";
                textDiary.Document = saveLoad.LoadPage(listDiary, pageCount - 1, PathList).Document;
            }
            textDiary.Focus();
            textDiary.IsReadOnly = true;
        }

        private void OkPage()
        {
            saveLoad.EditPage(listDiary, pageCount - 1, PathList);
            listDiary.RemoveAt(pageCount);
            textDiary.IsReadOnly = true;
            btnOk.Visibility = Visibility.Collapsed;
            bEdit.Visibility = Visibility.Visible;
        }

        private void Tools()
        {
            if (istools)
            {
                istools = false;
                pTools.Visibility = Visibility.Hidden;
            }
            else
            {
                istools = true;
                pTools.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 0,
                    To = pTools.ActualWidth,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };
                pTools.BeginAnimation(StackPanel.WidthProperty, animation);
            }
        }
        private void VisibilityTools()
        {
            btnDel.Visibility = Visibility.Visible;
            bEdit.Visibility = Visibility.Visible;
          
        }
        public void UpdatePath()
        {
            CreateDate = DateTime.Now;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          LoadPage();          
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DirectoryInfo dirInfo = new DirectoryInfo($"{Environment.CurrentDirectory}\\img\\");

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
           AddPage();
        }
        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
             DeletePage();
        }
        private void BtnVoice_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnDraw_Click(object sender, RoutedEventArgs e)
        {
            DrawWindow drawWindow = new DrawWindow(this);
            drawWindow.Show(); 
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
           OkPage();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            BackPage();
        }

        private void BTools_Click(object sender, RoutedEventArgs e)
        {
            Tools();
        }
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            textDiary.IsReadOnly = false;
            bEdit.Visibility = Visibility.Collapsed;
            btnOk.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

