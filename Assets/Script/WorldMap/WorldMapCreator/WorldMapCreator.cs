using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMapCreator : AssetCreator<WorldMapData>
{
	[SerializeField]
	GameObject cityPrefab;
	[SerializeField]
	GameObject pathPrefab;
	[SerializeField]
	GameObject nodePrefab;

	public GameObject worldMapObject;

	protected override string FolderName {
		get {
			return "WorldMap";
		}
	}
//	public Dictionary<Point,PathData> Paths = new Dictionary<Point, PathData>();

	public List<City> citylist = new List<City>();
	public Dictionary<int,City> cityHash = new Dictionary<int, City>();
	public Dictionary<Point,WayPath> pathTable= new Dictionary<Point, WayPath>();

	protected override void EditAssetBeforeSave (WorldMapData data)
	{
		data.Cities = new List<CityData>();
		foreach(City c in citylist)
		{
			c.UpdatePos();
			data.Cities.Add(c.data);
		}

		data.Paths = new List<PathData>();
		foreach(WayPath p in pathTable.Values)
		{
			data.Paths.Add(p.AsPathData());
		}
	}

	 

//	public void Load(string fileName)
//	{
//		if (
//	}

	public void ClearMap()			//TODO   destroy
	{
		foreach(City c in citylist)
		{
			DestroyImmediate(c.gameObject);
		}
		citylist.Clear();
		cityHash.Clear();
		
		foreach(WayPath p in pathTable.Values)
		{
			foreach(WayNode n in p.nodes)
			{
				DestroyImmediate(n.gameObject);
			}
			DestroyImmediate(p.gameObject);
		}
		pathTable.Clear();

	}

	#region implemented abstract members of AssetCreator

	protected override void PreLoadCleanUp ()
	{
		ClearMap();
	}

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
		}

		foreach(PathData pathData in data.Paths)
		{
			if(!UpdateCityNeighbors(pathData.connection))
				continue;

			GameObject go = Instantiate(pathPrefab) as GameObject;
			go.transform.SetParent(worldMapObject.transform);
			WayPath path = go.AddComponent<WayPath>();
			path.prefab = pathPrefab;
			path.LoadPathInEditor(pathData);
			pathTable.Add(pathData.connection, path);
		}

	}
	#endregion

	public void SaveMap(string name){SaveAsset(name);}
	public void LoadMap(string name){LoadAsset(name);}

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



	#region Inspector methods
	public void CreateNewCity(string name)
	{
		if(string.IsNullOrEmpty(name))
		{
			Debug.LogError("Please enter name");
			return;
		}

		int cityNum = citylist.Count;
		CityData cityData = new CityData(name,cityNum);

		GameObject go = Instantiate(cityPrefab) as GameObject;
		City city = go.AddComponent<City>();
		
		city.transform.SetParent(worldMapObject.transform,false);
		city.Load(cityData);

		AddCity(city,cityNum);
	}

	public void CreateNewPath(int dest1, int dest2,int numOfNodesBtn,Color c)
	{
		if(!CheckConnectionValid(new Vector2(dest1,dest2)))
		{
			Debug.LogError("Invalid path connection");
			return;
		}

		int smallerNum = dest1<dest2? dest1:dest2;
		int biggerNum =  dest1>dest2? dest1: dest2;

		Point connection = new Point(smallerNum,biggerNum);

		if(pathTable.ContainsKey(connection))
		{
			Debug.Log("Path already exists.");
			return;
		}

		PathData pData = new PathData(connection);
		pData.points = CreateNodesInPath(smallerNum,biggerNum,numOfNodesBtn);
		pData.c = c;

		GameObject go = Instantiate(pathPrefab) as GameObject;
		go.transform.SetParent(worldMapObject.transform,false);

		WayPath path = go.AddComponent<WayPath>();
		path.prefab = nodePrefab;
		path.LoadPathInEditor(pData);
		pathTable.Add(pData.connection,path);

	}
	#endregion

	bool CheckConnectionValid(Vector2 vec)
	{
		return (vec.x!=vec.y) && !(Mathf.Min(vec.x,vec.y)<0) && !(Mathf.Max (vec.x,vec.y)>citylist.Count-1);
	}

	Point[] CreateNodesInPath(int smallerCity, int biggerCity, int numOfNodesBtn)
	{
		Point[] retVal = new Point[numOfNodesBtn+2];
		retVal[0] = cityHash[smallerCity].data.Pos;
//		Debug.Log(retVal[0]);
		retVal[numOfNodesBtn+1] = cityHash[biggerCity].data.Pos;
		for(int i=1;i<numOfNodesBtn+1;i++)
		{
			retVal[i] = Vector2.Lerp(retVal[0],retVal[numOfNodesBtn+1],(float)i/(numOfNodesBtn+1)).ToPoint();
			Debug.Log(retVal[i]);
		}
		Debug.Log(retVal[numOfNodesBtn+1]);
		return retVal;
	}
}
