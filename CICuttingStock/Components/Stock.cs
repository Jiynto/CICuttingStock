using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock
{
    public class Stock
    {
        private readonly float length;
        private readonly float cost;

        public float Length { get { return length; } }
        public float Cost { get { return cost; }  }

        public Stock(float _length, float _cost)
        {
            length = _length;
            cost = _cost;
        }




    }
}
