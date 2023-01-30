using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Weekly_Diary.Models;
using Weekly_Diary.Service;
using Weekly_Diary.ParseWeather;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Documents;
using System.IO;

namespace Weekly_Diary
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow : Window
    {
        private readonly string Path = $"{Environment.CurrentDirectory}\\WeeklyDiaryModels.json";
        private BindingList<WeeklyDiaryModel> modelsDataList;
        private SaveLoad saveLoad;
        PWeather weatherP = new PWeather();
        List<RichTextBox> listDiary=new List<RichTextBox>();
        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void SaveRichTextBoxContent()
        {
            TextRange textRange = new TextRange(textDiary.Document.ContentStart, textDiary.Document.ContentEnd);
            using (FileStream fs = File.Create(Path))
            {
                textRange.Save(fs, DataFormats.Rtf);
            }
        }

        private void LoadFromFile(string Path)
        {
            if (File.Exists(Path))
            {
                FileStream fileStream = new FileStream(Path, FileMode.Open);
                TextRange range = new TextRange(textDiary.Document.ContentStart, textDiary.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textDiary.Focus();
           LoadFromFile(Path);
            OpenWeather open = JsonConvert.DeserializeObject<OpenWeather>(weatherP.Parse());                    
            condition.Content = open.weather[0].main;
            conditionWeather.Content = open.weather[0].description;
            temperature.Content = open.main.temp.ToString("0.##")+ " °C";
            speedWind.Content ="Wiatr "+open.wind.speed.ToString()+" m/s";
            humidity.Content = "Wilgotność "+open.main.humidity.ToString()+"%";
            pressure.Content ="Nacisk "+((int)open.main.pressure).ToString()+" mm";
        }

     

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SaveRichTextBoxContent();
            listDiary.Add(textDiary);
            textDiary.Document.Blocks.Clear();
            textDiary = new RichTextBox();

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

        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            textDiary.DataContext = listDiary.Count - 1;
        }

    }
}

