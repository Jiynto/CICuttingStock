using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Selections
{
    public class RouletteWheelSelection : ISelect
    {
        private Random randy = new Random();

        public List<Solution> Select(List<Solution> _population, bool asexual, int parentNo)
        {
            List<Solution> population = new List<Solution>(_population);
            List<Solution> Parents = new List<Solution>();
            while(Parents.Count < parentNo)
            {
                Solution currentParent = Roulette(GenerateDistribution(population));
                if(!asexual)
                {
                    population.Remove(currentParent);
                }
                Parents.Add(currentParent);
            }
            return Parents;
        }

        private ValueTuple<float, float[], List<Solution>> GenerateDistribution(List<Solution> population)
        {
            float totalFitness = 0;
            //population.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
            float[] cumulativeFitness = new float[population.Count];
            for (int i = 0; i < population.Count; i++)
            {
                totalFitness += population[i].Fitness;
                cumulativeFitness[i] = totalFitness;
            }

            return (totalFitness, cumulativeFitness, population);
        }


        private Solution Roulette(ValueTuple<float, float[], List<Solution>> distribution)
        {
            Solution result;
            float totalFitness = distribution.Item1;
            float[] cumulativeFitness = distribution.Item2;
            List<Solution> population = distribution.Item3;
            float spin = (float)randy.NextDouble() * (totalFitness);
            for(int i = 0; i < cumulativeFitness.Length; i++)
            {
                if(cumulativeFitness[i] >= spin)
                {
                    result = population[i];
                    return result;
                }
            }

            return null;

        }



    }
}
