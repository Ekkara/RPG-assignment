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
            this.Name = name;
            Level = 1;
        }
        //common variables 
        public string Name { get; private set; }
        public int Level { get; private set; }


        protected HeroAttribute increaseAttribute;
        public HeroAttribute CurrentAttribute { get; protected set; }
   

        public Dictionary<EquipmentSlot, Armor> EquippedArmor { get; private set; } = new();
        public Weapon EquippedWeapon { get; private set; }
        
        public void Equip(Equipment equipment)
        {
            if (equipment.RequiredLevel > Level)
            {
                Console.WriteLine("Too low level to equip this item");
                return;
            }
            if (equipment.Slot == EquipmentSlot.Weapon)
            {
                if (!ValidWeapons.Contains((equipment as Weapon).WeaponType))
                {
                    Console.WriteLine(GetType().Name + " can't equip this type of weapon");
                    return;
                }
                EquippedWeapon = equipment as Weapon;
            }
            else
            {
                if (!ValidArmors.Contains((equipment as Armor).ArmorType))
                {
                    Console.WriteLine(GetType().Name + " can't equip this type of armor");
                    return;
                }
                EquippedArmor.Remove(equipment.Slot);
                EquippedArmor.Add(equipment.Slot, equipment as Armor);
            }
        }
        public WeaponType[] ValidWeapons { get; protected set; }
        public abstract double GetDamage();
        public ArmorType[] ValidArmors { get; protected set; }
        public HeroAttribute GetTotalAttributes()
        {
            int totalArmorAmplifier = 0;

            for (int index = 0; index < EquippedArmor.Count; index++)
            {
                KeyValuePair<EquipmentSlot, Armor> item = EquippedArmor.ElementAt(index);
                if (item.Value == null) continue;
                totalArmorAmplifier += item.Value.DeffenseModifier;
            }
            return new HeroAttribute(CurrentAttribute + totalArmorAmplifier);
        }

        public void LevelUp(int amountOfLevels = 1)
        {
            for (int i = 0; i < amountOfLevels; i++)
            {
                Level++;
                CurrentAttribute += increaseAttribute;
            }
        }

        public string DisplayState()
        {
            HeroAttribute totalAttributes = GetTotalAttributes();
         
            StringBuilder sb = new StringBuilder();

            sb.Append("Name: " + Name + '\n');
            sb.Append("Class: " + GetType().Name + '\n');
            sb.Append("Level:" + Level + '\n');
            sb.Append("Total strength: " + totalAttributes.Strength + '\n');
            sb.Append("Total dexterity " + totalAttributes.Dexterity + '\n');
            sb.Append("Total intelligence " + totalAttributes.Intelligence + '\n');
            sb.Append("Damage: " + GetDamage() + '\n');

            return sb.ToString();
        }
    }
}
