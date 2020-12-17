using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock
{
    public class Activity
    {
        private Dictionary<float, int> orders;
        public Dictionary<float, int> Orders { get { return orders; } }
        private Stock stock;
        public Stock Stock { get { return stock; }  }


        
        public Activity(List<float> _orders, Stock _stock)
        {
            orders = new Dictionary<float, int>();
            foreach(float o in _orders)
            {
                if(orders.ContainsKey(o))
                {
                    orders[o]++;
                }
                else
                {
                    orders.Add(o, 1);
                }
            }
            stock = _stock;

        }

        public override string ToString()
        {
            string returnString = null;
            returnString += "Stock Length:" + stock.Length + "; Stock Cost:" + stock.Cost + "; ";
            foreach(float o in orders.Keys)
            {
                returnString += o + ": " + orders[o] + ";";
            }

            return returnString;
        }


    }
}
