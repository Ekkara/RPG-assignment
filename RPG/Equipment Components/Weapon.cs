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
            this.Damage = damage;
            this.WeaponType = weaponType;
            this.Slot = EquipmentSlot.Weapon;
            this.RequiredLevel = requiredLevel;
            this.RequiredAttributeLevel = requiredAttributeLevel;
        }

        public double Damage { get; private set; }
        public WeaponType WeaponType { get; private set; }
    }
}
