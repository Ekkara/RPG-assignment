using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    public class Mage : Hero
    {
        public Mage(string name) : base(name)
        {
            //initiate a mage
            CurrentAttribute = new(strength: 1, dexterity: 1, intelligence: 8);
            increaseAttribute = new(strength: 1, dexterity: 1, intelligence: 5);
            ValidWeapons = new[] { WeaponType.Staffs, WeaponType.Wands};
            ValidArmors = new[] { ArmorType.Cloth };
        }

        //override damage function with the mage's armor and total attribute of their damage modifier attribute
        public override double GetDamage()
        {
            return Math.Round((EquippedWeapon == null ? 1 : EquippedWeapon.Damage) *
                (1 + (double)GetTotalAttributes().Intelligence / 100),2);
        }
    }
}
