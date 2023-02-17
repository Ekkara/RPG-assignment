using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG;
using RPG.Hero_Components;

namespace TDDRPGTest.Hero_Classes
{
    public class RogueTests
    {
        #region construction and level tests

        [Fact]
        public void When_CreatingNewRogue_Expect_KeepNameFromParameter()
        {
            //Arrange-Act
            string name = "Forest Rogue :)";
            Rogue rogue = new(name);

            //Assert
            Assert.Equal(rogue.Name, name);
        }
        [Fact]
        public void When_CreatingNewRogue_Expect_StartLevelToBeEqualToOne()
        {
            //Arrange-Act
            Rogue rogue = new("");
            int expectedLevel = 1;

            //Assert
            Assert.Equal(expectedLevel, rogue.Level);
        }

        [Theory]
        [InlineData(1, 2, 6, 1)]
        [InlineData(2, 3, 10, 2)]
        [InlineData(3, 4, 14, 3)]
        [InlineData(10, 11, 42, 10)]
        public void When_LevelingUp_Expect_AttributesToIncreaseBySpecifiedAmount(int finalLevel, int finalStrength, int finalDextarity, int finalIntelligence)
        {
            //Arrange
            Rogue rogue = new("");
            int timesLevelingUp = finalLevel - 1;

            //Act
            rogue.LevelUp(timesLevelingUp);


            //Assert            
            Assert.Equal(rogue.CurrentAttribute,
                new HeroAttribute(finalStrength, finalDextarity, finalIntelligence));
        }
        #endregion

        #region weapon equip tests
        [Theory]
        [InlineData("", 1, 1, WeaponType.Swords)]
        [InlineData("", 1, 1, WeaponType.Daggers)]
        public void When_TryingToEquipAWeaponThatRogueCanWield_Expect_WeaponShouldBeEquipped(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Rogue rogue = new("");

            //Act
            rogue.Equip(weapon);

            //Assert
            Assert.Equal(weapon, rogue.EquippedWeapon);
        }

        [Theory]
        [InlineData("", 1, 1, WeaponType.Axes)] //wrong weapon types
        [InlineData("", 1, 1, WeaponType.Hammers)]
        [InlineData("", 1, 1, WeaponType.Staffs)]
        [InlineData("", 1, 1, WeaponType.Wands)]
        [InlineData("", 1, 1, WeaponType.Bows)]
        [InlineData("", 1, 10, WeaponType.Daggers)]//too high level

        public void When_TryingToEquipAWeaponThatRogueCanNotWield_Expect_InvalidWeaponException(string name, double damage, int requiredLevel, WeaponType weaponType)
        {
            //Assign
            Weapon weapon = new(name, damage, requiredLevel, weaponType);
            Rogue rogue = new("");

            //Act - Assert
            Assert.Throws<InvalidWeaponException>(() => rogue.Equip(weapon));
        }

        [Fact]
        public void When_EquipingAWeaponWhileAnWeaponIsEquipped_Expect_DisarmOldWeaponAndOnlyCarryTheNewOne()
        {
            //assign
            Weapon weapon1 = new("", 1, 1, WeaponType.Swords);
            Weapon weapon2 = new("", 1, 1, WeaponType.Daggers);
            Rogue rogue = new("");

            //act
            rogue.Equip(weapon1);
            rogue.Equip(weapon2);

            //Assert
            Assert.Equal(weapon2, rogue.EquippedWeapon);
        }
        #endregion

        #region get damage tests

        [Fact]
        public void When_GettingDamageWithNoWeapon_Expect_BaseDamage()
        {
            //Assign
            Rogue rogue = new("");
            double expectedDamage = 1.06;
            double damage;

            //Act
            damage = rogue.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damage);
        }

        [Fact]
        public void When_GettingDamageWithAWeaponEquipped_Expect_DamageBasedOfStatAndWeapon()
        {
            //Assign
            Rogue rogue = new("");
            Weapon weapon = new Weapon("", 10, 1, WeaponType.Swords);
            double damageDealt;
            double expectedDamage = 10.6;

            //Act
            rogue.Equip(weapon);
            damageDealt = rogue.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }

