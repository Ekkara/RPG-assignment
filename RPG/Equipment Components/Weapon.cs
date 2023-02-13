using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Equipment_Components
{
    public class Weapon : Item
    {
        public Weapon(string name, double damage, int requiredLevel, WeaponType weaponType) : 
            base(name, requiredLevel, EquipmentSlot.Weapon)
        {
            this.Damage = damage;
            this.WeaponType = weaponType;
        }

        public double Damage { get; private set; }
        public WeaponType WeaponType { get; private set; }
    }
}
