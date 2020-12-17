using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Selections
{
    class RandomSelection : ISelect
    {
        private Random randy = new Random();
        public List<Solution> Select(List<Solution> _population, bool asexual, int parentNo)
        {
            List<Solution> population = new List<Solution>(_population);
            List<Solution> Parents = new List<Solution>();
            while (Parents.Count < parentNo)
            {
                Solution currentParent = population[randy.Next(0, population.Count)];
                if (!asexual)
                {
                    population.Remove(currentParent);
                }
                Parents.Add(currentParent);
            }
            return Parents;
        }
    }
}
