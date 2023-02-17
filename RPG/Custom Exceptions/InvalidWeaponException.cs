namespace RPG.Custom_Exceptions
{
    public class InvalidWeaponException : Exception 
    {
        //default error message
        public override string Message => "Something went wrong with a weapon";
        public InvalidWeaponException() { }
        public InvalidWeaponException(string message) : base(message) { }
    }
}
