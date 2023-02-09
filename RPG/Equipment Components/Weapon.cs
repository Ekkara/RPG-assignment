using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Equipment_Components
{
    public class Weapon : Equipment
    {
        public Weapon(double damage, int requiredLevel, HeroAttribute requiredAttributeLevel, WeaponType weaponType)
        {
            this.damage = damage;
            this.weaponType = weaponType;
            this.slot = EquipmentSlot.Weapon;
            this.requiredLevel = requiredLevel;
            this.requiredAttributeLevel = requiredAttributeLevel;
        }

        public double damage { get; private set; }
        public WeaponType weaponType { get; private set; }
    }
}
