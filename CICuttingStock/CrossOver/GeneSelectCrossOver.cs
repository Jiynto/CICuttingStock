using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.CrossOver
{
    public class GeneSelectCrossOver : ICrossOver
    {
        private Random randy;
        public Solution CrossOver(List<Solution> population, Selections.ISelect Selection, Problem problem)
        {
            List<Activity> activitySet = new List<Activity>();
            List<int> patternNos = new List<int>();
            randy = new Random();
            Dictionary<float, float> tempOrder = new Dictionary<float, float>(problem.Orders);
            int sum = 0;
            foreach(Solution s in population)
            {
                sum += s.Activities.Count;
            }
            int meanActivities = sum / population.Count;

            int g = meanActivities;

            while (tempOrder.Values.Where(x => x > 0).Any())
            {

                Activity newAct;
                int numImplementations = 0;
                if (g > 0)
                {
                    Solution parent = Selection.Select(population, true, 1).First();
                    newAct = parent.Activities[randy.Next(0, parent.Activities.Count)];
                    numImplementations = CalculateNoImplamentations(newAct, problem, tempOrder);
                    g--;
                }
                else
                {
                    newAct = Components.GenerateActivity.Generate(problem.Stock, new Dictionary<float, float>(tempOrder));
                    numImplementations = CalculateNoImplamentations(newAct, problem, tempOrder);
                }


                if(numImplementations > 0)
                {
                    foreach (float o in newAct.Orders.Keys)
                    {
                        tempOrder[o] -= newAct.Orders[o] * numImplementations;
                    }
                    activitySet.Add(newAct);
                    patternNos.Add(numImplementations);
                }
                

               
            }
            Solution newSol = new Solution(activitySet, patternNos);
            return newSol;
        }

        private int CalculateNoImplamentations(Activity newAct, Problem problem, Dictionary<float, float> tempOrder)
        {
            float firstDemand = newAct.Orders.Keys.First();
            float lastDemand = newAct.Orders.Keys.First();
            float first = 0;
            float last = 0;
            float maxOverProduction = problem.Stock.Min(x => x.Length);
            int numImplementations;
            foreach (float o in newAct.Orders.Keys)
            {
                if (tempOrder[firstDemand] > tempOrder[o])
                {
                    firstDemand = o;
                }
                if (tempOrder[lastDemand] < tempOrder[o])
                {
                    lastDemand = o;
                }
            }
            if (tempOrder[firstDemand] > 0)
            {
                first = tempOrder[firstDemand];
                last = tempOrder[lastDemand];
                if (firstDemand * (last - first) > maxOverProduction)
                {
                    numImplementations = (int)tempOrder[firstDemand];
                }
                else
                {
                    numImplementations = (int)tempOrder[lastDemand];
                }
            }
            else
            {
                first = tempOrder[firstDemand];
                numImplementations = 0;
            }

            return numImplementations;

        }

        /*
        private Activity GenerateActivity(List<Stock> stock, Dictionary<float, float> orders)
        {
            List<float> keys = orders.Keys.ToList();
            foreach(float i in keys)
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
        */





    }
}
