using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
	public string unitName;
	public Alliances unitAlly;
	public Point pos{get{return tile.pos;}}
	public Tile tile { get; protected set;}//Unit所在的Tile
	public Stats stat{get{return GetComponentInChildren<Stats>();}}
	public Directions dir;//Unit正面所面對的方向

	public void Place (Tile target)
	{
		//在移動位置前先清空現在位置的MapObject
		if (tile != null && tile.content == gameObject)  
			tile.content = null;

		tile = target;


		if (target != null)
			target.content = gameObject;
	}

	public void Match()
	{
		transform.localPosition = tile.GridTileCenter;
		transform.localEulerAngles = dir.ToEuler();
	}
}