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
        public int strength { get; private set; }
        public int dexterity { get; private set; }
        public int intelligence { get; private set; }

        //constructors to make an attribute
        public HeroAttribute()
        {
            this.strength = 0;
            this.dexterity = 0;
            this.intelligence = 0;
        }
        public HeroAttribute(int strength, int dexterity, int intelligence)
        {
            this.strength = strength;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
        }
        public HeroAttribute(HeroAttribute attribute)
        {
            this.strength = attribute.strength;
            this.dexterity = attribute.dexterity;
            this.intelligence = attribute.intelligence;
        }

        //costum operators for easier use of atributes
        public static HeroAttribute operator +(HeroAttribute a1, HeroAttribute a2)
        {
            a1.strength += a2.strength;
            a1.dexterity += a2.dexterity;
            a1.intelligence += a2.intelligence;
            return a1;
        //    return new HeroAttribute(
        //        a1.strength + a2.strength,
        //        a1.dexterity + a2.dexterity,
        //        a1.intelligence + a2.intelligence);
        //
        }
        public static HeroAttribute operator +(HeroAttribute a1, int i1)
        {
            return new HeroAttribute(
                a1.strength + i1,
                a1.dexterity + i1,
                a1.intelligence + i1);
        }

        public static HeroAttribute operator -(HeroAttribute a1, HeroAttribute a2)
        {
            return new HeroAttribute(
                  a1.strength - a2.strength,
                  a1.dexterity - a2.dexterity,
                  a1.intelligence - a2.intelligence);
        }

        public static HeroAttribute operator -(HeroAttribute a1, int i1)
        {
            return new HeroAttribute(
                a1.strength - i1,
                a1.dexterity - i1,
                a1.intelligence - i1);
        }
        public static bool operator <(HeroAttribute a1, HeroAttribute a2)
        {
            return a1.strength < a2.strength &&
                a1.dexterity < a2.dexterity &&
                a1.intelligence < a2.intelligence;
        }
        public static bool operator >(HeroAttribute a1, HeroAttribute a2)
        {
            return (a1.strength > a2.strength &&
                 a1.dexterity > a2.dexterity &&
                 a1.intelligence > a2.intelligence);
        }

        public override string ToString()
        {
            return $"strength: {strength}, dexterity: {dexterity}, intelligence: {intelligence}";
        }
    }
}
