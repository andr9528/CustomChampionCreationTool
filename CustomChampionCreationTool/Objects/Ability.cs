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
        public bool IsToogleAble { get; internal set; }
        public string DescriptionAct { get; internal set; }
        public string DamageAct { get; internal set; }
        public string CooldownAct { get; internal set; }
        public string RangeAct { get; internal set; }
        public string ResourceCostAct { get; internal set; }
        
        public bool HaveEmpoweredOrAlternative { get; internal set; }
        public string DescriptionEmpAlt { get; internal set; }
        public string DamageEmpAlt { get; internal set; }
        public string CooldownEmpAlt { get; internal set; }
        public string RangeEmpAlt { get; internal set; }
        public string ResourceCostEmpAlt { get; internal set; }

        public bool HavePassive { get; internal set; }
        public string DescriptionPas { get; internal set; }
        public string RangePas { get; internal set; }
        public string DamagePas { get; internal set; }
        public string CooldownPas { get; internal set; }

        public Ability()
        {

        }

    }
}
