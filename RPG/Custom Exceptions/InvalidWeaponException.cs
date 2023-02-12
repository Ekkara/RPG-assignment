using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Custom_Exceptions
{
    public class InvalidWeaponException : Exception 
    {
        public override string Message => "Something went wrong with a weapon";
        public InvalidWeaponException() { }
        public InvalidWeaponException(string message) : base(message) { }
    }
}
