using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CCCTLibrary
{
    public class Ability
    {
        public int ID { get; set; } // a unique value between all abilities.
        public string Name { get; set; }
        public Resource ResourceUse { get; set; }
        public LibRepo.AbilitySlot Slot { get; set; }

        public bool HaveActive { get; set; }
        public bool IsToogleAble { get; set; }
        public string DescriptionAct { get; set; }
        public string DamageAct { get; set; }
        public string CooldownAct { get; set; }
        public string RangeAct { get; set; }
        public string ResourceCostAct { get; set; }
        
        public bool HaveEmpoweredOrAlternative { get; set; }
        public string DescriptionEmpAlt { get; set; }
        public string DamageEmpAlt { get; set; }
        public string CooldownEmpAlt { get; set; }
        public string RangeEmpAlt { get; set; }
        public string ResourceCostEmpAlt { get; set; }

        public bool HavePassive { get; set; }
        public string DescriptionPas { get; set; }
        public string RangePas { get; set; }
        public string DamagePas { get; set; }
        public string CooldownPas { get; set; }
        public Ability()
        {

        }

        public string ToStringA()
        {
            return Name;
        }
    }
}
