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
        PWeather weather = new PWeather();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            saveLoad = new SaveLoad(Path);
            try
            {
                modelsDataList = saveLoad.LoadData();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }           
          
           /* Week.ItemsSource = modelsDataList;
            Affairs.ItemsSource = modelsDataList;*/
            modelsDataList.ListChanged += ModelsDataList_ListChanged;
            OpenWeather open = JsonConvert.DeserializeObject<OpenWeather>(weather.Parse());
          /*  BitmapImage d = new BitmapImage();
            d.UriSource = new Uri(open.weathers.name);
            panel1.Source = d;*/
            //condition.Content = open.weathers[0].main;
            conditionWeather.Content = open.weathers.description;
            temperature.Content = open.main.temp.ToString("0.##");
            speedWind.Content = open.wind.speed.ToString();
            humidity.Content = open.main.humidity.ToString();
            pressure.Content =((int)open.main.pressure).ToString();

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

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {

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
