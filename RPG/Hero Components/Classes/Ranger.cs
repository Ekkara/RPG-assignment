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
            currentAttribute = new(strength: 1, dexterity: 7, intelligence: 1);
            increaseAttribute = new(strength: 1, dexterity: 5, intelligence: 1);
            validWeapons = new[] { WeaponType.Bows };
            validArmor = new[] { ArmorType.Leather, ArmorType.Mail };

        }
        public override double GetDamage()
        {
            return ((armor[EquipmentSlot.Weapon] == null ? 1 : (armor[EquipmentSlot.Weapon] as Weapon).damage) *
                (1 + (currentAttribute.dexterity / 100)));
        }
    }
}
