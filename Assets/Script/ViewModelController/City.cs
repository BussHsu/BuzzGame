using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class City : MonoBehaviour
{
	List<City> _neighbors;
	public CityData data;
			
	public City prev;//尋路，前一個Tile
	public int distance;//距離，也就是移動過程中走過多少個Tile
	public List<City> Neighbors {get{if(_neighbors==null) _neighbors= new List<City>(); return _neighbors;}}
	RectTransform rt{get{return transform as RectTransform;}}

	public void Load (CityData data)
	{
		this.data = data;
		Match ();
	}

	void Match ()
	{
		GetComponentInChildren<Text>().text = gameObject.name = data.Name;
		rt.anchoredPosition = data.Pos;

//		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("WorldMap/City/"+data.CityNum+".png");

	}

	public void UpdatePos()
	{
		data.Pos = rt.anchoredPosition.ToPoint();
	}
}

[System.Serializable]
public class CityData
{
	public string Name;
	public int CityNum;
	public Point Pos = new Point(0,0);

	public CityData(string name, int cityNum):this(name,cityNum,new Point(0,0)) { }

	public CityData(string name, int cityNum, Point p)
	{
		this.Name = name;
		this.CityNum = cityNum;
		this.Pos =p;
	}
}
	