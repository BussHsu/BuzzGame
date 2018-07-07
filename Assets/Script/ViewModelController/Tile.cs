using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
	[SerializeField]
	public Point pos;//地磚的點Point
	public Vector3 GridTileCenter {get{return GetPosition(pos);}} 
	public int height, GridTileType;//地磚的座標高度、地磚的種類、地磚的地形

	public string TileTypeName;
	public int TileCost;//移動力消耗
	public float[] TileBonus;//地形效果
	public Renderer TileRenderer;

	[SerializeField]
	public float HexWidth, HexDepth, HexHeight;//宣告儲存地磚模型的寬長(深)高，跟Unity內的xyz定義不同，成長比例
		
	public const float stepHeight = 1f;

	public GameObject content;//地圖上的物件可破壞障礙物寶箱之類的
	public Unit unit{get{return (content==null)?null:content.GetComponentInChildren<Unit>();}}
//	public Unit unit;

	//尋路用
	public Tile prev;//尋路，前一個Tile
	public int distance;//距離，也就是移動過程中走過多少個Tile
    public int TotalCost;//移動消耗總和


	void Awake () 
	{
		GridTileType = 0;
		TileRenderer = GetComponent<Renderer> ();
		HexWidth = GetComponent<Renderer>().bounds.size.x;//取得地磚寬
		HexDepth = GetComponent<Renderer>().bounds.size.z;//取得地磚長(深)，以Unity的座標來看則為z
		HexHeight = GetComponent<Renderer>().bounds.size.y;//取得地磚高，以Unity的座標來看則為y
	}


	public void Load (Point p, int gh, int gtp)
	{
		pos = p;
		height = gh;
		GridTileType = gtp;
		Match (p);
	}

	public void Load (Vector4 v)
	{
		Load (new Point((int)v.x, (int)v.z), (int)v.y, (int)v.w);
	}

	public Vector3 GetPosition (Point p)
	{
		float TPx = p.x * HexWidth +  HexWidth * 0.5f * ( p.y % 2);//TP 為 TilePosition
		float TPy = p.y * HexDepth * 0.75f;
		return new Vector3(TPx, HexHeight * height, TPy);
	}

	void Match (Point p)
	{
		Vector3 v = GetPosition (p);
		transform.localPosition = new Vector3(v.x, 0, v.z);
		transform.localScale = new Vector3(1, height, 1);
		SetTileType (GridTileType);
	}

	//不太想加在這

	public void Grow (Point p)
	{
		height++;
		Match(p);
	}
	
	public void Shrink (Point p)
	{
		height--;
		Match (p);
	}

	public void SetTileType(int i)
	{
		TileTypeData data = Resources.Load<TileTypeData>("TileTypeData/TileTypeData");

		GridTileType = i;
		TileTypeName = data.TileTypeList [i].Name;
		TileCost = data.TileTypeList [i].Cost;
		TileRenderer.materials = data.TileTypeList [i].TileMaterial;
		TileBonus = data.TileTypeList [i].Bonus;
	}
}