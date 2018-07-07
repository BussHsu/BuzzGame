using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PathData 
{
	public Point connection;
	public Point[] points;
	public Color c;

	public PathData(Point p)
	{
		if(p.x==p.y)
		{
			Debug.Log("Error: self connection");
			return;
		}

		if(p.x<0||p.y<0)
		{
			Debug.Log("Error: city number < 0");
			return;
		}
		connection = p;

	}


}
