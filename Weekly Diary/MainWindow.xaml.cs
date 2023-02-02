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

namespace Weekly_Diary
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow : Window
    {
        public static DateTime CreateDate { get; set; } = DateTime.Now;
        private string Path { get { return $"{Environment.CurrentDirectory}\\dataDiary\\data{CreateDate.ToString("yyyy/MM/dd/HH-mm-ss")}.rtf"; } }
        List<string> PathList = new List<string>();
        PWeather weatherP = new PWeather();
        private List<RichTextBox> listDiary=new List<RichTextBox>();
        int pageCount=1;
        SaveLoad saveLoad = new SaveLoad();
        int index = 0;


       


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void UodatePath()
        {
            CreateDate = DateTime.Now;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenWeather open = JsonConvert.DeserializeObject<OpenWeather>(weatherP.Parse());
            imageWeather.Source = open.weather[0].Icon;
            condition.Content = open.weather[0].main;
            conditionWeather.Content = open.weather[0].description;
            temperature.Content = open.main.temp.ToString("0.##") + " °C";
            speedWind.Content = "Wiatr " + open.wind.speed.ToString() + " m/s";
            humidity.Content = "Wilgotność " + open.main.humidity.ToString() + "%";
            pressure.Content = "Nacisk " + ((int)open.main.pressure).ToString() + " mm";
            textDiary =  saveLoad.LoadLastPage(listDiary,index,textDiary,PathList);
            pageCount+=listDiary.Count;
            index = pageCount - 1;
            labelPage.Content =$"Page: {pageCount}";
            textDiary.Focus();
        }

     

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            saveLoad.Save(listDiary, index, textDiary,Path,PathList);            
            textDiary.Document.Blocks.Clear();      
            pageCount++;
            index++;
            labelPage.Content = $"Page: {pageCount}";
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
           
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
            pageCount++;
            index++;
            if (pageCount > listDiary.Count+1)
            {
                pageCount = listDiary.Count+1;
                index = pageCount - 1;
            }
            else
            {
                labelPage.Content = $"Page: {pageCount}";
                textDiary.Document = saveLoad.LoadPage(listDiary, index, PathList).Document;
            }              
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            pageCount--;
            index--;
            if (pageCount < 1)
            {
                pageCount = 1;
                index = 0;
            }
            else
            {
                labelPage.Content = $"Page: {pageCount}";
                textDiary.Document = saveLoad.LoadPage(listDiary, index, PathList).Document;
            }
        }

    }
}

