using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CICuttingStock
{
    public class Solution
    {

        private List<Activity> activities;
        public List<Activity> Activities { get { return activities; } }

        private float totalCost;
        public float TotalCost { get { return totalCost; } }

        private List<int> patternNos;
        public List<int> PatternNos { get { return patternNos; } }

        private float fitness;

        public float Fitness { get { return fitness; } }

        public Solution(List<Activity> _activities, List<int> _patternNos)
        {
            activities = _activities;
            patternNos = _patternNos;
            float cost = 0;
            for(int i = 0; i < activities.Count; i++)
            {
                cost += (activities[i].Stock.Cost*patternNos[i]);
            }
            totalCost = cost;
            fitness = 1/(new Vector2(totalCost, patternNos.Count).Length());

        }


        public override string ToString()
        {
            string returnString = null;

            for(int i = 0; i < activities.Count; i++)
            {
                returnString += activities[i].ToString();
                returnString += "number of implementations:" + patternNos[i] + ";\n";
            }
            returnString += "total cost:" + totalCost + ";\n";
            returnString += "Fitness:" + fitness + ";\n";
            return returnString;

        }



    }
}
