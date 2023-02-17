using RPG;
using RPG.Hero_Components;
using RPG.Equipment_Components;
using RPG.Custom_Exceptions;

namespace TDDRPGTest.Item
{
    public class ArmorTests
    {
        //gives error if trying to use a armor in the weapon slot
        [Fact]
        public void When_InitializingArmorAsWeapon_Expect_ShouldCastException()
        {
            //Assign-Act-Assert
            Assert.Throws<InvalidArmorException>(() =>
            {
                Armor armor = new("Targe of the Blooded", new HeroAttribute(), ArmorType.Leather, 1, EquipmentSlot.Weapon);
            });
        }

        [Fact]
        public void When_InitializedAnArmor_Expect_ArmorIsInitializedWithCorrectName()
        {
            //Assign-act
            string name = "wheel of cheese";
            Armor armor = new(name, new(), ArmorType.Plate, 1, EquipmentSlot.Head);

            //Assert
            Assert.Equal(name, armor.Name);
        }
        [Fact]
        public void When_InitializedAnArmor_Expect_ArmorIsInitializedWithCorrectAttribute()
        {
            //Assign-act
            HeroAttribute heroAttribute = new HeroAttribute(1,2,3);
            Armor armor = new("", heroAttribute, ArmorType.Plate, 1, EquipmentSlot.Head);

            //Assert
            Assert.Equal(heroAttribute, armor.AttributeModifier);
        }
        [Fact]
        public void When_InitializedAnArmor_Expect_ArmorIsInitializedWithCorrectRequiredLevel()
        {
            //Assign-act
            int reqLevel = 402;
            Armor armor = new("", new(), ArmorType.Plate, reqLevel, EquipmentSlot.Head);

            //Assert
            Assert.Equal(reqLevel, armor.RequiredLevel);
        }
        [Fact]
        public void When_InitializedAnArmor_Expect_ArmorIsInitializedWithCorrectArmorType()
        {
            //Assign-act
            ArmorType armorType = ArmorType.Plate;
            Armor armor = new("", new(), armorType, 1, EquipmentSlot.Head);

            //Assert
            Assert.Equal(armorType, armor.ArmorType);
        }

        //idk how this could ever go wrong
        [Fact]
        public void When_InitializedAnArmor_Expect_ArmorIsInitializedWithCorrectEquipmentSlot()
        {
            //Assign - Act
            EquipmentSlot slot = EquipmentSlot.Body;
            Armor armor = new Armor("", new HeroAttribute(), ArmorType.Plate, 1, slot);

            //Assert
            Assert.Equal(armor.Slot, slot);
        }
    }
}
