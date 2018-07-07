using UnityEngine;
using System.Collections;

public static class PointExtensions
{
	public static Point ToPoint(this Vector2 vec)
	{
		return new Point(Mathf.RoundToInt(vec.x),Mathf.RoundToInt(vec.y));
	}
}
