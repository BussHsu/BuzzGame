using UnityEngine;
using System;

public class EnemyData : ScriptableObject
{
    public string moster_name; //名稱
    public GameObject prefabs;//模型
    public string AIType;//
	public int Lvl;     // Level
	//add code種族
	public int exp;		// 獲得經驗值
	public int gold;	// 掉落金錢
	public int b_MHP;	// 最大生命
	public int b_MMP;	// 最大法力
	public int b_STR;	// 力量
	public int b_CON;	// 體質
	public int b_AGI;	// 敏捷
	public int b_INT;	// 智力
	public int b_WIS;	// 感知
	public int b_LUK;	// 運氣
    public MovementTypes m_MoveType;//移動類型
	public int Mov;		// 移動力
	public int JtU;		// Jump To Up
	public int JtD;		// Jump To Down
	public int Count;

    //add code怪身上的裝備
    //add code專長
    //add code技能
    //add code天賦

	public void Load(string line)
	{
		string[] elements = line.Split(',');
        moster_name = elements[0];
        prefabs = Resources.Load<GameObject>("Prefab/Moster/" + elements[1]);
		//AIType = 
        Lvl = Convert.ToInt32(elements[3]);
        exp = Convert.ToInt32(elements[4]);
        gold = Convert.ToInt32(elements[5]);
        b_MHP = Convert.ToInt32(elements[6]);
        b_MMP = Convert.ToInt32(elements[7]);
        b_STR = Convert.ToInt32(elements[8]);
        b_CON = Convert.ToInt32(elements[9]);
        b_AGI = Convert.ToInt32(elements[10]);
        b_INT = Convert.ToInt32(elements[11]);
        b_WIS = Convert.ToInt32(elements[12]);
        b_LUK = Convert.ToInt32(elements[13]);
        m_MoveType = (MovementTypes)Convert.ToInt32(elements[14]);
        Mov = Convert.ToInt32(elements[15]);
        JtU = Convert.ToInt32(elements[16]);
        JtD = Convert.ToInt32(elements[17]);
    }
}