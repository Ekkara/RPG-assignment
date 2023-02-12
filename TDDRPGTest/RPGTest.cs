using RPG.Equipment_Components;
using RPG.Hero_Components;

namespace TDDRPGTest
{
    public class RPGTest
    {
        [Theory]
        [InlineData(1, 1, 1, 8)]
        [InlineData(2, 2, 2, 13)]
        [InlineData(3, 3, 3, 18)]
        public void LevelUp_LevelUpMage_ShouldReturnSameAttribute(int finalLevel, int finalStrength, int finalDextarity, int finalIntelligence)
        {
            //Arrange
            Mage mage = new("Steve the dyslexic");

            //Act
            for (int i = 0; i < finalLevel - 1; i++)
            {
                mage.LevelUp();
            }

            //Assert            
            Assert.Equal(mage.CurrentAttribute,
                new HeroAttribute(finalStrength, finalDextarity, finalIntelligence));
        }

        [Fact]
        public void EquipItem_EquipRightWeapon_ShouldEquipItem()
        {
            //Arrange
            Warrior warrior = new("Mighty Bill");
            Weapon weapon = new("Death Sun", 10, 1, WeaponType.Swords);

            //Act
            warrior.Equip(weapon);

            //Assert            
            Assert.Equal(warrior.EquippedWeapon?.Name, weapon.Name);
        }

        [Fact]
        public void EquipItem_EquipWrongWeapon_ShouldNotEquipItem()
        {
            //Arrange
            Warrior warrior = new("Mighty Bill");
            Weapon weapon = new("Magical stick", 10, 1, WeaponType.Staffs);

            //Act
            warrior.Equip(weapon);

            //Assert            
            Assert.NotEqual(warrior.EquippedWeapon?.Name, weapon.Name);
        }

        [Fact]
        public void EquipItem_EquipMultipleWeaponOverride_ShoulOverrideOldWeapon()
        {
            //Arrange
            Mage warrior = new("Collector");
            Weapon weapon1 = new("Magical stick", 10, 1, WeaponType.Staffs);
            Weapon weapon2 = new("Better magical stick", 100, 1, WeaponType.Staffs);

            //Act
            warrior.Equip(weapon1);
            warrior.Equip(weapon2);

            //Assert            
            Assert.Equal(warrior.EquippedWeapon?.Name, weapon2.Name);
        }

    }
}