using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Diagnostics;

namespace CICuttingStock.Initialisations
{
    public class RandomInitialisation : IInitialisation
    {
        private Random randy = new Random();

        public List<Solution> Initialise(List<Stock> stock, Dictionary<float, float> orders, int generationSize)
        {

            List<Solution> population = new List<Solution>();


            while(population.Count <= generationSize)
            {
                List<Activity> activitySet = new List<Activity>();
                List<int> patternNos = new List<int>();
                Dictionary<float, float> tempOrder = new Dictionary<float, float>(orders);
                while (tempOrder.Count != 0)
                {
                    Activity newAct = GenerateActivity(stock, tempOrder);
                    int count = 0;
                    bool done = false;
                    while(!done)
                    {
                        if (ActivityValidation(newAct, tempOrder))
                        {
                            foreach (float o in newAct.Orders.Keys)
                            {
                                tempOrder[o] -= newAct.Orders[o];
                                if (tempOrder[o] <= 0) tempOrder.Remove(o);
                            }
                            count++;
                        }
                        else
                        {
                            done = true;
                        }
                        
                    }
                    if (count > 0)
                    {
                        activitySet.Add(newAct);
                        patternNos.Add(count);
                    }
                }

                Solution newSol = new Solution(activitySet, patternNos);
                population.Add(newSol);
                

            }

            return population;
        }

        private bool ActivityValidation(Activity act, Dictionary<float, float> order)
        {
            foreach(float cut in act.Orders.Keys)
            {
                if(!order.ContainsKey(cut))
                {
                    return false;
                }

            }

            return true;
        
        }



        private Activity GenerateActivity(List<Stock> stock, Dictionary<float, float> orders)
        {
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

        /*
        private void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;

            while (count > 1)
            {
                count--;
                int i = randy.Next(count + 1);
                T value = list[i];
                list[i] = list[count];
                list[count] = value;
            }
        }
        */

    }
}
