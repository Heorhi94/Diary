using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace Weekly_Diary.Service
{
    class SaveLoad 
    {
        public void Save(List<RichTextBox> listDiary, RichTextBox textDiary,string Path,List<string>PathList)
        {
        TextRange doc = new TextRange(textDiary.Document.ContentStart, textDiary.Document.ContentEnd);
        using (FileStream fs = File.Create(Path))
        {                  
            doc.Save(fs, DataFormats.Rtf);
        }
        listDiary.Add(textDiary);
         PathList.Add(Path);    
        }

        public RichTextBox LoadLastPage(List<RichTextBox> listDiary, int index,RichTextBox textDiary,List<string>PathList)
        {
            string[] files = Directory.GetFiles($"{Environment.CurrentDirectory}\\dataDiary", "*.rtf");
            string Path;
            int count = files.Length;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    Path = files[i];
                    PathList.Add(Path);
                    TextRange doc = new TextRange(textDiary.Document.ContentStart, textDiary.Document.ContentEnd);
                    using (FileStream fs = new FileStream(Path, FileMode.Open))
                    {
                        doc.Load(fs, DataFormats.Rtf);
                    }
                    listDiary.Add(textDiary);
                }
                return listDiary[index];
            }
            catch
            {
               return textDiary;
            }
           
        }

        public RichTextBox LoadPage(List<RichTextBox> listDiary, int index, List<string>PathList)
        {
            TextRange doc = new TextRange(listDiary[index].Document.ContentStart, listDiary[index].Document.ContentEnd);
            using (FileStream fs = new FileStream(PathList[index], FileMode.Open))
            {
                doc.Load(fs, DataFormats.Rtf);
            }      
            return listDiary[index];
        }

        public void EditPage(List<RichTextBox>listDiary, int index, List<string>PathList)
        {
            TextRange doc = new TextRange(listDiary[index].Document.ContentStart, listDiary[index].Document.ContentEnd);
            using (FileStream fs = new FileStream(PathList[index], FileMode.Create))
            {
                doc.Save(fs, DataFormats.Rtf);
            } 
            listDiary.Insert(index,listDiary[index]);
        }
    }
}
