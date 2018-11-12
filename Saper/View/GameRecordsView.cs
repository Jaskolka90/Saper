using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper.View
{
    public partial class GameRecordsView : Form
    {
        short margin = 20;
        short offsetY = 20;
        short labelSizeX = 100;
        short labelSizeY = 20;

        public GameRecordsView(List<string> records)
        {
            InitializeComponent();
            CreateLabels(records);
        }

        private void CreateLabels(List<string> records)
        {
            for (int i = 0; i < records.Count; i++)
            {
                Label l = new Label();
                l.SetBounds(margin, margin+ offsetY*i, labelSizeX, labelSizeY);
                l.Text = i + 1 + ". "+ records[i];
                Controls.Add(l);
            }
        }
        
    }
}