        [Fact]
        public void When_GettingDamageAfterWeaponHaveBeenReplaced_Expect_DamageBasedOfTheNewWeaponAndBaseStats()
        {
            //Assign
            Rogue rogue = new("");
            Weapon weapon1 = new Weapon("first", -10, 1, WeaponType.Swords);
            Weapon weapon2 = new Weapon("second", 10, 1, WeaponType.Swords);
            double expectedDamage = 10.6;
            double damageDealt;

            //Act
            rogue.Equip(weapon1);
            rogue.Equip(weapon2);
            damageDealt = rogue.GetDamage();

            //Assert
            Assert.Equal(expectedDamage, damageDealt);
        }
        [Fact]
        public void When_GettingDamageWithArmorEquiped_Expect_ShouldReturnDamageFromWeaponAndBaseStatsOnly()
        {
            //Assign
            Rogue rogue = new("");
            Weapon weapon1 = new Weapon("", 100, 1, WeaponType.Swords);
            Armor head = new("", new HeroAttribute(10, 10, 10), ArmorType.Mail, 1, EquipmentSlot.Head);
            Armor body = new("", new HeroAttribute(10, 10, 10), ArmorType.Mail, 1, EquipmentSlot.Body);
            Armor legs = new("", new HeroAttribute(10, 10, 10), ArmorType.Mail, 1, EquipmentSlot.Legs);
            double expectedDamage = 136; //1,06 from base, 1,36 with armor, muliplied with 100 from weapons => 136
            double damageDealt;

            //Act
            rogue.Equip(weapon1);
            rogue.Equip(head);
            rogue.Equip(body);
            rogue.Equip(legs);
            damageDealt = rogue.GetDamage();

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
            Rogue rogue = new("");

            //Act
            rogue.Equip(armor);

            //Assert
            Assert.Equal(rogue.EquippedArmor[slot], armor);
        }

        [Theory]
        [InlineData(1, ArmorType.Plate)]//wrong material
        [InlineData(15, ArmorType.Cloth)]//too high level
        public void When_TryingToEquipAnArmorThatTheClassCanNotEquip_Expect_InvalidArmorException(int requiredLevel, ArmorType armorType)
        {
            //Assign
            Armor armor = new("", new HeroAttribute(10), armorType, requiredLevel, EquipmentSlot.Head);
            Rogue rogue = new("");

            //Act - Assert
            Assert.Throws<InvalidArmorException>(() => rogue.Equip(armor));
        }

        [Theory]
        [InlineData(EquipmentSlot.Head)]
        [InlineData(EquipmentSlot.Body)]
        [InlineData(EquipmentSlot.Legs)]
        public void When_EquippingTwoArmorInTheSameSlot_Expect_TheFirstOneToBeReplacedByTheSecondOne(EquipmentSlot slot)
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Mail, 1, slot);
            Armor armor2 = new("two", new HeroAttribute(3, 2, 1), ArmorType.Leather, 1, slot);
            Rogue rogue = new("");

            //act
            rogue.Equip(armor1);
            rogue.Equip(armor2);

            //Assert
            Assert.Equal(armor2, rogue.EquippedArmor[slot]);
        }
        #endregion

        #region get attributes tests
        [Fact]
        public void When_GetAttributeWithOneArmor_Expect_AttributeFromBaseAndOneArmor()
        {
            //assign
            Armor armor = new("", new HeroAttribute(1, 2, 3), ArmorType.Leather, 1, EquipmentSlot.Body);
            Rogue rogue = new("");
            HeroAttribute expectedAttribute = new(3, 8, 4);
            HeroAttribute actualAttribute;

            //act
            rogue.Equip(armor);
            actualAttribute = rogue.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }

        [Fact]
        public void When_GetAttributeWithTwoArmorItems_Expect_AttributeFromBaseAndTwoArmor()
        {
            //assign
            Armor armor1 = new("one", new HeroAttribute(1, 2, 3), ArmorType.Mail, 1, EquipmentSlot.Head);
            Armor armor2 = new("two", new HeroAttribute(4, 5, 6), ArmorType.Mail, 1, EquipmentSlot.Body);
            Rogue rogue = new("");
            HeroAttribute expectedAttribute = new(7, 13, 10);
            HeroAttribute actualAttribute;

            //act
            rogue.Equip(armor1);
            rogue.Equip(armor2);
            actualAttribute = rogue.GetTotalAttributes();

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
            Rogue rogue = new("");
            HeroAttribute expectedAttribute = new(14, 21, 19);
            HeroAttribute actualAttribute;
            //act
            rogue.Equip(armor1);
            rogue.Equip(armor2);
            rogue.Equip(armor3);
            actualAttribute = rogue.GetTotalAttributes();

            //Assert
            Assert.Equal(actualAttribute, expectedAttribute);
        }
        #endregion

        #region display state test 

        [Fact]
        public void DisplayState_DisplayingState_ShouldDisplayState()
        {
            //Assign
            Rogue rogue = new("Steve");
            string displayState;
            //state at level 1
            string expectedState =
                "Name: Steve" + '\n' +
                "Class: Rogue" + '\n' +
                "Level: 1" + '\n' +
                "Total strength: 2" + '\n' +
                "Total dexterity 6" + '\n' +
                "Total intelligence 1" + '\n' +
                "Damage: 1,06" + '\n';

            //Act
            displayState = rogue.DisplayState();

            //Assert
            Assert.Equal(displayState, expectedState);
        }
        #endregion
    }
}
