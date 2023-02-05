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
        DateTime CreateDate { get; set; } = DateTime.Now;
        string Path { get { return $"{Environment.CurrentDirectory}\\dataDiary\\data{CreateDate.ToString("yyyy/MM/dd/HH-mm-ss")}.rtf"; } }
        List<string> PathList = new List<string>();
        PWeather weatherP = new PWeather();
        private List<RichTextBox> listDiary=new List<RichTextBox>();
        int pageCount=1;
        SaveLoad saveLoad = new SaveLoad();
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
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = pTools.ActualWidth;
                animation.Duration = new Duration(TimeSpan.FromSeconds(1));

                pTools.BeginAnimation(StackPanel.WidthProperty, animation);
            }
        }
        private void VisibilityTools()
        {
            btnDel.Visibility = Visibility.Visible;
            bEdit.Visibility = Visibility.Visible;
          
        }
        private void UpdatePath()
        {
            CreateDate = DateTime.Now;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          LoadPage();          
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           AddPage();
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
             DeletePage();
        }
        private void btnVoice_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            DrawWindow drawWindow = new DrawWindow(this);
            drawWindow.Show(); 
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
           OkPage();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackPage();
        }

        private void bTools_Click(object sender, RoutedEventArgs e)
        {
            Tools();
        }
        private void bEdit_Click(object sender, RoutedEventArgs e)
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

