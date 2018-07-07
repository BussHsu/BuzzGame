using UnityEngine;
using System.Collections;

public class ATypeHitRate : HitRate
{
	public override int Calculate (Tile target)
	{
		if(AutomaticHitCheck(attacker,target.unit))
			return Final(0);

		if(AutomaticMissCheck(attacker, target.unit))
			return Final(100);

		int evade = GetStat(target.unit,StatTypes.eq_AC);
		int aim = GetStat(attacker,StatTypes.eq_Atk);
		Debug.Log(aim+"/"+evade);
		evade -= aim;
		evade = AdjustForRelativeFacing(attacker, target.unit, evade);
		evade = AdjustForStatusCheck(attacker,target.unit,evade);
		evade = Mathf.Clamp(evade,1,19);
		return Final (evade*5);
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

	int GetStat(Unit target,StatTypes type)
	{
		return Mathf.Clamp(target.stat[type],0,100);
	}
}
