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
        public Armor(string name, HeroAttribute attributeModifier, ArmorType armorType, int requiredLevel, HeroAttribute requiredAttributeLevel, EquipmentSlot slot) :
            base(name, requiredLevel, slot)
        {
            this.ArmorType = armorType;
            if (Slot == EquipmentSlot.Weapon)
            {
                Console.WriteLine("Not allowed to be set as a weapon");
            }
            this.AttributeModifier = attributeModifier;
        }
        public ArmorType ArmorType { get; private set; }
        public HeroAttribute AttributeModifier { get; private set; }
    }
}
