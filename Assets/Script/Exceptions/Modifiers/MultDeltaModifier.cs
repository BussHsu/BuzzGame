using UnityEngine;
using System.Collections;

public class MultDeltaModifier : ValueModifier
{
	public readonly float toMult;

	public MultDeltaModifier(int sortOrder, float toMultiply):base(sortOrder)
	{
		toMult = toMultiply;
	}

	#region implemented abstract members of ValueModifier

	public override float Modify (float fromValue, float toValue)
	{
		float delta = toValue - fromValue;

		return fromValue+ delta*toMult;
	}

	#endregion


}
