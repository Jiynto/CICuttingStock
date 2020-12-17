using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CICuttingStock.Searches
{
    public class ESFullSet : EvolutionarySearch
    {
        public ESFullSet(Problem _problem, int generationSize, Initialisations.IInitialisation Initialise) : base(_problem, generationSize, Initialise)
        { }

        public override Solution Search(int runtime, Selections.ISelect Selection, CrossOver.ICrossOver CrossOver)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();

            while(sp.ElapsedMilliseconds < runtime)
            {
                Solution child = CrossOver.CrossOver(problem.Population, Selection, problem);
                if(child.Fitness > problem.Population.First().Fitness)
                {
                    problem.Population[0] = child;
                    problem.Population.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
                }
            }
            sp.Stop();

            return problem.GetBest();

        }
    }
}
