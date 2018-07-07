using UnityEngine;
using System.Collections;

[System.Flags]
public enum EquipSlots
{
	None = 0,			 //
	Primary = 1 << 0,	 //
	Secondary = 1 << 1,	 //
	Head = 1 << 2,		 //
	Body = 1 << 3,		 //
	Glove = 1 << 4,		 //
	Boot = 1 << 5,		 //
	Blet = 1 << 6,		 //
	Cloak = 1 << 7,		 //
	Accessory = 1 << 8,	 //
	Ring = 1 << 9,		 //
	Ring2 = 1 << 10		 
}