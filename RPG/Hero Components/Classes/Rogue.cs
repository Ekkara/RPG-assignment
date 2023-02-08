using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Hero_Components
{
    internal class Rogue : Hero
    {
        public Rogue(string name, Attribute startAttribute, Attribute increaseAttribute) : base(name, startAttribute, increaseAttribute)
        {

        }
    }
}
