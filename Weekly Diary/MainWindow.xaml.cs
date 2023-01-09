using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Weekly_Diary.Models;
using Weekly_Diary.Service;

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
          
            Week.ItemsSource = modelsDataList;
            modelsDataList.ListChanged += ModelsDataList_ListChanged;
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
    }
}
