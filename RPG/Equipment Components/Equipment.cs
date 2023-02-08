using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Equipment
    {
        public int requiredLevel { get; set; }
        public Attribute requredAttributeLevel { get; set; }
        public EquipmentSlot Slot { get; private set; }
    }
}
