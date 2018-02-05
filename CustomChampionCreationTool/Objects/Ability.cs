using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomChampionCreationTool.Objects
{
    public class Ability
    {
        public int ID { get; internal set; } // a unique value between all abilities.
        public string Name { get; internal set; }
        public Resource ResourceUse { get; internal set; }
        public Repo.AbilitySlot Slot { get; internal set; }

        public bool HaveActive { get; internal set; }
        public string DescriptionNorm { get; internal set; }
        public string DamageNorm { get; internal set; }
        public string CooldownNorm { get; internal set; }
        public int RangeNorm { get; internal set; }
        public int ResourceCostNorm { get; internal set; }
        
        public bool HaveEmpowered { get; internal set; }
        public string DescriptionEmp { get; internal set; }
        public string DamageEmp { get; internal set; }
        public string CooldownEmp { get; internal set; }
        public int RangeEmp { get; internal set; }
        public int ResourceCostEmp { get; internal set; }

        public bool HavePassive { get; internal set; }
        public string DescriptionPass { get; internal set; }
        public int RangePass { get; internal set; }
        public string DamagePass { get; internal set; }
        public string CooldownPass { get; internal set; }

        public Ability()
        {

        }

    }
}
