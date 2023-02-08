using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Hero
    {
        protected Hero(string name, Attribute startAttribute, Attribute increaseAttribute)
        {
            this.name = name;
            level = 1;

            this.currentAttribute = startAttribute;
            this.increaseAttribute = increaseAttribute;
        }
        //common variables 
        public string name { get; private set; }
        public int level { get; private set; }


        private Attribute increaseAttribute;
        public Attribute currentAttribute { get; private set; }
        public int GetTotalLevelAttributes()
        {
            return currentAttribute.TotalLevel();
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
    }
}
