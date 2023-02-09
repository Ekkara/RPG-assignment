using RPG;
using RPG.Equipment_Components;
using RPG.Hero_Components;

Ranger m = new("Babben");
Console.WriteLine(m.DisplayState());
m.LevelUp();
Console.WriteLine(m.DisplayState());
m.Equip(new Weapon(10, WeaponType.Staffs));
Console.WriteLine(m.DisplayState());
m.Equip(new Weapon(10, WeaponType.Bows));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor(10, ArmorType.Plate, EquipmentSlot.Head));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor(10, ArmorType.Cloth, EquipmentSlot.Body));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor(9001, ArmorType.Cloth, EquipmentSlot.Body));
m.Equip(new Weapon(9001, WeaponType.Staffs));
Console.WriteLine(m.DisplayState());