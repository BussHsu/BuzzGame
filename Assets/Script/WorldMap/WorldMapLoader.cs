using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
public class WorldMapLoader : AssetLoader<WorldMapData>
{
	[SerializeField]
	GameObject cityPrefab;
	[SerializeField]
	GameObject pathPrefab;

//
	public GameObject worldMapObject;
	
	public List<City> citylist = new List<City>();
	public Dictionary<int,City> cityHash = new Dictionary<int, City>();
	public Dictionary<Point,PathData> pathTable= new Dictionary<Point, PathData>();
	
	#region implemented abstract members of AssetLoader
	
	protected override void BuildAfterLoad (WorldMapData data)
	{
		foreach(CityData cityData in data.Cities)
		{
			GameObject go = Instantiate(cityPrefab) as GameObject;
			City city = go.AddComponent<City>();
			
			city.transform.SetParent(worldMapObject.transform,false);
			city.Load(cityData);
			
			if(!AddCity(city,cityData.CityNum))
				continue;
			//region for more city setup
		}
		
		foreach(PathData pathData in data.Paths)
		{
			if(!UpdateCityNeighbors(pathData.connection))
				continue;
			
			pathTable.Add(pathData.connection,pathData);
		}
		
	}
	protected override string FolderName {
		get {
			return "WorldMap";
		}
	}
	
	#endregion
	bool AddCity(City city, int cityNum)
	{
		citylist.Add(city);
		if(cityHash.ContainsKey(cityNum))
		{
			Debug.LogError("Repeating city number.");
			return false;
		}
		cityHash.Add(cityNum,city);
		return true;
	}
	
	bool UpdateCityNeighbors(Vector2 con)
	{
		if(!CheckConnectionValid(con))
			return false;
		
		//build connections in city
		City dest1 = cityHash[(int)con.x];
		Debug.Log(dest1);
		City dest2 = cityHash[(int)con.y];
		Debug.Log(dest2);
		dest1.Neighbors.Add(dest2);
		dest2.Neighbors.Add(dest1);
		return true;
	}
	
	
	bool CheckConnectionValid(Vector2 vec)
	{
		return (vec.x!=vec.y) && !(Mathf.Min(vec.x,vec.y)<0) && !(Mathf.Max (vec.x,vec.y)>citylist.Count-1);
	}

}