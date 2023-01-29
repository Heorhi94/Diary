﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Weekly_Diary.Models
{
     
     class WeeklyDiaryModel1:INotifyPropertyChanged
    {
        public DateTime CreateDate { get; set; } = DateTime.Now.Date;

        private List<RichTextBox> _text;

        public event PropertyChangedEventHandler PropertyChanged;

     

        public List<RichTextBox> Text 
        {
            get { return _text; }
            set 
            {
                if(_text == value) return;
                _text = value; 
                OnPropertyChanged("Text");
            } 
        }

        protected virtual void OnPropertyChanged(string propertyName="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
