namespace RPG.Hero_Components
{
    //cosutm data type "HeroAtribute" inspired by int vector with clearer name for the purpose of this assignment
    public struct HeroAttribute
    {
        //get set for the thre attributes 
        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Intelligence { get; private set; }

        //constructors to make an attribute
        public HeroAttribute()
        {
            this.Strength = 0;
            this.Dexterity = 0;
            this.Intelligence = 0;
        }
        public HeroAttribute(int strength, int dexterity, int intelligence)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Intelligence = intelligence;
        }
        public HeroAttribute(int value)
        {
            this.Strength = value;
            this.Dexterity = value;
            this.Intelligence = value;
        }
        public HeroAttribute(HeroAttribute attribute)
        {
            this.Strength = attribute.Strength;
            this.Dexterity = attribute.Dexterity;
            this.Intelligence = attribute.Intelligence;
        }

        //costum add operator to easier use the data type
        public static HeroAttribute operator +(HeroAttribute a1, HeroAttribute a2)
        {
            a1.Strength += a2.Strength;
            a1.Dexterity += a2.Dexterity;
            a1.Intelligence += a2.Intelligence;
            return a1;
        }
    }
}
