﻿using RPG;
using RPG.Equipment_Components;
using RPG.Hero_Components;

Mage m = new("Babben");
Console.WriteLine(m.DisplayState());
m.LevelUp();
Console.WriteLine(m.DisplayState());
m.Equip(new Weapon(damage: 10, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence:1), WeaponType.Staffs));
Console.WriteLine(m.DisplayState());
m.Equip(new Weapon(damage: 10, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), WeaponType.Bows));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor(deffenseModifier: 10, ArmorType.Plate, requiredLevel:  1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), EquipmentSlot.Head));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor(deffenseModifier:  10, ArmorType.Cloth, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), EquipmentSlot.Body));
Console.WriteLine(m.DisplayState());
m.Equip(new Armor(deffenseModifier: 9001, ArmorType.Cloth, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), EquipmentSlot.Body));
m.Equip(new Weapon(damage: 9001, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), WeaponType.Staffs));
Console.WriteLine(m.DisplayState());

m.Equip(new Weapon(damage: 9001, requiredLevel: 10, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 1), WeaponType.Staffs));

m.Equip(new Weapon(damage: 9001, requiredLevel: 1, new HeroAttribute(strength: 1, dexterity: 1, intelligence: 100), WeaponType.Staffs));