using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_Diary.ParseWeather
{
    internal class OpenWeather
    {
        [JsonProperty("base")]
        public string Base;
        public double visibility;
        public double dt;
        public int id;
        public string name;
        public double cod;
    }
}
