using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Graph.Model
{
    public class CollectionResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
