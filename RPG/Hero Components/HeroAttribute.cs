using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    public struct HeroAttribute
    {
        //variables
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
        public HeroAttribute(HeroAttribute attribute)
        {
            this.Strength = attribute.Strength;
            this.Dexterity = attribute.Dexterity;
            this.Intelligence = attribute.Intelligence;
        }

        //costum operators for easier use of atributes
        public static HeroAttribute operator +(HeroAttribute a1, HeroAttribute a2)
        {
            a1.Strength += a2.Strength;
            a1.Dexterity += a2.Dexterity;
            a1.Intelligence += a2.Intelligence;
            return a1;
        }
        public static HeroAttribute operator +(HeroAttribute a1, int i1)
        {
            a1.Strength += i1;
            a1.Dexterity += i1;
            a1.Intelligence +=i1;
            return a1;
        }

        public static HeroAttribute operator -(HeroAttribute a1, HeroAttribute a2)
        {
            a1.Strength -= a2.Strength;
            a1.Dexterity -= a2.Dexterity;
            a1.Intelligence -= a2.Intelligence;
            return a1;
        }

        public static HeroAttribute operator -(HeroAttribute a1, int i1)
        {
            a1.Strength -= i1;
            a1.Dexterity -= i1;
            a1.Intelligence -= i1;
            return a1;
        }
        public static bool operator ==(HeroAttribute a1, HeroAttribute a2)
        {
            return (
                a1.Strength == a2.Strength &&
                a1.Dexterity == a2.Dexterity &&
                a1.Intelligence == a2.Intelligence);
        }
        public static bool operator !=(HeroAttribute a1, HeroAttribute a2)
        {
            return !(a1 == a2);
        }

        public override string ToString()
        {
            return $"strength: {Strength}, dexterity: {Dexterity}, intelligence: {Intelligence}";
        }
    }
}
