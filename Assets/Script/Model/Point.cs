using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public struct Point : IEquatable<Point>
{
	#region IEquatable implementation
	public bool Equals (Point p)
	{

		return x == p.x && y == p.y;
	}
	#endregion

	public int x;
	public int y;
	
	public Point (int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public Point(Vector2 v)
	{
		this.x = Mathf.RoundToInt(v.x);
		this.y = Mathf.RoundToInt(v.y);
	}

	public override string ToString ()
	{
		return string.Format ("({0},{1})", x, y);
	}
	
	public static bool operator == (Point a, Point b)
	{
		return a.x == b.x && a.y == b.y; 
	}
	
	public static bool operator != (Point a, Point b)
	{
		return !(a == b);
	}
	
	public static Point operator + (Point a, Point b)
	{
		return new Point (a.x + b.x, a.y + b.y);
	}
	
	public static Point operator - (Point a, Point b)
	{
		return new Point (a.x - b.x, a.y - b.y);
	}

	public static Point operator * (int a, Point b)
	{
		return new Point (a*b.x, a*b.y);
	}

	public static Point operator / (Point a, int b)
	{
		return new Point ((int)(a.x/b),(int) (a.y/b));
	}

	public override bool Equals(object obj)
	{
		if (obj is Point) 
		{
			Point p = (Point)obj;
			return x == p.x && y == p.y;
		}
		return false;
	}
	
	public override int GetHashCode()
	{
		return x ^ y;
	}

	public static implicit operator Vector2(Point p)
	{
		return new Vector2(p.x,p.y);
	}
}