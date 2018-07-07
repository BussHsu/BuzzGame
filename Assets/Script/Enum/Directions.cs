using UnityEngine;
using System.Collections;

public enum Directions //方向類型
{
	//以左上為起點，分別為a, b, c, d, e, f

	a_UpLeft = 0,//左上,北
	b_UpRight = 1,//右上.東北
	c_Right = 2,//右.東南
	d_DownRight = 3,//右下.南
	e_DownLeft = 4,//左下.西南
	f_Left = 5,//左.西北
	None =6
}


public enum Facings
{
	Front,
	Side_F,
	Side_B,
	Back
}