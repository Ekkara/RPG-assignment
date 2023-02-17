using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Armor : Item
    {
        public Armor(string name, HeroAttribute attributeModifier, ArmorType armorType, int requiredLevel, EquipmentSlot slot) :
            base(name, requiredLevel, slot)
        {
            //prevent any missuse of armor, by trying to assign armor as a weapon
            if (Slot == EquipmentSlot.Weapon)
            {
                throw new InvalidArmorException("an armor can't be placed in the weapon slot");
            }
            //assigning variables
            this.ArmorType = armorType;
            this.AttributeModifier = attributeModifier;
        }
        public ArmorType ArmorType { get; private set; }
        public HeroAttribute AttributeModifier { get; private set; }
    }
}
