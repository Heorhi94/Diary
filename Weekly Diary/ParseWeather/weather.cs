using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_Diary.ParseWeather
{
    internal class weather
    {
        public int id;
        public string main;
        public string discrition;
        public string icon;
        public Bitmap Icon
        { 
            get
            {
                return new Bitmap(Image.FromFile($"ImageWeather/{icon}.png"));
            }
        }
    }
}
