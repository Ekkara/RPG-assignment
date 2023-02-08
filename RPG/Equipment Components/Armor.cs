using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Armor : Equipment
    {
        public int deffenseModifier { get; private set; }
        public Armor(int deffenseModifier)
        {
            this.deffenseModifier = deffenseModifier;
        }
    }
}
