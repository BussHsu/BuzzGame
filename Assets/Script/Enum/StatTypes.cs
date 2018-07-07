using UnityEngine;
using System.Collections;

public enum StatTypes
{
	Lvl =0,	// Level
	exp,	// 經驗值
	b_HP,	// 生命
	b_MHP,	// 最大生命
	b_SP,	// 法力
	b_MSP,	// 最大法力
	b_STR,	// 力量
	b_CON,	// 體質
	b_AGI,	// 敏捷
	b_INT,	// 智力
	b_WIS= 10,	// 感知
	b_LUK,	// 運氣
	eq_Atk,	// 物理攻擊
	eq_AC,	// 物理evade
	eq_Mat,	// 魔法攻擊
	eq_Mdf,	// 魔法防禦
	Phy_Red,// physical dmg reduction 
	Mag_Red,// magical dmg reduction
	SPD,	// 速度，影響行動順序
	WT,		// 行動順序
	Mov = 20,	// 移動力
	JtU,	// Jump To Up
	JtD,	// Jump To Down
	CTR,
	Count
}