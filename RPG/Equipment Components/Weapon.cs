using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Equipment_Components
{
    public class Weapon : Equipment
    {
        public Weapon(double damage)
        {
            this.damage = damage;
        }

        public double damage { get; private set; }
    }
}
