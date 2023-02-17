using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG;
using RPG.Hero_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TDDRPGTest.Hero_Classes
{
    public class WarriorTests
    {
        #region construction and level tests

        [Fact]
        public void When_CreatingNewWarrior_Expect_KeepNameFromParameter()
        {
            //Arrange-Act
            string name = "Forest Warrior :)";
            Warrior warrior = new(name);

            //Assert
            Assert.Equal(warrior.Name, name);
        }
        [Fact]
        public void When_CreatingNewWarrior_Expect_StartLevelToBeEqualToOne()
        {
            //Arrange-Act
            Warrior warrior = new("");
            int expectedLevel = 1;

            //Assert
            Assert.Equal(expectedLevel, warrior.Level);
        }

        [Theory]
        [InlineData(1, 5, 2, 1)]
        [InlineData(2, 8, 4, 2)]
        [InlineData(3, 11, 6, 3)]
        [InlineData(10, 32, 20, 10)]
        public void When_LevelingUp_Expect_AttributesToIncreaseBySpecifiedAmount(int finalLevel, int finalStrength, int finalDextarity, int finalIntelligence)
        {
            //Arrange
            Warrior warrior = new("");
            int timesLevelingUp = finalLevel - 1;

            //Act
            warrior.LevelUp(timesLevelingUp);

            //Assert            
            Assert.Equal(warrior.CurrentAttribute,
                new HeroAttribute(finalStrength, finalDextarity, finalIntelligence));
        }
        #endregion

        #region weapon equip tests
        [Theory]
        [InlineData("", 1, 1, WeaponType.Swords)]
        [InlineData("", 1, 1, WeaponType.Axes)]
        [InlineData("", 1, 1, WeaponType.Hammers)]
        public void When_TryingToEquipAWeaponThatWarriorCanWield_Expect_WeaponShouldBeEquipped(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Warrior warrior = new("");

            //Act
            warrior.Equip(weapon);

            //Assert
            Assert.Equal(weapon, warrior.EquippedWeapon);
        }

        [Theory]
        [InlineData("", 1, 1, WeaponType.Staffs)]//wrong weapon types
        [InlineData("", 1, 1, WeaponType.Wands)]
        [InlineData("", 1, 1, WeaponType.Bows)]
        [InlineData("", 1, 10, WeaponType.Daggers)]//too high level

        public void When_TryingToEquipAWeaponThatWarriorCanNotWield_Expect_InvalidWeaponException(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Warrior warrior = new("");

            //Act - Assert
            Assert.Throws<InvalidWeaponException>(() => warrior.Equip(weapon));
        }

        [Fact]
        public void When_EquipingAWeaponWhileAnWeaponIsEquipped_Expect_DisarmOldWeaponAndOnlyCarryTheNewOne()
        {
            //assign
            Weapon weapon1 = new("", 1, 1, WeaponType.Hammers);
            Weapon weapon2 = new("", 1, 1, WeaponType.Axes);
            Warrior warrior = new("");

            //act
            warrior.Equip(weapon1);
            warrior.Equip(weapon2);

            //Assert
            Assert.Equal(weapon2, warrior.EquippedWeapon);
        }
        #endregion

        #region get damage tests

        [Fact]
        public void When_GettingDamageWithNoWeapon_Expect_BaseDamage()
        {
            //Assign
            Warrior warrior = new("");
            double expectedDamage = 1.05;
            double damage;

            //Act
            damage = warrior.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damage);
        }

        [Fact]
        public void When_GettingDamageWithAWeaponEquipped_Expect_DamageBasedOfStatAndWeapon()
        {
            //Assign
            Warrior warrior = new("");
            Weapon weapon = new Weapon("", 10, 1, WeaponType.Swords);
            double damageDealt;
            double expectedDamage = 10.5;

            //Act
            warrior.Equip(weapon);
            damageDealt = warrior.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }

        [Fact]
        public void When_GettingDamageAfterWeaponHaveBeenReplaced_Expect_DamageBasedOfTheNewWeaponAndBaseStats()
        {
            //Assign
            Warrior warrior = new("");
            Weapon weapon1 = new Weapon("first", -10, 1, WeaponType.Swords);
            Weapon weapon2 = new Weapon("second", 10, 1, WeaponType.Swords);
            double expectedDamage = 10.5;
            double damageDealt;

            //Act
            warrior.Equip(weapon1);
            warrior.Equip(weapon2);
            damageDealt = warrior.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        [Fact]
        public void When_GettingDamageWithArmorEquiped_Expect_ShouldReturnDamageFromWeaponAndBaseStatsOnly()
        {
            //Assign
            Warrior warrior = new("");
            Weapon weapon1 = new Weapon("", 100, 1, WeaponType.Swords);
            Armor head = new("", new HeroAttribute(10, 10, 10), ArmorType.Plate, 1, EquipmentSlot.Head);
            Armor body = new("", new HeroAttribute(10, 10, 10), ArmorType.Plate, 1, EquipmentSlot.Body);
            Armor legs = new("", new HeroAttribute(10, 10, 10), ArmorType.Plate, 1, EquipmentSlot.Legs);
            double expectedDamage = 135; //1,05 from base, 1,35 with armor, muliplied with 100 from weapons => 135
            double damageDealt;

            //Act
            warrior.Equip(weapon1);
            warrior.Equip(head);
            warrior.Equip(body);
            warrior.Equip(legs);
            damageDealt = warrior.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        #endregion

        #region armor equip tests

        [Theory]
        [InlineData("", 23, ArmorType.Mail, 1, EquipmentSlot.Body)]
        [InlineData("", 2, ArmorType.Plate, 1, EquipmentSlot.Body)]

        public void When_EquippingAnArmorThatTheClassCanEquip_Expect_ArmorEquiped(string name, int attributeValue, ArmorType armorType, int requiredLevel, EquipmentSlot slot)
        {
            //Assign
            Armor armor = new(name, new HeroAttribute(attributeValue), armorType, requiredLevel, slot);
            Warrior warrior = new("");

            //Act
            warrior.Equip(armor);

            //Assert
            Assert.Equal(warrior.EquippedArmor[slot], armor);
        }

        [Theory]
        [InlineData(1, ArmorType.Leather)]//wrong material
        [InlineData(15, ArmorType.Plate)]//too high level
        public void When_TryingToEquipAnArmorThatTheClassCanNotEquip_Expect_InvalidArmorException(int requiredLevel, ArmorType armorType)
        {
            //Assign
            Armor armor = new("", new HeroAttribute(10), armorType, requiredLevel, EquipmentSlot.Head);
            Warrior warrior = new("");

            //Act - Assert
            Assert.Throws<InvalidArmorException>(() => warrior.Equip(armor));
        }

        [Theory]
        [InlineData(EquipmentSlot.Head)]
        [InlineData(EquipmentSlot.Body)]
        [InlineData(EquipmentSlot.Legs)]
        public void When_EquippingTwoArmorInTheSameSlot_Expect_TheFirstOneToBeReplacedByTheSecondOne(EquipmentSlot slot)
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Plate, 1, slot);
            Armor armor2 = new("two", new HeroAttribute(3, 2, 1), ArmorType.Plate, 1, slot);
            Warrior warrior = new("");

            //act
            warrior.Equip(armor1);
            warrior.Equip(armor2);

            //Assert
            Assert.Equal(armor2, warrior.EquippedArmor[slot]);
        }
        #endregion

        #region attributs
        [Fact]
        public void When_GetAttributeWithOneArmor_Expect_AttributeFromBaseAndOneArmor()
        {
            //assign
            Armor armor = new("", new HeroAttribute(1, 2, 3), ArmorType.Plate, 1, EquipmentSlot.Body);
            Warrior warrior = new("");
            HeroAttribute expectedAttribute = new(6, 4, 4);
            HeroAttribute actualAttribute;

            //act
            warrior.Equip(armor);
            actualAttribute = warrior.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithTwoArmorItems_Expect_AttributeFromBaseAndTwoArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Plate, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Mail, 1, EquipmentSlot.Body);
            Warrior warrior = new("");
            HeroAttribute expectedAttribute = new(10, 9, 10);
            HeroAttribute actualAttribute;

            //act
            warrior.Equip(armor1);
            warrior.Equip(armor2);
            actualAttribute = warrior.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithThreeArmorItems_Expect_AttributeFromBaseAndThreeArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Plate, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Plate, 1, EquipmentSlot.Body);
            Armor armor3 = new("three", new HeroAttribute(7, 8, 9), ArmorType.Plate, 1, EquipmentSlot.Legs);
            Warrior warrior = new("");
            HeroAttribute expectedAttribute = new(17, 17, 19);
            HeroAttribute actualAttribute;
            //act
            warrior.Equip(armor1);
            warrior.Equip(armor2);
            warrior.Equip(armor3);
            actualAttribute = warrior.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }
        #endregion

        #region display state tests 

        [Fact]
        public void DisplayState_DisplayingState_ShouldDisplayState()
        {
            //Assign
            Warrior warrior = new("Steve");
            string displayState;
            //state at level 1
            string expectedState =
                "Name: Steve" + '\n' +
                "Class: Warrior" + '\n' +
                "Level: 1" + '\n' +
                "Total strength: 5" + '\n' +
                "Total dexterity 2" + '\n' +
                "Total intelligence 1" + '\n' +
                "Damage: 1,05" + '\n';

            //Act
            displayState = warrior.DisplayState();

            //Assert
            Assert.Equal(displayState, expectedState);
        }
        #endregion
    }
}
