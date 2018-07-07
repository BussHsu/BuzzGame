using UnityEngine;
using System.Collections;

public class BandNode : Node
{
	public Vector2 range = new Vector2(0.4f,0.6f);

	#region implemented abstract members of Node

	public override float Calculate (float input)
	{
		return (input>range.x&& input<range.y)?1f:0f;
	}

	#endregion


	 
}
