using UnityEngine;
using System.Collections;

public static class FacingExtensions 
{
	public static Facings GetFacings(this Unit attacker,Unit target)
	{
		Vector2 targetNormal = target.dir.GetNormal();
		Vector3 temp = target.tile.GridTileCenter - attacker.tile.GridTileCenter;
		Vector2 attackerNormal = new Vector2(temp.x,temp.z).normalized;
		float dot = Vector2.Dot(targetNormal,attackerNormal);

		if(dot>Mathf.Acos(30))
			return Facings.Back;
		if(dot>0)
			return Facings.Side_B;
		if(dot>-Mathf.Acos(30))
			return Facings.Side_F;
		else
			return Facings.Front;
	}
}
