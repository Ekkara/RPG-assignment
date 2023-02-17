using RPG;
using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG.Hero_Components;

namespace TDDRPGTest.Hero_Classes
{
    public class MageTests
    {
        #region construction and level tests
        //can generate a mage
        [Fact]
        public void When_CreatingNewMage_Expect_KeepNameFromParameter()
        {
            //Arrange-Act
            string name = "Great wizard of oz";
            Mage mage = new(name);

            //Assert
            Assert.Equal(mage.Name, name);
        }
        [Fact]
        public void When_CreatingNewMage_Expect_StartLevelToBeEqualToOne()
        {
            //Arrange-Act
            Mage mage = new("");
            int expectedLevel = 1;

            //Assert
            Assert.Equal(expectedLevel, mage.Level);
        }

        [Theory]
        [InlineData(1, 1, 1, 8)]
        [InlineData(2, 2, 2, 13)]
        [InlineData(3, 3, 3, 18)]
        [InlineData(10, 10, 10, 53)]
        public void When_LevelingUp_Expect_AttributesToIncreaseBySpecifiedAmount(int finalLevel, int finalStrength, int finalDextarity, int finalIntelligence)
        {
            //Arrange
            Mage mage = new("Steve the dyslexic");
            int timesLevelingUp = finalLevel - 1;

            //Act
            mage.LevelUp(timesLevelingUp);

            //Assert            
            Assert.Equal(mage.CurrentAttribute,
                new HeroAttribute(finalStrength, finalDextarity, finalIntelligence));
        }
        #endregion

        #region weapon equip tests
        [Theory]
        [InlineData("", 1, 1, WeaponType.Wands)]
        [InlineData("", 1, 1, WeaponType.Staffs)]
        public void When_TryingToEquipAWeaponThatMagesCanWield_Expect_WeaponShouldBeEquipped(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Mage mage = new("");

            //Act
            mage.Equip(weapon);

            //Assert
            Assert.Equal(weapon, mage.EquippedWeapon);
        }
        [Theory]
        [InlineData("", 1, 1, WeaponType.Axes)] //wrong weapon types
        [InlineData("", 1, 1, WeaponType.Bows)]
        [InlineData("", 1, 1, WeaponType.Daggers)]
        [InlineData("", 1, 1, WeaponType.Hammers)]
        [InlineData("", 1, 1, WeaponType.Swords)]
        [InlineData("", 1, 10, WeaponType.Staffs)] //too high level
        public void When_TryingToEquipAWeaponThatMagesCanNotWield_Expect_InvalidWeaponException(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Mage mage = new("");

            //Act - Assert
            Assert.Throws<InvalidWeaponException>(() => mage.Equip(weapon));
        }

        [Fact]
        public void When_EquipingAWeaponWhileAnWeaponIsEquipped_Expect_DisarmOldWeaponAndOnlyCarryTheNewOne()
        {
            //assign
            Weapon weapon1 = new("", 1, 1, WeaponType.Staffs);
            Weapon weapon2 = new("", 1, 1, WeaponType.Staffs);
            Mage mage = new("");

            //act
            mage.Equip(weapon1);
            mage.Equip(weapon2);

            //Assert
            Assert.Equal(weapon2, mage.EquippedWeapon);
        }
        #endregion

        #region get damage tests

        [Fact]
        public void When_GettingDamageWithNoWeapon_Expect_BaseDamage()
        {
            //Assign
            Mage mage = new("");
            double damage;

            //Act
            damage = mage.GetDamage();

            //Assert
            Assert.Equal(1.08, damage);
        }

        [Fact]
        public void When_GettingDamageWithAWeaponEquipped_Expect_DamageBasedOfStatAndWeapon()
        {
            //Assign
            Mage mage = new("");
            Weapon weapon = new Weapon("", 10, 1, WeaponType.Staffs);
            double damageDealt;
            double expectedDamage = 10.8;

            //Act
            mage.Equip(weapon);
            damageDealt = mage.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }

