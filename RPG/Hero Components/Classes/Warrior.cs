using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    internal class Warrior : Hero
    {
        public Warrior(string name) : base(name) 
        {
            currentAttribute = new(strength: 5, dexterity: 2, intelligence: 1); 
            increaseAttribute = new(strength: 3, dexterity: 2, intelligence: 1);
            validWeapons = new[] { WeaponType.Axes, WeaponType.Hammers, WeaponType.Swords };
            validArmor = new[] { ArmorType.Mail, ArmorType.Plate };
        }
        public override double GetDamage()
        {
            return ((equipments[EquipmentSlot.Weapon] == null ? 1 : (equipments[EquipmentSlot.Weapon] as Weapon).damage) *
                (1 + (currentAttribute.strength / 100)));
        }
    }
}
