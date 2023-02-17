namespace RPG.Equipment_Components
{
    public class Weapon : Item
    {
        public Weapon(string name, double damage, int requiredLevel, WeaponType weaponType) : 
            base(name, requiredLevel, EquipmentSlot.Weapon)
        {
            this.Damage = damage;
            this.WeaponType = weaponType;
        }

        public double Damage { get; private set; }
        public WeaponType WeaponType { get; private set; }
    }
}
