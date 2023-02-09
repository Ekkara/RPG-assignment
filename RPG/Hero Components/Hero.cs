using RPG.Equipment_Components;
using RPG.Hero_Components;
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


        protected HeroAttribute increaseAttribute;
        public HeroAttribute currentAttribute { get; protected set; }
        public int GetTotalLevelAttributes()
        {
            return currentAttribute.TotalLevel();
        }

        public Dictionary<EquipmentSlot, Armor> equippedArmor { get; private set; } = new();
        public Weapon equippedWeapon { get; private set; }
        public void Equip(Equipment equipment)
        {
            if (equipment.requiredLevel > level)
            {
                Console.WriteLine("Too low level to equip this item");
                return;
            }
            if (equipment.requredAttributeLevel > currentAttribute)
            {
                Console.WriteLine("Too low attribute levels to equip this item!");
                return;
            }

            if (equipment.slot == EquipmentSlot.Weapon)
            {
                if (!validWeapons.Contains((equipment as Weapon).weaponType))
                {
                    Console.WriteLine(GetType().Name + " can't equip this type of weapon");
                    return;
                }
                equippedWeapon = equipment as Weapon;
            }
            else
            {
                if (!validArmors.Contains((equipment as Armor).armorType))
                {
                    Console.WriteLine(GetType().Name + " can't equip this type of armor");
                    return;
                }
                equippedArmor.Remove(equipment.slot);                
                equippedArmor.Add(equipment.slot, equipment as Armor);
            }
        }
        public WeaponType[] validWeapons { get; protected set; }
        public abstract double GetDamage();
        public ArmorType[] validArmors { get; protected set; }
        public HeroAttribute GetTotalAttributes()
        {
            int totalArmorAmplifier = 0;

            for (int index = 0; index < equippedArmor.Count; index++)
            {
                KeyValuePair<EquipmentSlot, Armor> item = equippedArmor.ElementAt(index);
                if (item.Value == null) continue;
                totalArmorAmplifier += item.Value.deffenseModifier;
            }
            return new HeroAttribute(currentAttribute + totalArmorAmplifier);
        }

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
            HeroAttribute totalAttributes = GetTotalAttributes();
         
            StringBuilder sb = new StringBuilder();

            sb.Append("Name: " + name + '\n');
            sb.Append("Class: " + GetType().Name + '\n');
            sb.Append("Level:" + level + '\n');
            sb.Append("Total strength: " + totalAttributes.strength + '\n');
            sb.Append("Total dexterity " + totalAttributes.dexterity + '\n');
            sb.Append("Total intelligence " + totalAttributes.intelligence + '\n');
            sb.Append("Damage: " + GetDamage() + '\n');

            return sb.ToString();
        }
    }
}
