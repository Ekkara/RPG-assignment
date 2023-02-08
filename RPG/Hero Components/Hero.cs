using RPG.Equipment_Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Hero
    {
        protected Hero(string name)
        {
            this.name = name;
            level = 1;
        }
        //common variables 
        public string name { get; private set; }
        public int level { get; private set; }


        protected Attribute increaseAttribute;
        public Attribute currentAttribute { get; protected set; }
        public int GetTotalLevelAttributes()
        {
            return currentAttribute.TotalLevel();
        }

        public Dictionary<EquipmentSlot, Equipment> equipments{ get; private set; } = new();
        public Weapon equpiedWeapon { get; private set; }
        public void Equip(Equipment equipment)
        {
            //TODO: add requirements 
            equipments.Add(equipment.Slot, equipment);   
        }
        public WeaponType[] validWeapons { get; protected set; }
        public abstract double GetDamage();
        public ArmorType[] validArmor { get; protected set; }
        public Attribute GetTotalAttributes()
        {
            int totalArmorAmplifier = 0;

            for (int index = 0; index < equipments.Count; index++)
            {
                KeyValuePair<EquipmentSlot, Equipment> item = equipments.ElementAt(index);
                if(item.Key != EquipmentSlot.Weapon) //do i need to check? can't i just try and cast if it doesn't work it is not an armor?
                {
                    if (item.Value == null) continue;
                    totalArmorAmplifier += (item.Value as Armor).deffenseModifier; 
                }
            }
            return new Attribute(currentAttribute + totalArmorAmplifier);
        }

        //level up function, TODO: add xp system and from that calculate how many levels should be increased 
        public void LevelUp(int amountOfLevels = 1)
        {
            for (int i = 0; i < amountOfLevels; i++)
            {
                level++; 
                currentAttribute += increaseAttribute;
            }
        }

        public string DisplayState()
        {
            StringBuilder sb = new StringBuilder();

            Attribute totalAttributes = GetTotalAttributes();

            sb.Append("Name: " + name + '\n');
            sb.Append("Class: " + GetType() + '\n');
            sb.Append("Level:" + level + '\n');
            sb.Append("Total strength: " + totalAttributes.strength + '\n');
            sb.Append("Total dexterity " + totalAttributes.dexterity + '\n' );
            sb.Append("Total intelligence" + totalAttributes.intelligence + '\n');
            sb.Append("Damage: " + GetDamage() + '\n');
                 
            return sb.ToString();
        }
    }
}
