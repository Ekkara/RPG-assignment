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
            currentAttribute = new(strength: 1, dexterity: 1, intelligence: 8);
            increaseAttribute = new(strength: 1, dexterity: 1, intelligence: 5);
            validWeapons = new[] { WeaponType.Staffs, WeaponType.Wands};
            validArmors = new[] { ArmorType.Cloth };
        }

        public override double GetDamage()
        {
            return Math.Round((equippedWeapon == null ? 1 : equippedWeapon.damage) *
                (1 + (double)currentAttribute.intelligence / 100),2);
        }
    }
}
