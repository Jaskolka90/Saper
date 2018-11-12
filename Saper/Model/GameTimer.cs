using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper.Model
{
    class GameTimer
    {
        int time;
        Timer timer;
        Label timeLabel;

        public GameTimer()
        {
            time = 0;
            timer = new Timer();
            timer.Tick += UpdateTime;
            timer.Interval = 1000;
        }

        public void ResetTimer()
        {
            time = 0;
            timeLabel.Text = "Czas: " + time;
        }

        public void SetLabel(Label l)
        {
            timeLabel = l;
            timeLabel.Text = "Czas: " + time;
        }

        public bool IsRunning()
        {
            return time > 0; 
        } 

        public void StartTime()
        {
             timer.Start();
        }

        public void StopTime()
        {
            timer.Stop();
        }

        public int GetTime()
        {
            return time;
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            time++;
            timeLabel.Text = "Czas: " + time;
        }

        


    }
}
