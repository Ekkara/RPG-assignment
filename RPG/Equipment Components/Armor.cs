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
            this.DeffenseModifier = deffenseModifier;
            this.ArmorType = armorType;
            this.Slot = slot;
            this.RequiredLevel = requiredLevel;
            this.RequiredAttributeLevel = requiredAttributeLevel;
        }
        public int DeffenseModifier { get; private set; }
        public ArmorType ArmorType { get; private set; }
    }
}
