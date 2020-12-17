using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CICuttingStock
{
    public class Problem
    {
        private int m;
        private int n;

        private List<Stock> stock;
        public List<Stock> Stock { get { return stock;  } }

        private Dictionary<float, float> orders;
        public Dictionary<float, float> Orders { get { return orders; } }
        
        private List<Solution> population;
        public List<Solution> Population { get { return population;  } set { population = value; } }



        public Problem(string fileName)
        {
            stock = new List<Stock>();
            orders = new Dictionary<float, float>();
            ParseProblemFile(fileName);
            population = new List<Solution>();
        }

        public Solution GetBest()
        {
            return population.Aggregate((x, y) => x.TotalCost < y.TotalCost ? x : y);
            //return population.Aggregate((x, y) => x.Fitness > y.Fitness ? x : y);
        }


        private void ParseProblemFile(string fileName)
        {
            List<float> stockCosts = new List<float>();
            List<float> stockLengths = new List<float>();
            List<float> orderLengths = new List<float>();
            List<float> orderNums = new List<float>();
            StreamReader reader = File.OpenText(fileName);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("#") && line != "")
                {
                    line = line.Replace(" ","");
                    string[] headerBody = line.Split(':');
                    string header = headerBody[0];
                    string body = headerBody[1];
                    string[] bodyVals = body.Split(',');
                    List<float> values = new List<float>();
                    foreach (string v in bodyVals) values.Add(float.Parse(v)); 

                    switch (header)
                    {
                        case "m":
                            m = (int)values.First();
                            break;
                        case "n":
                            n = (int)values.First();
                            break;
                        case "Stocklengths":
                            stockLengths = values;
                            break;
                        case "Stockcosts":
                            stockCosts = values;
                            break;
                        case "Piecelengths":
                            orderLengths = values;
                            break;
                        case "Quantities":
                            orderNums = values;
                            break;
                    }

                }
            }


            for (int i = 0; i < stockLengths.Count; i++)
            {
                Stock newStock = new Stock(stockLengths[i], stockCosts[i]);
                stock.Add(newStock);
            }

            for (int i = 0; i < orderLengths.Count; i++)
            {
                orders.Add(orderLengths[i], orderNums[i]);
            }
        }




    }
}