        [Fact]
        public void When_GettingDamageAfterWeaponHaveBeenReplaced_Expect_DamageBasedOfTheNewWeaponAndBaseStats()
        {
            //Assign
            Mage mage = new("");
            Weapon weapon1 = new Weapon("first", -10, 1, WeaponType.Staffs);
            Weapon weapon2 = new Weapon("second", 10, 1, WeaponType.Staffs);
            double expectedDamage = 10.8;
            double damageDealt;

            //Act
            mage.Equip(weapon1);
            mage.Equip(weapon2);
            damageDealt = mage.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        [Fact]
        public void When_GettingDamageWithArmorEquiped_Expect_ShouldReturnDamageFromWeaponAndBaseStatsOnly()
        {
            //Assign
            Mage mage = new("");
            Weapon weapon1 = new Weapon("", 100, 1, WeaponType.Staffs);
            Armor head = new("", new HeroAttribute(10, 10, 10), ArmorType.Cloth, 1, EquipmentSlot.Head);
            Armor body = new("", new HeroAttribute(10, 10, 10), ArmorType.Cloth, 1, EquipmentSlot.Body);
            Armor legs = new("", new HeroAttribute(10, 10, 10), ArmorType.Cloth, 1, EquipmentSlot.Legs);
            double expectedDamage = 138; //1,08 from base, 1,38 with armor, muliplied with 100 from weapons => 138
            double damageDealt;

            //Act
            mage.Equip(weapon1);
            mage.Equip(head);
            mage.Equip(body);
            mage.Equip(legs);
            damageDealt = mage.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        #endregion

        #region armor equip tests

        [Theory]
        [InlineData("", 10, ArmorType.Cloth, 1, EquipmentSlot.Head)]
        [InlineData("", 23, ArmorType.Cloth, 1, EquipmentSlot.Body)]
        public void When_EquippingAnArmorThatTheClassCanEquip_Expect_ArmorEquiped(string name, int attributeValue, ArmorType armorType, int requiredLevel, EquipmentSlot slot)
        {
            //Assign
            Armor armor = new(name, new HeroAttribute(attributeValue), armorType, requiredLevel, slot);
            Mage mage = new("");

            //Act
            mage.Equip(armor);

            //Assert
            Assert.Equal(mage.EquippedArmor[slot], armor);
        }

        [Theory]
        [InlineData(1, ArmorType.Plate)]//wrong material
        [InlineData(1, ArmorType.Mail)]
        [InlineData(1, ArmorType.Leather)]
        [InlineData(15, ArmorType.Cloth)]//too high level
        public void When_TryingToEquipAnArmorThatTheClassCanNotEquip_Expect_InvalidArmorException(int requiredLevel, ArmorType armorType)
        {
            //Assign
            Armor armor = new("", new HeroAttribute(10), armorType, requiredLevel, EquipmentSlot.Head);
            Mage mage = new("");

            //Act - Assert
            Assert.Throws<InvalidArmorException>(() => mage.Equip(armor));
        }

        [Theory]
        [InlineData(EquipmentSlot.Head)]
        [InlineData(EquipmentSlot.Body)]
        [InlineData(EquipmentSlot.Legs)]
        public void When_EquippingTwoArmorInTheSameSlot_Expect_TheFirstOneToBeReplacedByTheSecondOne(EquipmentSlot slot)
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1), ArmorType.Cloth, 1, slot);
            Armor armor2 = new("two", new HeroAttribute(2), ArmorType.Cloth, 1, slot);
            Mage mage = new("");

            //act
            mage.Equip(armor1);
            mage.Equip(armor2);

            //Assert
            Assert.Equal(armor2, mage.EquippedArmor[slot]);
        }
        #endregion

        #region get attribute tests
        [Fact]
        public void When_GetAttributeWithOneArmor_Expect_AttributeFromBaseAndOneArmor()
        {
            //assign
            Armor armor = new("", new HeroAttribute(1, 2, 3), ArmorType.Cloth, 1, EquipmentSlot.Body);
            Mage mage = new("");
            HeroAttribute expectedAttribute = new(2, 3, 11);
            HeroAttribute actualAttribute;

            //act
            mage.Equip(armor);
            actualAttribute = mage.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithTwoArmorItems_Expect_AttributeFromBaseAndTwoArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Cloth, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Cloth, 1, EquipmentSlot.Body);
            Mage mage = new("");
            HeroAttribute expectedAttribute = new(6, 8, 17);
            HeroAttribute actualAttribute;

            //act
            mage.Equip(armor1);
            mage.Equip(armor2);
            actualAttribute = mage.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithThreeArmorItems_Expect_AttributeFromBaseAndThreeArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Cloth, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Cloth, 1, EquipmentSlot.Body);
            Armor armor3 = new("three", new HeroAttribute(7, 8, 9), ArmorType.Cloth, 1, EquipmentSlot.Legs);
            Mage mage = new("");
            HeroAttribute expectedAttribute = new(13, 16, 26);
            HeroAttribute actualAttribute;
            //act
            mage.Equip(armor1);
            mage.Equip(armor2);
            mage.Equip(armor3);
            actualAttribute = mage.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }
        #endregion

        #region display state test 

        [Fact]
        public void DisplayState_DisplayingState_ShouldDisplayState()
        {
            //Assign
            Mage mage = new("Steve");
            string displayState;
            //state at level 1
            string expectedState =
                "Name: Steve" + '\n' +
                "Class: Mage" + '\n' +
                "Level: 1" + '\n' +
                "Total strength: 1" + '\n' +
                "Total dexterity 1" + '\n' +
                "Total intelligence 8" + '\n' +
                "Damage: 1,08" + '\n';

            //Act
            displayState = mage.DisplayState();


            //Assert
            Assert.Equal(displayState, expectedState);
        }
        #endregion
    }
}
