using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CICuttingStock.Components
{
    public class PrintResults
    {
        private string results;
        private Dictionary<Solution, List<Solution>> searches;
        public PrintResults()
        {
            searches = new Dictionary<Solution, List<Solution>>();
        }

        public void AddSearch(Solution best, List<Solution> population)
        {
            searches.Add(best, population);
        }

        public void Print()
        {
            Done();
            Console.WriteLine(results);
        }

        public void WriteToFile(string fileName)
        {
            Done();
            File.WriteAllText("Results/" +fileName + ".txt", results);
        }

        public void Done()
        {
            int count = 0;
            foreach (Solution best in searches.Keys)
            {
                count++;
                results += "Search Number " + count + ":\n";
                /*
                foreach (Solution s in searches[best])
                {
                    results += s.TotalCost + "\n";
                }
                */
                results += "Best Solution:\n";
                results += best.ToString() + "\n";

            }
        }


    }
}
