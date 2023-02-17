using RPG.Equipment_Components;

namespace RPG.Hero_Components
{
    public class Ranger : Hero
    {
        public Ranger(string name) : base(name)
        {
            //inititate a ranger
            CurrentAttribute = new(strength: 1, dexterity: 7, intelligence: 1);
            increaseAttribute = new(strength: 1, dexterity: 5, intelligence: 1);
            ValidWeapons = new[] { WeaponType.Bows };
            ValidArmors = new[] { ArmorType.Leather, ArmorType.Mail };

        }
        public override double GetDamage()
        {
            //override damage function with the ranger's armor and total attribute of their damage modifier attribute
            return Math.Round((EquippedWeapon==null ? 1 : EquippedWeapon.Damage) *
                (1 + ((double)GetTotalAttributes().Dexterity / 100)),2);
        }
    }
}
