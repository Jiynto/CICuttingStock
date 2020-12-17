using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.CrossOver
{
    class TwoPointCrossOver : ICrossOver
    {
        Random randy = new Random();
        public Solution CrossOver(List<Solution> population, Selections.ISelect Selection, Problem problem)
        {
            List<Solution> parents = Selection.Select(population, false, 2);
            Dictionary<Activity, float> ActivityPatternSet = new Dictionary<Activity, float>();
            int[] activityLengths = new int[2];
            for(int i = 0; i < parents.Count; i++)
            {
                activityLengths[i] = parents[i].Activities.Count;
            }

            int indexOne = randy.Next(0, activityLengths.Min());
            int indexTwo = randy.Next(0, activityLengths.Min());
            while( indexTwo == indexOne) indexTwo = randy.Next(0, activityLengths.Min());
            if(indexOne > indexTwo)
            {
                int temp = indexOne;
                indexOne = indexTwo;
                indexTwo = temp;
            }

            for(int i = 0; i < activityLengths.Max(); i++)
            {
                if(i <= indexOne)
                {
                    ActivityPatternSet.Add(parents[0].Activities[i], parents[0].PatternNos[i]);
                }
                else if (indexOne < i && i <= indexTwo)
                {
                    if(ActivityPatternSet.ContainsKey(parents[1].Activities[i]))
                    {
                        ActivityPatternSet[parents[1].Activities[i]]++;
                    }
                    else
                    {
                        ActivityPatternSet.Add(parents[1].Activities[i], parents[1].PatternNos[i]);
                    }
                    
                }
                else if(i > indexTwo)
                {
                    if(parents[0].Activities.ElementAtOrDefault(i) != null)
                    {
                        if (ActivityPatternSet.ContainsKey(parents[0].Activities[i]))
                        {
                            ActivityPatternSet[parents[0].Activities[i]]++;
                        }
                        else
                        {
                            ActivityPatternSet.Add(parents[0].Activities[i], parents[0].PatternNos[i]);
                        }
                            
                    }
                }
            }


            Dictionary<float, float> tempOrder = new Dictionary<float, float>(problem.Orders);
            List<Activity> activitySet = new List<Activity>();
            List<int> patternNos = new List<int>();

            foreach(Activity a in ActivityPatternSet.Keys)
            {
                int pCount = implementationNo(a, tempOrder);
                if(pCount > 0)
                {
                    Activity newAct = a;
                    activitySet.Add(newAct);
                    patternNos.Add(pCount);
                }

            }
            while(tempOrder.Values.Where(x => x > 0).Any())
            {
                Activity a = Components.GenerateActivity.Generate(problem.Stock, new Dictionary<float, float>(tempOrder));
                int pCount = implementationNo(a, tempOrder);
                if (pCount > 0)
                {
                    activitySet.Add(a);
                    patternNos.Add(pCount);
                }
            }

            Solution newSol = new Solution(activitySet, patternNos);
            return newSol;




        }

        private int implementationNo(Activity a, Dictionary<float, float> tempOrder, float limit = Int32.MaxValue)
        {
            int pCount = 0;
            bool done = false;
            while (pCount < limit && !done)
            {

                foreach (float o in a.Orders.Keys)
                {
                    if (tempOrder[o] < 0)
                    {
                        done = true;
                    }
                }
                if (done == false)
                {
                    pCount++;

                    foreach (float o in a.Orders.Keys)
                    {
                        tempOrder[o] -= a.Orders[o];
                    }
                }

            }

            return pCount;


        }


    }
}
