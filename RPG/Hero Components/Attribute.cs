namespace RPG
{
    //i could have used an int vector but this is clearer and more scalable
    public struct Attribute
    {
        //variables
        public int strength = 0;
        public int dexterity = 0;
        public int intelligence = 0;

        //constructors to make an attribute
        public Attribute() { }
        public Attribute(int strength, int dexterity, int intelligence)
        {
            this.strength = strength;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
        }
        public Attribute(Attribute attribute)
        {
            this.strength = attribute.strength;
            this.dexterity = attribute.dexterity;
            this.intelligence = attribute.intelligence;
        }

        //costum operators for easier use of atributes
        public static Attribute operator +(Attribute a1, Attribute a2)
        {
            return new Attribute(
                a1.strength + a2.strength,
                a1.dexterity + a2.dexterity,
                a1.intelligence + a2.intelligence);
        }
        public static Attribute operator +(Attribute a1, int i1)
        {
            return new Attribute(
                a1.strength + i1,
                a1.dexterity + i1,
                a1.intelligence + i1);
        }

        public static Attribute operator -(Attribute a1, Attribute a2)
        {
            return new Attribute(
                  a1.strength - a2.strength,
                  a1.dexterity - a2.dexterity,
                  a1.intelligence - a2.intelligence);
        }

        public static Attribute operator -(Attribute a1, int i1)
        {
            return new Attribute(
                a1.strength - i1,
                a1.dexterity - i1,
                a1.intelligence - i1);
        }
        public static bool operator < (Attribute a1, Attribute a2)
        {
            return a1.strength < a2.strength &&
                a1.dexterity < a2.dexterity &&
                a1.intelligence < a2.intelligence;
        }
        public static bool operator > (Attribute a1, Attribute a2)
        {
            return a1.strength > a2.strength &&
                 a1.dexterity > a2.dexterity &&
                 a1.intelligence > a2.intelligence;
        }


        public int TotalLevel() { 
        return strength + dexterity + intelligence;
        }

        public override string ToString()
        {
            return $"strength: {strength}, dexterity: {dexterity}, intelligence: {intelligence}";
        }
    }
}
