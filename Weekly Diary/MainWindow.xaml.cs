using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Weekly_Diary.Models;
using Weekly_Diary.Service;
using Weekly_Diary.ParseWeather;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Image = System.Windows.Controls.Image;
using System.Windows.Documents;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;

namespace Weekly_Diary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow : Window
    {
        private readonly string Path = $"{Environment.CurrentDirectory}\\WeeklyDiaryModels.json";
        private BindingList<WeeklyDiaryModel1> modelsDataList;
        private SaveLoad saveLoad;
        PWeather weatherP = new PWeather();
        public static List<RichTextBox> listDiary = new List<RichTextBox>();  


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textDiary.Focus();
            saveLoad = new SaveLoad(Path);
            try
            {
                modelsDataList = saveLoad.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            
        //   Week.ItemsSource = modelsDataList;
        //   Affairs.ItemsSource = modelsDataList;
            modelsDataList.ListChanged += ModelsDataList_ListChanged;
            OpenWeather open = JsonConvert.DeserializeObject<OpenWeather>(weatherP.Parse());                    
            condition.Content = open.weather[0].main;
            conditionWeather.Content = open.weather[0].description;
            temperature.Content = open.main.temp.ToString("0.##")+ " °C";
            speedWind.Content ="Wiatr "+open.wind.speed.ToString()+" m/s";
            humidity.Content = "Wilgotność "+open.main.humidity.ToString()+"%";
            pressure.Content ="Nacisk "+((int)open.main.pressure).ToString()+" mm";
        }

        private void ModelsDataList_ListChanged(object sender, ListChangedEventArgs e)
            {
            if(e.ListChangedType==ListChangedType.ItemAdded || e.ListChangedType==ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                     saveLoad.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Week_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
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
            
        }

    }
}

