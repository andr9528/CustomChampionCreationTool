using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChampionCreationTool.Objects
{
    public class Resource
    {
        public int MaxValue { get; internal set; } // -1 if no max.
        public int MinValue { get; internal set; } // should proably always be 0.
        public string Name { get; internal set; } // name of the Resource. ie Resourseless, Ferocity, Mana...
        public int ID { get; internal set; } // a unique value between all resources.
        public bool MaxedAtStart { get; internal set; } // wether or not champions current max starts at max.

        public Resource()
        {
        }

        public string ToStringR()
        {
            return Name;
        }
    }
}
