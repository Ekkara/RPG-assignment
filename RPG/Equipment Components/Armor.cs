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
        public Armor(string name, int deffenseModifier, ArmorType armorType, int requiredLevel, HeroAttribute requiredAttributeLevel, EquipmentSlot slot) :
            base(name, requiredLevel, slot)
        {
            this.DeffenseModifier = deffenseModifier;
            this.ArmorType = armorType;
            if(Slot == EquipmentSlot.Weapon)
            {
                Console.WriteLine("Not allowed to be set as a weapon");
            }
        }
        public int DeffenseModifier { get; private set; }
        public ArmorType ArmorType { get; private set; }
    }
}
