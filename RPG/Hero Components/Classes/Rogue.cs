using RPG.Equipment_Components;

namespace RPG.Hero_Components
{
    public class Rogue : Hero
    {
        public Rogue(string name) : base(name) {
            //initiate a rogue
            CurrentAttribute = new(strength: 2, dexterity: 6, intelligence: 1); 
            increaseAttribute = new(strength: 1, dexterity: 4, intelligence: 1);
            ValidWeapons = new[] { WeaponType.Daggers, WeaponType.Swords };
            ValidArmors = new[] { ArmorType.Leather, ArmorType.Mail };
        }
        public override double GetDamage()
        {
            //override damage function with the rogue's armor and total attribute of their damage modifier attribute
            return Math.Round((EquippedWeapon == null ? 1 : EquippedWeapon.Damage) * 
                (1 + ((double)GetTotalAttributes().Dexterity / 100)), 2);
        }
    }
}
