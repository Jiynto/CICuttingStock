using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Components
{
    public static class GenerateActivity
    {
        static Random randy = new Random();

        public static Activity Generate(List<Stock> stock, Dictionary<float, float> orders)
        {
            List<float> keys = orders.Keys.ToList();
            foreach (float i in keys)
            {
                if (orders[i] <= 0)
                {
                    orders.Remove(i);
                }
            }

            Stock currentStock = stock[randy.Next(0, stock.Count())];
            float shortestOrder = orders.Keys.Min();
            float activityLength = 0;
            List<float> activityOrders = new List<float>();
            bool full = false;
            while (full == false)
            {
                float currentOrder = orders.Keys.ElementAt(randy.Next(0, orders.Keys.Count()));
                while (activityLength + currentOrder > currentStock.Length) currentOrder = orders.Keys.ElementAt(randy.Next(0, orders.Keys.Count()));
                activityLength += currentOrder;
                activityOrders.Add(currentOrder);
                if (currentStock.Length - activityLength < shortestOrder) full = true;
            }

            Activity newActivity = new Activity(activityOrders, currentStock);
            return newActivity;

        }

    }
}
