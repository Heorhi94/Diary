using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Weekly_Diary.Models
{

    public class WeeklyDiaryModel
    {
        public DateTime CreateDate { get; set; } = DateTime.Now.Date;

        //  public event PropertyChangedEventHandler PropertyChanged;


        private string listDiary;

        public string ListDiary
        {
            get { return listDiary; }
            set { listDiary = value; }
        }

       

    }

}

