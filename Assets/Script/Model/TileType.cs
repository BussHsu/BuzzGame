using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType
{
	public string Name;
	public int Cost;//移動消耗，先加未來加在角色職業身上
	public Material[] TileMaterial = new Material[2];//第一個元素是側面，第二個是頂面
	public float[] Bonus = new float[4];//防禦、迴避、地形傷害/恢復、影蔽(最低0：用途為減少AI判定範圍)DnD

}
