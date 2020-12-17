using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock
{
    class Program
    {


        static void Main(string[] args)
        {

            Problem newProblem = new Problem("Problems/problem3.txt");
            Components.PrintResults newPrint = new Components.PrintResults();

            /*
            Initialisations.RandomInitialisation initialise = new Initialisations.RandomInitialisation();
            Selections.RouletteWheelSelection select = new Selections.RouletteWheelSelection();
            CrossOver.GeneSelectCrossOver crossover = new CrossOver.GeneSelectCrossOver();

            for (int i = 0; i < 30; i++)
            {
                Searches.ESFullSet newSearch = new Searches.ESFullSet(newProblem, 100, initialise);
                Solution bestSolution = newSearch.Search(30000, select, crossover);
                newPrint.AddSearch(bestSolution, newProblem.Population);
            }
           */



            
            Initialisations.RandomInitialisation initialiseB = new Initialisations.RandomInitialisation();
            Selections.TournamentSelection selectB = new Selections.TournamentSelection();
            CrossOver.TwoPointCrossOver crossoverB = new CrossOver.TwoPointCrossOver();

            for(int i = 0; i < 30; i++)
            {
                Searches.ESSubSet newSearchB = new Searches.ESSubSet(newProblem, 100, initialiseB, 30, 10);
                Solution bestSolutionB = newSearchB.Search(30000, selectB, crossoverB);
                newPrint.AddSearch(bestSolutionB, newProblem.Population);
            }

            

            newPrint.WriteToFile("TestSubSet");




        }

    }
}
