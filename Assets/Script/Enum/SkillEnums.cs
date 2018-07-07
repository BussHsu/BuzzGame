using UnityEngine;
using System.Collections;

public enum RangeType//射程範圍  first one需要選擇(進入選擇階段), second one & third one直接進入確認階段
{
	Select ,//Normal+射程=0 表示自己為中心,選擇方向(如龍息, 橫劈)
	AutoSelf,//只有自己，如戰吼，以自己為中心的範圍技
	AutoAll,//全地圖
}



//public enum SkillMoveType //??? 技能移動方式，用於高低差判斷
//{
//	Normal,//一般的由使用者為中心向外移動一般類型
//	Move,//如衝鋒，這種讓使用者移動的
//	ALL,//無視高低差
//}

[System.Flags]
public enum AreaType //在射程範圍內的有效範圍
{
	None = 0,
	Single = 1<<0,//單體
	Radial = 1<<1,//放射狀
	Cone = 1<<2,//錐形，如龍息
	Line = 1<<3,//一條直線，如機戰的月光加農砲
	All = 1<<4,//圓形
	Custom,//自己設定，如3*1(x*y)
}


[System.Flags]
public enum TargetFilter//針對對像
{
	None =0,
	Enemy = 1<<0,//對敵方
	Player = 1<<1,//對友方
	Self = 1<<2,//對自己
	Land = 1<<3	// land
}

[System.Flags]
public enum EffectiveTarget//TODO 有效對象: 寫在effect中比較好
{
	None =0,
	Normal = 1<<0, 
	DownUnit = 1<<1,//對倒地單位有效
	Corpse = 1<<2,//對屍體有效，造屍術之類的
	Poisoned = 1<<3
}

[System.Flags]
public enum AbnormalStatus
{
	None =0,
	MagicConstrain =1<<0,	//封魔
	SkillConstrain =1<<1,	//封技
	Silenced = 1<<2			//全封
}


//障礙物影響
public enum BlockEffect 
{
	FirstSingle,//被阻擋，只能選擇射程內第一個目標
	SelectSingle,//掠過，可指定射程內任一目標
	PierceAll,//貫穿，射程內都受到傷害，傷害遞減
	ShareAll,//射程內都受到傷害，傷害由目標平均分攤
	All,//射程內都受到傷害，傷害不遞減
}