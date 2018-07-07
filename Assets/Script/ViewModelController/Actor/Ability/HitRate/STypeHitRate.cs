using UnityEngine;
using System.Collections;

public class STypeHitRate : HitRate
{
	public override int Calculate (Tile target)
	{
		if(AutomaticHitCheck(attacker,target.unit))
			return Final(0);
		
		if(AutomaticMissCheck(attacker, target.unit))
			return Final(100);
		
		int MDF = GetMDF(target.unit);
		MDF = AdjustForRelativeFacing(attacker, target.unit, MDF);
		MDF = AdjustForStatusCheck(attacker,target.unit,MDF);
		MDF = Mathf.Clamp(MDF,5,95);
		return Final (MDF);
	}
	
	int AdjustForRelativeFacing(Unit attacker, Unit target, int rate)
	{
		Facings facing = attacker.GetFacings(target);
		switch(facing)
		{
		case Facings.Back:
			return rate/4;
		case Facings.Side_B:
			return rate/3;
		case Facings.Side_F:
			return rate/2;
		default:
			return rate;
		}
	}
	
	int GetMDF(Unit target)
	{
		Stats s = target.GetComponentInParent<Stats>();
		return Mathf.Clamp(s[StatTypes.eq_Mdf],0,100);
	}

}
