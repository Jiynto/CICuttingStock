using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CICuttingStock.Searches
{
    class ESSubSet : EvolutionarySearch
    {
        private int parentNo;
        private int offspringNo;


        public ESSubSet(Problem _problem, int generationSize, Initialisations.IInitialisation Initialise, int _parentNo, int _offspringNo) : base(_problem, generationSize, Initialise)
        {
            parentNo = _parentNo;
            offspringNo = _offspringNo;
        }

        public override Solution Search(int runtime, Selections.ISelect Selection, CrossOver.ICrossOver CrossOver)
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();

            while(sp.ElapsedMilliseconds < runtime)
            {
                List<Solution> parents = Selection.Select(problem.Population, false, parentNo);
                Selections.RandomSelection randomSelection = new Selections.RandomSelection();
                List<Solution> children = new List<Solution>();
                for(int j = 0; j < offspringNo; j++)
                {
                    Solution child = CrossOver.CrossOver(parents, randomSelection, problem);
                    children.Add(child);
                }

                foreach(Solution child in children)
                {

                    if(child.Fitness < problem.Population[0].Fitness)
                    {
                        for (int s = 0; s < problem.Population.Count; s++)
                        {
                            if (child.Fitness > problem.Population[s].Fitness && s == problem.Population.Count - 1)
                            {
                                problem.Population[s] = child;
                            }
                            else if (problem.Population[s].Fitness < child.Fitness && child.Fitness < problem.Population[s + 1].Fitness)
                            {
                                problem.Population[s] = child;
                                break;
                            }
                        }
                    }
                    
                    for(int s = 0; s < problem.Population.Count; s++)
                    {
                        if(child.Fitness > problem.Population[s].Fitness && s == problem.Population.Count -1)
                        {
                            problem.Population[s] = child;
                        }
                        else if(problem.Population[s].Fitness < child.Fitness  && child.Fitness < problem.Population[s + 1].Fitness)
                        {
                            problem.Population[s] = child;
                            break;
                        }

                    }
                    /*
                    if(children[j].Fitness > problem.Population[0].Fitness)
                    {
                        problem.Population[0] = children[j];
                        problem.Population.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
                    }
                    */
                    
                }
                problem.Population.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
            }
            sp.Stop();

            return problem.GetBest();

        }

    }
}
