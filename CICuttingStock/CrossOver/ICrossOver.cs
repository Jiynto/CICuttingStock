using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICuttingStock.CrossOver
{
    public interface ICrossOver
    {
        Solution CrossOver(List<Solution> population, Selections.ISelect Selection, Problem problem);
    }
}
