using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    internal class Ranger : Hero
    {
        public Ranger(string name) : base(name)
        {
            CurrentAttribute = new(strength: 1, dexterity: 7, intelligence: 1);
            increaseAttribute = new(strength: 1, dexterity: 5, intelligence: 1);
            ValidWeapons = new[] { WeaponType.Bows };
            ValidArmors = new[] { ArmorType.Leather, ArmorType.Mail };

        }
        public override double GetDamage()
        {
            return Math.Round((EquippedWeapon==null ? 1 : EquippedWeapon.Damage) *
                (1 + ((double)CurrentAttribute.Dexterity / 100)),2);
        }
    }
}
