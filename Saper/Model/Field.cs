using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public class Field
    {
        public bool HasBomb { get; private set; }
        public FieldType FType { get; set; }
        public int BombsAround { get; set; }


        public Field(bool hasBomb)
        {
            this.HasBomb = hasBomb;
            this.FType = FieldType.Hidden;
        }

       
    }
}