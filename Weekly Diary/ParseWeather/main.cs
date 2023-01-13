using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_Diary.ParseWeather
{
    internal class main
    {
        public double humidity;
        private double _temp_min
        {
            get { return _temp_min; }
            set { _temp_min = value - 273.15; } //gradusy Celsia
        }
        private double _temp_max
        {
            get { return _temp_max; }
            set { _temp_max = value - 273.15; }// gradusy Celsia
        }
    }
}
