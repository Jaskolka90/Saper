using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Saper.Model
{
    class GameRecordHandler
    {
        string fileName = "records.txt";
        string localization = Path.Combine(Environment.CurrentDirectory, "Records");
        RecordsSorter recordsSorter;

        public GameRecordHandler()
        {
            Directory.CreateDirectory(localization);
            this.recordsSorter = new RecordsSorter();
        }
        
        public void SaveRecords(string Name, int Time)
        {
            var recordsList = LoadRecords();
            recordsSorter.AddSortedRecord(recordsList, string.Format("{0}: {1}", Name, Time));
            if (recordsList.Count > 5)
            {
                recordsList.RemoveAt(recordsList.Count - 1);
            }
            StreamWriter file = File.CreateText(Path.Combine(localization, fileName));
            foreach (var item in recordsList)
            {
                file.WriteLine(item);
            }
            file.Close();
        }

        public List<string> LoadRecords()
        {
            FileStream file = File.OpenRead(Path.Combine(localization, fileName));
            StreamReader fileReader = new StreamReader(file);
            List<string> list = new List<string>();
            string line = fileReader.ReadLine();
            while (line != null)
            {
                list.Add(line);
                line = fileReader.ReadLine();
            }
            file.Close();
            fileReader.Close();
            return list;
        }
    }
}
