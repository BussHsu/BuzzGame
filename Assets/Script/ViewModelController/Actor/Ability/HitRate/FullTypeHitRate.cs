using UnityEngine;
using System.Collections;

public class FullTypeHitRate : HitRate
{
	public override int Calculate (Tile target)
	{
		if(AutomaticMissCheck(attacker,target.unit))
		{
			return Final(100);
		}

		return Final (0);
	}

}
