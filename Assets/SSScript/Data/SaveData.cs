using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class SaveData 
{
	public int dataNum;
	public string stageInfo;
//	public Sprite sprite;
	public DateTime time;

	public SaveData(int num)
	{
		this.dataNum = num;
		this.stageInfo ="N/A";
		this.time = DateTime.MinValue; //If empty slot => time = minVal
	}
}
