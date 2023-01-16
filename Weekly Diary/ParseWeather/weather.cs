using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_Diary.ParseWeather
{
     class weather
    {

        public int id;
        public string main;
        public string description;
        public static string icon = "01d";
        public string name = $"/{icon}.png";
        public Bitmap Icon
        { 
            get
            {
                return new Bitmap(Image.FromFile($"ImageWeather/{icon}.png"));
            }
        }
    }
}
