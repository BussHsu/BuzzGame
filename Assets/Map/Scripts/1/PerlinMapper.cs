using UnityEngine;
using System.Collections;

public class PerlinMapper : Mapper 
{
	public Vector2 offset = Vector2.zero;
	public Vector2 scale = new Vector2(0.1f,0.1f);

	public override float Map (int x, int y)
	{
		return Mathf.PerlinNoise((offset.x+x)*scale.x,(offset.y+y)*scale.y);
	}	 
}
