using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    class RecordsSorter
    {
        List<int> lista = new List<int>();

        internal List<string> SortList(List<string> recordsList)
        {
            foreach (var item in recordsList)
            {
                var lista2 = item.Split(':');
            }
            return new List<string>();
        }

        internal void AddSortedRecord(List<string> recordsList, string newRecord)
        {
            if (recordsList.Count == 0)
            {
                recordsList.Add(newRecord);
            }
            for (int i = 0; i < recordsList.Count; i++)
            {
                int recordTime = GiveTime(recordsList[i]);
                if (recordTime > GiveTime(newRecord))
                {
                    recordsList.Insert(i, newRecord);
                    return;
                }
            }
        }

        private int GiveTime(string record)
        {
            var time = record.Replace(" ", "").Split(':');
            return int.Parse(time[1]);
        }
    }
}
