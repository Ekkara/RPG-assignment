using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG.Hero_Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        public WeaponType[] ValidWeapons { get; protected set; }
        public ArmorType[] ValidArmors { get; protected set; }

        public Dictionary<EquipmentSlot, Armor> EquippedArmor { get; private set; } = new();
        public Weapon EquippedWeapon { get; private set; }

        //funtion to try equip a weapon

        public virtual void Equip(Weapon weaponToEquip)
        {
            //failsafe to see if weapon can be equiped
            if (weaponToEquip.RequiredLevel > Level)
            {
                throw new InvalidWeaponException("Too low level to equip this weapon");
            }
            else if (!ValidWeapons.Contains(weaponToEquip.WeaponType))
            {
                throw new InvalidWeaponException(GetType().Name + " can't equip this type of weapon");
            }
            else
            {
                EquippedWeapon = weaponToEquip;
            }
        }

        //function to try to equip a weapon, same name as the function with weapon for easier use
        public virtual void Equip(Armor armorToEquip)
        {
            //same failsafe as before
            if (armorToEquip.RequiredLevel > Level)
            {
                throw new InvalidArmorException("Too low level to equip this armor");
            }
            else if (!ValidArmors.Contains(armorToEquip.ArmorType))
            {
                throw new InvalidArmorException(GetType().Name + " can't equip this type of armor");
            }
            EquippedArmor.Remove(armorToEquip.Slot);
            EquippedArmor.Add(armorToEquip.Slot, armorToEquip);
        }

        //promes all heroes have a function to calculate their damage
        public abstract double GetDamage();

        //calculate total attribute from current attribute (defined by level and class) and armor
        public virtual HeroAttribute GetTotalAttributes()
        {
            HeroAttribute modifier = new();
            for (int index = 0; index < EquippedArmor.Count; index++)
            {
                KeyValuePair<EquipmentSlot, Armor> item = EquippedArmor.ElementAt(index);
                if (item.Value == null) continue;
                modifier += item.Value.AttributeModifier;
            }
            return new HeroAttribute(CurrentAttribute + modifier);
        }

        //level up x amount of times
        public virtual void LevelUp(int amountOfLevels = 1)
        {
            for (int i = 0; i < amountOfLevels; i++)
            {
                Level++;
                CurrentAttribute += increaseAttribute;
            }
        }

        //fetch all information in form of a string
        public virtual string DisplayState()
        {
            HeroAttribute totalAttributes = GetTotalAttributes();

            StringBuilder sb = new StringBuilder();

            sb.Append("Name: " + Name + '\n');
            sb.Append("Class: " + GetType().Name + '\n');
            sb.Append("Level: " + Level + '\n');
            sb.Append("Total strength: " + totalAttributes.Strength + '\n');
            sb.Append("Total dexterity " + totalAttributes.Dexterity + '\n');
            sb.Append("Total intelligence " + totalAttributes.Intelligence + '\n');
            sb.Append("Damage: " + GetDamage() + '\n');

            return sb.ToString();
        }
    }
}
