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
    public partial class RecordInputView : Form
    {
        int time;
        Action onFormClose;

        public RecordInputView(int time, Action onFormClose)
        {
            InitializeComponent();
            this.time = time;
            this.onFormClose = onFormClose;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imie = textBox1.Text;
            Model.GameRecordHandler grm = new Model.GameRecordHandler();
            grm.SaveRecords(imie, time);
            Close();
            onFormClose();
        }
    }
}
