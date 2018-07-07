using UnityEngine;
using System.Collections;

public class BarNode : Node
{
	public float value = 0.5f;
	public override float Calculate (float input)
	{
		return input> this.value?1.0f:0f;
	}
}
