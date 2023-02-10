using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    internal class Mage : Hero
    {
        public Mage(string name) : base(name)
        {
            CurrentAttribute = new(strength: 1, dexterity: 1, intelligence: 8);
            increaseAttribute = new(strength: 1, dexterity: 1, intelligence: 5);
            ValidWeapons = new[] { WeaponType.Staffs, WeaponType.Wands};
            ValidArmors = new[] { ArmorType.Cloth };
        }

        public override double GetDamage()
        {
            return Math.Round((EquippedWeapon == null ? 1 : EquippedWeapon.Damage) *
                (1 + (double)CurrentAttribute.Intelligence / 100),2);
        }
    }
}
