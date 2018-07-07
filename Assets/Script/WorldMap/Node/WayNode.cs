using UnityEngine;
using System.Collections;

public class WayNode : MapNode
{
	public Color c;
	Connection connect;
	public Point p;

	public WayNode(int city1, int city2)
	{
		connect = new Connection(city1,city2);
	}

	#region implemented abstract members of MapNode

	public override bool ConnectCity (int cityNum)
	{
		return connect.ConnectCity(cityNum);
	}

	#endregion

	public void Match()
	{
		RT.anchoredPosition = p;
		Debug.Log(p);
	}

	public void UpdatePos()
	{
		p = RT.anchoredPosition.ToPoint();
	}

	void OnDrawGizmos()
	{
		if(c!=null)
		Gizmos.color = c;
		Gizmos.DrawSphere (transform.position, 0.05f);
	}
}


public struct Connection : System.IEquatable<Connection>
{
	int dest1;
	int dest2;

	public int Dest1
	{
		get
		{
			if(dest1<0) 
				Debug.LogError("dest1 <0 not set"); 
			return dest1;
		}
		set
		{
			 dest1=value;
		}
	}
	public int Dest2{get{if(dest2<0) Debug.LogError("dest2 <0 not set"); return dest2;} set{ dest2=value;}}

	public Connection (int city1, int city2)
	{
		Dest1 = city1;
		Dest2 = city2;
	}

	public bool ConnectCity(int city)
	{
		if(dest1==city||dest2 == city)
			return true;
		return false;
	}

	public static implicit operator Point(Connection c)
	{
		return new Point(c.Dest1,c.Dest2);
	}

	#region IEquatable implementation

	public bool Equals (Connection other)
	{
		if(dest1==other.dest1 && dest2==other.dest2)
			return true;
		if(dest2==other.dest1 && dest1 == other.dest2)
			return true;
		return false;
	}

	#endregion
}