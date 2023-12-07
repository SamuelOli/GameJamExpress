using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public int SellPrice;
    public int BuyPrice;
    public ItemType itemType;
    public Rarity itemRarity;

    [Header("If Misc/Consumable")]
    [TextArea(3, 10)]
    public string Description;
    public bool IsStackable;

	[Header("If Gear")]

    public int LevelReq;
    public GearMainType gearMainType;

    public List<BasicAttribute> Attributes;

	[Header("System")]
	public int slot = 0;
}

public enum Rarity
{
    Normal = 0,
	Rare = 1,
	Epic = 2,
	Mythic=3,
	Legendary = 4
}

public enum GearMainType
{
	Amulet = 0,
	Helmet = 1,
	Spirit = 2,
    Weapon = 3,
	Armor = 4,
	SecondaryWeapon = 5,
	Ring = 6,
	Pants = 7,
	Arrow = 8,
	SecondRing = 9
}

public enum ItemType
{
    Gear = 0,
	Misc = 1, 
	Consumable=2
}

public enum MainStat
{
    Attack = 0, 
	Magic = 1, 
	Defence = 2
}
