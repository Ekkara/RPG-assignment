using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    public class Rogue : Hero
    {
        public Rogue(string name) : base(name) {
            CurrentAttribute = new(strength: 2, dexterity: 6, intelligence: 1); 
            increaseAttribute = new(strength: 1, dexterity: 4, intelligence: 1);
            ValidWeapons = new[] { WeaponType.Daggers, WeaponType.Swords };
            ValidArmors = new[] { ArmorType.Leather, ArmorType.Mail };
        }
        public override double GetDamage()
        {
            return Math.Round((EquippedWeapon == null ? 1 : EquippedWeapon.Damage) * 
                (1 + ((double)GetTotalAttributes().Dexterity / 100)), 2);
        }
    }
}
