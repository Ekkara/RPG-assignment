using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Equipment_Components
{
    public class Weapon : Equipment
    {
        public Weapon(int damage, WeaponType weaponType)
        {
            this.damage = damage;
            this.weaponType = weaponType;
        }

        public int damage { get; private set; }
        public WeaponType weaponType { get; private set; }
    }
}
