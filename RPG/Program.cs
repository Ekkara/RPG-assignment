using RPG;
using RPG.Equipment_Components;
using RPG.Hero_Components;

Mage m = new("Babben");
Console.WriteLine(m.DisplayState());
m.LevelUp();
Console.WriteLine(m.DisplayState());
m.Equip(new Weapon("stick", damage: 10, requiredLevel: 1, WeaponType.Staffs));
Console.WriteLine(m.DisplayState());
m.Equip(new Weapon("bent stick with a string", damage: 10, requiredLevel: 1, WeaponType.Bows));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor("bucket", deffenseModifier: 10, ArmorType.Plate, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), EquipmentSlot.Head));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor("robe", deffenseModifier: 10, ArmorType.Cloth, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), EquipmentSlot.Body));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor("powerful robe", deffenseModifier: 9001, ArmorType.Cloth, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), EquipmentSlot.Body));
Console.WriteLine(m.DisplayState());
