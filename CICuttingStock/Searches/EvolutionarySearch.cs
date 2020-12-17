using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Searches
{
    public abstract class EvolutionarySearch
    {
        protected Problem problem;


        public EvolutionarySearch(Problem _problem, int generationSize, Initialisations.IInitialisation Initialise)
        {
            problem = _problem;
            List<Solution> pop = Initialise.Initialise(problem.Stock, problem.Orders, generationSize);
            pop.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
            problem.Population = pop;
        }

        public abstract Solution Search(int runTime, Selections.ISelect Selection, CrossOver.ICrossOver CrossOver);
    }
}