using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Selections
{
    class TournamentSelection : ISelect
    {
        Random randy = new Random();
        public List<Solution> Select(List<Solution> _population, bool asexual, int parentNo)
        {
            List<Solution> population = new List<Solution>(_population);
            List<Solution> Parents = new List<Solution>();
            float tournamentSize = population.Count * 0.1f;
            while (Parents.Count < parentNo)
            {
                List<Solution> tournament = new List<Solution>();
                while(tournament.Count < tournamentSize)
                {
                    Solution currentParent = population[randy.Next(0, population.Count)];
                    tournament.Add(currentParent);

                }
                tournament.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
                Solution newParent = tournament.First();
                if (!asexual)
                {
                    population.Remove(newParent);
                }
                Parents.Add(newParent);
            }
            return Parents;
        }

    }
}
