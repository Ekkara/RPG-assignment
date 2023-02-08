using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    internal class Rogue : Hero
    {
        public Rogue(string name) : base(name) {
            currentAttribute = new(strength: 2, dexterity: 6, intelligence: 1); 
            increaseAttribute = new(strength: 1, dexterity: 4, intelligence: 1);
            validWeapons = new[] { WeaponType.Daggers, WeaponType.Swords };
            validArmor = new[] { ArmorType.Leather, ArmorType.Mail };
        }
    }
}
