using UnityEngine;
using System.Collections;

public class MultiplyNode : Node
{
	public float value=0.5f;
	
	public override float Calculate (float input)
	{
		return input * value;
	}
}