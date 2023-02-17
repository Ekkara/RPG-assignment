using RPG.Custom_Exceptions;
using RPG.Equipment_Components;
using RPG.Hero_Components;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDDRPGTest.Item
{
    public class WeaponTests
    {
        [Fact]
        public void When_InitializingWeapon_Expect_ShouldBeOfItemTypeWeapon()
        {
            //Assign - act
            Weapon weapon = new("", 1, 1, WeaponType.Swords);

            //Assert 
            Assert.Equal(EquipmentSlot.Weapon, weapon.Slot);
        }

        [Fact]
        public void When_InitializedAWeapon_Expect_WeaponIsInitializedWithCorrectName()
        {
            //Assign-act
            string name = "wheel of cheese";
            Weapon weapon = new(name, 1, 1, WeaponType.Swords);

            //Assert
            Assert.Equal(name, weapon.Name);
        }
        [Fact]
        public void When_InitializedAWeapon_Expect_WeaponIsInitializedWithCorrectDamage()
        {
            //Assign-act
            double expectedDamade = 10;
            Weapon weapon = new("", expectedDamade, 1, WeaponType.Swords);

            //Assert
            Assert.Equal(expectedDamade, weapon.Damage);
        }
        [Fact]
        public void When_InitializedAWeapon_Expect_WeaponIsInitializedWithCorrectRequiredLevel()
        {
            //Assign-act
            int reqLevel = 402;
            Weapon weapon = new("", 1, reqLevel, WeaponType.Swords);

            //Assert
            Assert.Equal(reqLevel, weapon.RequiredLevel);
        }
        [Fact]
        public void When_InitializedAWeapon_Expect_WeaponIsInitializedWithCorrectWeaponType()
        {
            //Assign-act
            WeaponType expectedType = WeaponType.Swords;
            Weapon weapon = new("", 1, 1, expectedType);

            //Assert
            Assert.Equal(expectedType, weapon.WeaponType);
        }
    }
}
