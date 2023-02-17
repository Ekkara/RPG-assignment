using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG;
using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDRPGTest.Hero_Classes
{
    public class RangerTests
    {
        #region construction and level tests

        [Fact]
        public void When_CreatingNewRanger_Expect_KeepNameFromParameter()
        {
            //Arrange-Act
            string name = "Forest Ranger :)";
            Ranger ranger = new(name);

            //Assert
            Assert.Equal(ranger.Name, name);
        }
        [Fact]
        public void When_CreatingNewRanger_Expect_StartLevelToBeEqualToOne()
        {
            //Arrange-Act
            Ranger ranger = new("");
            int expectedLevel = 1;

            //Assert
            Assert.Equal(expectedLevel, ranger.Level);
        }

        [Theory]
        [InlineData(1, 1, 7, 1)]
        [InlineData(2, 2, 12, 2)]
        [InlineData(3, 3, 17, 3)]
        [InlineData(10, 10, 52, 10)]
        public void When_LevelingUp_Expect_AttributesToIncreaseBySpecifiedAmount(int finalLevel, int finalStrength, int finalDextarity, int finalIntelligence)
        {
            //Arrange
            Ranger ranger = new("");
            int timesLevelingUp = finalLevel - 1;

            //Act
            ranger.LevelUp(timesLevelingUp);

            //Assert            
            Assert.Equal(ranger.CurrentAttribute,
                new HeroAttribute(finalStrength, finalDextarity, finalIntelligence));
        }
        #endregion

        #region weapon equip tests
        [Theory]
        [InlineData("", 1, 1, WeaponType.Bows)]
        public void When_TryingToEquipAWeaponThatRangerCanWield_Expect_WeaponShouldBeEquipped(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Ranger ranger = new("");

            //Act
            ranger.Equip(weapon);

            //Assert
            Assert.Equal(weapon, ranger.EquippedWeapon);
        }

        [Theory]
        [InlineData("", 1, 1, WeaponType.Axes)] //wrong weapon types
        [InlineData("", 1, 1, WeaponType.Daggers)]
        [InlineData("", 1, 1, WeaponType.Hammers)]
        [InlineData("", 1, 1, WeaponType.Swords)]
        [InlineData("", 1, 1, WeaponType.Staffs)]
        [InlineData("", 1, 1, WeaponType.Wands)]
        [InlineData("", 1, 10, WeaponType.Bows)]//too high level

        public void When_TryingToEquipAWeaponThatRangerCanNotWield_Expect_InvalidWeaponException(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Ranger ranger = new("");

            //Act - Assert
            Assert.Throws<InvalidWeaponException>(() => ranger.Equip(weapon));
        }

        [Fact]
        public void When_EquipingAWeaponWhileAnWeaponIsEquipped_Expect_DisarmOldWeaponAndOnlyCarryTheNewOne()
        {
            //assign
            Weapon weapon1 = new("", 1, 1, WeaponType.Bows);
            Weapon weapon2 = new("", 1, 1, WeaponType.Bows);
            Ranger ranger = new("");

            //act
            ranger.Equip(weapon1);
            ranger.Equip(weapon2);

            //Assert
            Assert.Equal(weapon2, ranger.EquippedWeapon);
        }
        #endregion

        #region get damage tests

        [Fact]
        public void When_GettingDamageWithNoWeapon_Expect_BaseDamage()
        {
            //Assign
            Ranger ranger = new("");
            double expectedDamage = 1.07;
            double damage;

            //Act
            damage = ranger.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damage);
        }

        [Fact]
        public void When_GettingDamageWithAWeaponEquipped_Expect_DamageBasedOfStatAndWeapon()
        {
            //Assign
            Ranger ranger = new("");
            Weapon weapon = new Weapon("", 10, 1, WeaponType.Bows);
            double damageDealt;
            double expectedDamage = 10.7;

            //Act
            ranger.Equip(weapon);
            damageDealt = ranger.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }

        [Fact]
        public void When_GettingDamageAfterWeaponHaveBeenReplaced_Expect_DamageBasedOfTheNewWeaponAndBaseStats()
        {
            //Assign
            Ranger ranger = new("");
            Weapon weapon1 = new Weapon("first", -10, 1, WeaponType.Bows);
            Weapon weapon2 = new Weapon("second", 10, 1, WeaponType.Bows);
            double expectedDamage = 10.7;
            double damageDealt;

            //Act
            ranger.Equip(weapon1);
            ranger.Equip(weapon2);
            damageDealt = ranger.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        [Fact]
        public void When_GettingDamageWithArmorEquiped_Expect_ShouldReturnDamageFromWeaponAndBaseStatsOnly()
        {
            //Assign
            Ranger ranger = new("");
            Weapon weapon1 = new Weapon("", 100, 1, WeaponType.Bows);
            Armor head = new("", new HeroAttribute(10, 10, 10), ArmorType.Mail, 1, EquipmentSlot.Head);
            Armor body = new("", new HeroAttribute(10, 10, 10), ArmorType.Mail, 1, EquipmentSlot.Body);
            Armor legs = new("", new HeroAttribute(10, 10, 10), ArmorType.Mail, 1, EquipmentSlot.Legs);
            double expectedDamage = 137; //1,07 from base, 1,37 with armor, muliplied with 100 from weapons => 137 
            double damageDealt;

            //Act
            ranger.Equip(weapon1);
            ranger.Equip(head);
            ranger.Equip(body);
            ranger.Equip(legs);
            damageDealt = ranger.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        #endregion

        #region armor equip tests

        [Theory]
        [InlineData("", 23, ArmorType.Mail, 1, EquipmentSlot.Body)]
        [InlineData("", 2, ArmorType.Leather, 1, EquipmentSlot.Body)]

        public void When_EquippingAnArmorThatTheClassCanEquip_Expect_ArmorEquiped(string name, int attributeValue, ArmorType armorType, int requiredLevel, EquipmentSlot slot)
        {
            //Assign
            Armor armor = new(name, new HeroAttribute(attributeValue), armorType, requiredLevel, slot);
            Ranger ranger = new("");

            //Act
            ranger.Equip(armor);

            //Assert
            Assert.Equal(ranger.EquippedArmor[slot], armor);
        }

        [Theory]
        [InlineData(1, ArmorType.Plate)]//wrong material
        [InlineData(15, ArmorType.Cloth)]//too high level
        public void When_TryingToEquipAnArmorThatTheClassCanNotEquip_Expect_InvalidArmorException(int requiredLevel, ArmorType armorType)
        {
            //Assign
            Armor armor = new("", new HeroAttribute(10), armorType, requiredLevel, EquipmentSlot.Head);
            Ranger ranger = new("");

            //Act - Assert
            Assert.Throws<InvalidArmorException>(() => ranger.Equip(armor));
        }

        [Theory]
        [InlineData(EquipmentSlot.Head)]
        [InlineData(EquipmentSlot.Body)]
        [InlineData(EquipmentSlot.Legs)]
        public void When_EquippingTwoArmorInTheSameSlot_Expect_TheFirstOneToBeReplacedByTheSecondOne(EquipmentSlot slot)
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1,2,3), ArmorType.Mail, 1, slot);
            Armor armor2 = new("two", new HeroAttribute(3,2,1), ArmorType.Leather, 1, slot);
            Ranger ranger = new("");

            //act
            ranger.Equip(armor1);
            ranger.Equip(armor2);

            //Assert
            Assert.Equal(armor2, ranger.EquippedArmor[slot]);
        }
        #endregion

        #region attributes
        [Fact]
        public void When_GetAttributeWithOneArmor_Expect_AttributeFromBaseAndOneArmor()
        {
            //assign
            Armor armor = new("", new HeroAttribute(1, 2, 3), ArmorType.Leather, 1, EquipmentSlot.Body);
            Ranger ranger = new("");
            HeroAttribute expectedAttribute = new(2, 9, 4);
            HeroAttribute actualAttribute;

            //act
            ranger.Equip(armor);
            actualAttribute = ranger.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithTwoArmorItems_Expect_AttributeFromBaseAndTwoArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Mail, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Mail, 1, EquipmentSlot.Body);
            Ranger ranger = new("");
            HeroAttribute expectedAttribute = new(6, 14, 10);
            HeroAttribute actualAttribute;

            //act
            ranger.Equip(armor1);
            ranger.Equip(armor2);
            actualAttribute = ranger.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithThreeArmorItems_Expect_AttributeFromBaseAndThreeArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Leather, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Leather, 1, EquipmentSlot.Body);
            Armor armor3 = new("three", new HeroAttribute(7, 8, 9), ArmorType.Leather, 1, EquipmentSlot.Legs);
            Ranger ranger = new("");
            HeroAttribute expectedAttribute = new(13, 22, 19);
            HeroAttribute actualAttribute;
            //act
            ranger.Equip(armor1);
            ranger.Equip(armor2);
            ranger.Equip(armor3);
            actualAttribute = ranger.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }
        #endregion

        #region display state tests 

        [Fact]
        public void DisplayState_DisplayingState_ShouldDisplayState()
        {
            //Assign
            Ranger ranger = new("Steve");
            string displayState;
            //state at level 1
            string expectedState =
                "Name: Steve" + '\n' +
                "Class: Ranger" + '\n' +
                "Level: 1" + '\n' +
                "Total strength: 1" + '\n' +
                "Total dexterity 7" + '\n' +
                "Total intelligence 1" + '\n' +
                "Damage: 1,07" + '\n';

            //Act
            displayState = ranger.DisplayState();

            //Assert
            Assert.Equal(displayState, expectedState);
        }
        #endregion
    }
}
