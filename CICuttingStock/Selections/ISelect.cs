using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.Selections
{
    public interface ISelect
    {
        List<Solution> Select(List<Solution> population, bool asexual, int parentNo);
    }
}
