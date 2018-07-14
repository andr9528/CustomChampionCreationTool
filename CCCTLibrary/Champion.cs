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
        public Resource Resource { get; set; }
        public Ability PassiveAbility { get; set; }
        public Ability QAbility { get; set; }
        public Ability WAbility { get; set; }
        public Ability EAbility { get; set; }
        public Ability RAbility { get; set; }
        public int ID { get; set; } // a unique value between all champions.
        public string Name { get; set; }
        public string HealthStart { get; set; }
        public string HealthGrowth { get; set; }
        public string HealthRegenStart { get; set; }
        public string HealthRegenGrowth { get; set; }
        public string ResourceStart { get; set; }
        public string ResourceGrowth { get; set; }
        public string ResourceRegenStart { get; set; }
        public string ResourceRegenGrowth { get; set; }
        public string AttackDamageStart { get; set; }
        public string AttackDamageGrowth { get; set; }
        public string AbilityPowerStart { get; set; }
        public string AbilityPowerGrowth { get; set; }
        public string AttackSpeedStart { get; set; }
        public string AttackSpeedGrowth { get; set; }
        public string RangeStart { get; set; }
        public string RangeGrowth { get; set; }
        public string CriticalStrikeChanceStart { get; set; }
        public string CriticalStrikeChanceGrowth { get; set; }
        public string ArmorStart { get; set; }
        public string ArmorGrowth { get; set; }
        public string MagicResistStart { get; set; }
        public string MagicResistGrowth { get; set; }
        public string MoveSpeedStart { get; set; }
        public string MoveSpeedGrowth { get; set; }


        public string ToStringC()
        {
            return Name;
        }
    }
}
