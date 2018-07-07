using UnityEngine;
using System.Collections;

public class InvertNode : Node
{

	public override float Calculate (float input)
	{
		return 1f - input;
	}
}
