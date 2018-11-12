using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.View
{
    public class FieldButton : System.Windows.Forms.Button
    {
        public int I { get; private set; }
        public int J { get; private set; }

        public FieldButton(int i, int j)
        {
            this.I = i;
            this.J = j;
        }
    }
}
