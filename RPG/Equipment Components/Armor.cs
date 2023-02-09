using RPG.Equipment_Components;
using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Armor : Equipment
    {
        public Armor(int deffenseModifier, ArmorType armorType, int requiredLevel, HeroAttribute requiredAttributeLevel, EquipmentSlot slot)
        {
            this.deffenseModifier = deffenseModifier;
            this.armorType = armorType;
            this.slot = slot;
            this.requiredLevel = requiredLevel;
            this.requiredAttributeLevel = requiredAttributeLevel;
        }
        public int deffenseModifier { get; private set; }
        public ArmorType armorType { get; private set; }
    }
}
