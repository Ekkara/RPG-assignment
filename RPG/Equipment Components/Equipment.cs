using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Equipment
    {
        public string Name { get; protected set; } = "[Name not found]";
        public int RequiredLevel { get; protected set; }
        public EquipmentSlot Slot { get; protected set; }
    }
}
