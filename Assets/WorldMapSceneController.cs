//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class WorldMapSceneController : MonoBehaviour 
//{
//	Hero hero;
//	WorldMap map;
//	PathCtrl pathCtrl;
//
//	void OnEnable()
//	{
//		map = GetComponent<WorldMap>();
//		pathCtrl = GetComponent<WorldMap>();
//	}
//
//	public void HeroTravel(int destCityNum)
//	{
//		List<City> stops = map.FindPath(hero.location,map.citylist[destCityNum]);
//		List<PathData> paths = GetPathFromStops(stops);
//	}
//
////	List<PathData> GetPathFromStops(List<City> stops)
////	{
////		List<PathData> retValue = new List<PathData>();
////		PathData temp;
////		for(int i=0;i<stops.Count-2;i++)
////		{
////			temp = map.pathTable
////		}
////	}
//}
