using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Custom_Exceptions
{
    public class InvalidArmorException : Exception
    {
        //default error message
        public override string Message => "Something went wrong with the armor";
        public InvalidArmorException() { }
        public InvalidArmorException(string message) : base(message) { }
    }
}
