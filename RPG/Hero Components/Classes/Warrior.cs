using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    public class Warrior : Hero
    {
        public Warrior(string name) : base(name) 
        {
            //initiate a warrior
            CurrentAttribute = new(strength: 5, dexterity: 2, intelligence: 1); 
            increaseAttribute = new(strength: 3, dexterity: 2, intelligence: 1);
            ValidWeapons = new[] { WeaponType.Axes, WeaponType.Hammers, WeaponType.Swords };
            ValidArmors = new[] { ArmorType.Mail, ArmorType.Plate };
        }


        //override damage function with the warrior's armor and total attribute of their damage modifier attribute
        public override double GetDamage()
        {
            return Math.Round((EquippedWeapon == null ? 1 : EquippedWeapon.Damage) * 
                (1 + ((double)GetTotalAttributes().Strength / 100)), 2);
        }
    }
}
