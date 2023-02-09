using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Equipment
    {
        public int requiredLevel { get; protected set; }
        public HeroAttribute requredAttributeLevel { get; protected set; }
        public EquipmentSlot slot { get; protected set; }
    }
}
