using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Initialisations
{
    public interface IInitialisation
    {
        List<Solution> Initialise(List<Stock> stock, Dictionary<float, float> orders, int generationSize);
    }
}
