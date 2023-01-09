using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_Diary.Models
{
     class WeeklyDiaryModel1:INotifyPropertyChanged
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        private bool _isDone;
        public string _text;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDone
        {
            get { return _isDone; }
            set 
            {
                if(_isDone == value) return;
                _isDone = value; 
                OnPropertyChanged("IsDone");
            }
        }
        public string Text 
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
