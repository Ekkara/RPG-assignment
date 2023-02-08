using RPG.Equipment_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Armor : Equipment
    {
        public Armor(int deffenseModifier, ArmorType armorType)
        {
            this.deffenseModifier = deffenseModifier;
            this.armorType = armorType;
        }

        public int deffenseModifier { get; private set; }
        public ArmorType armorType { get; private set; }
    }
}
