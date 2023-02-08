using RPG.Equipment_Components;
using System;
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

        public List<Equipment> equipments{ get; private set; } = new();
        public WeaponType[] validWeapons { get; protected set; }
        public ArmorType[] validArmor { get; protected set; }


        //level up function, TODO: add xp system and from that calculate how many levels should be increased 
        public void LevelUp(int amountOfLevels = 1)
        {
            for (int i = 0; i < amountOfLevels; i++)
            {
                level++; 
                currentAttribute += increaseAttribute;
            }
        }
    }
}
