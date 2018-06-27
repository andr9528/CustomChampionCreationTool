using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CCCTLibrary
{
    public class Champion
    {
        public Ability PassiveAbility { get; set; }
        public Ability QAbility { get; set; }
        public Ability WAbility { get; set; }
        public Ability EAbility { get; set; }
        public Ability RAbility { get; set; }
        public int ID { get; set; } // a unique value between all champions.
    }
}
