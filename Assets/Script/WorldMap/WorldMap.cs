using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
public class WorldMap : WorldMapLoader
{
	public string mapName;



	void Awake()
	{
		if(!string.IsNullOrEmpty(mapName))
			LoadAsset(mapName);
	}

	public List<City> FindPath(City origin, City end)
	{
		foreach(City c in citylist)
		{
			c.distance = int.MaxValue;
			c.prev = null;
		}
		origin.distance =0;

		bool reachFlag = false;
		Queue<City> current= new Queue<City>();
		Queue<City> next = new Queue<City>();

		current.Enqueue(origin);

		while (current.Count>0)
		{
			City currentCity = current.Dequeue();
			if(currentCity == end)
			{
				reachFlag = true;
				break;
			}

			foreach(City c in currentCity.Neighbors)
			{
				if(currentCity.distance+1< c.distance)
				{
					c.prev = currentCity;
					c.distance = currentCity.distance+1;
					next.Enqueue(c);
				}
			}

			if(current.Count==0)
				Swap(ref current,ref next);
		}

		if(!reachFlag)			// if destination cannot be reached => return null;
		{
			return null;
		}

		List<City> retValue = new List<City>();
		while(end.prev!=origin)
		{
			retValue.Add(end);
			end = end.prev;
		}
		retValue.Add(origin);
		retValue.Reverse();

		return retValue;

	}

	public List<PathData> GetPathFromStops(List<City> stops)
	{
		List<PathData> retValue = new List<PathData>();
		PathData temp;
		for(int i=0;i<stops.Count-2;i++)
		{
			temp = pathTable[new Point(stops[i].data.CityNum,stops[i+1].data.CityNum)];
			if(temp == null)
			{
				Debug.LogError("Cannot find path from pathTable");
				return null;
			}

			retValue.Add(temp);				
		}
		return retValue;
	}


	void Swap(ref Queue<City> q1, ref Queue<City> q2)
	{
		Queue<City> temp = q1;
		q1 = q2;
		q2 = temp;
	}

	void PreSearchSetup()
	{
		//foreach city: distance = maxint, prev = null
		foreach(City c in citylist)
		{
			c.distance = int.MaxValue;
		}
	}
}
