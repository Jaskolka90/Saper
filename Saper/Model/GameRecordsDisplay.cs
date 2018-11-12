using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public class GameRecordsDisplay
    {
        GameRecordHandler gameRecordHandler;
        

        public GameRecordsDisplay()
        {
            gameRecordHandler = new GameRecordHandler();
        }

        internal void ShowRecords()
        {
            var recordsList = gameRecordHandler.LoadRecords();
            View.GameRecordsView grv = new View.GameRecordsView(recordsList);
            grv.Show();

        }
    }
}
