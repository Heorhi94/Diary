using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        public void Save(List<RichTextBox> listDiary, string Path)
        {
            var serializer = new JsonSerializer();
            using (var file = File.CreateText(Path))
            {
                serializer.Formatting = Formatting.Indented;

                serializer.Serialize(file, listDiary);
            }
           
        }

      /*  public BindingList<WeeklyDiaryModel1> Load(List<RichTextBox> listDiary, string Path)
        {
            
            var serializer = new JsonSerializer();
            var screens = JsonConvert.DeserializeObject< List < RichTextBox >> (File.ReadAllText(Path));

            listDiary.Clear();
            foreach (var screen in screens)
            {
                listDiary.Add(screen);
            }
            

        }*/
         public BindingList<WeeklyDiaryModel> LoadData()
         {
             var fileExists = File.Exists(Path);
             if (!fileExists)
             {
                 File.CreateText(Path).Dispose();
                 return new BindingList<WeeklyDiaryModel>();
             }
             using (var reader = File.OpenText(Path))
             {
                 var fileText = reader.ReadToEnd();
                 return JsonConvert.DeserializeObject<BindingList<WeeklyDiaryModel>>(fileText);
             }
         }

         public void SaveData(BindingList<WeeklyDiaryModel>models)
         {
             using(StreamWriter writer = File.CreateText(Path))
             {
                 string output = JsonConvert.SerializeObject(models);
                 writer.WriteLine(output);
             }
         }
    }
}
