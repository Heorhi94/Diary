using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly_Diary.Models;

namespace Weekly_Diary.Service
{
     class SaveLoad
    {
        private readonly string Path;

        public SaveLoad(string path)
        {
            Path = path;
        }

        public BindingList<WeeklyDiaryModel1> LoadData()
        {
            var fileExists = File.Exists(Path);
            if (!fileExists)
            {
                File.CreateText(Path).Dispose();
                return new BindingList<WeeklyDiaryModel1>();
            }
            using (var reader = File.OpenText(Path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<WeeklyDiaryModel1>>(fileText);
            }
        }

        public void SaveData(object modelDataList)
        {
            using(StreamWriter writer = File.CreateText(Path))
            {
                string output = JsonConvert.SerializeObject(modelDataList);
                writer.WriteLine(output);
            }
        }
    }
}
