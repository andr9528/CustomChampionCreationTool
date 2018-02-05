using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChampionCreationTool.Objects
{
    public class Champion
    {
        public Ability PassiveAbility { get; internal set; }
        public Ability QAbility { get; internal set; }
        public Ability WAbility { get; internal set; }
        public Ability EAbility { get; internal set; }
        public Ability RAbility { get; internal set; }
    }
}
