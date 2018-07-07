using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WayPath : MonoBehaviour 
{
	public GameObject prefab;

	public Color color;
	public Connection connection;
	public List<WayNode> nodes = new List<WayNode>();

	
	RectTransform rt{get{return GetComponent<RectTransform>();}}

	/// <summary>
	/// Loads pathData & create path under editor mode.
	/// </summary>
	/// <param name="data">Data.</param>
	public void LoadPathInEditor(PathData data)
	{
		connection = new Connection(data.connection.x,data.connection.y);
		gameObject.name = string.Format("Path {0}",data.connection.ToString());

		rt.anchoredPosition = Average(data.points);
		color = data.c;

		for(int i=0;i<data.points.Length;i++)
		{
			GameObject go = Instantiate(prefab) as GameObject;
			go.name = gameObject.name+" Point."+i;
			go.transform.SetParent(transform.parent,false);

			WayNode node = go.AddComponent<WayNode>();
			node.c = (color != null)?color:Color.yellow;
			node.p = data.points[i];
			node.Match();
			nodes.Add(node);
		}
	}


	/// <summary>
	/// change to pathData type for saving
	/// </summary>
	/// <returns>The path data.</returns>
	public PathData AsPathData()
	{
		PathData data = new PathData(connection);
		for(int i=0;i<nodes.Count;i++)
		{
			nodes[i].UpdatePos();

		}

		data.points = nodes.Select(x=>x.p).ToArray();

		return data;
	}
	
	Point Average(Point[] points)
	{
		Point sum= new Point(0,0);
		for(int i=0;i<points.Length;i++)
		{
			sum+=points[i];
		}
		return sum/points.Length;
	}
}