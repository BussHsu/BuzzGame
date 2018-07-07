using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldMapData : ScriptableObject
{
	public List<CityData> Cities;
	public List<PathData> Paths;

//	public void AddCities(CityData city)						//make sure city number don't repeat
//	{
//		if(Cities.Select(x=>x.CityNum).Contains(city.CityNum))
//		{
//			Debug.Log("a city data is rewritten");
//			Cities.Remove(Cities.Where(x=>x.CityNum == city.CityNum).First());
//
//		}
//		Cities.Add(city);
//	}
//
//	public void AddPath(Point connection, PathData p)
//	{
//		if(Paths.Keys.Contains(connection))
//		{
//			Debug.Log("a path data is rewritten");
//			Paths.Remove(connection);
//		}
//		Paths.Add(connection,p);
//	}
}
