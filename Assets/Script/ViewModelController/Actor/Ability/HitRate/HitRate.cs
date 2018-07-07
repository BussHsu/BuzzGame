using UnityEngine;
using System.Collections;

public abstract class HitRate : MonoBehaviour 
{
	protected Unit attacker {get{return GetComponentInParent<Unit>();}}

	public const string AutomaticHitCheckNotification = "HitRate.AutomaticHitCheckNotification";
	public const string AutomaticMissCheckNotification = "HitRate.AutomaticMissCheckNotification";
	public const string StatusCheckNotification = "HitRate.StatusCheckNotification";

	public abstract int Calculate(Tile target);

	protected bool AutomaticHitCheck(Unit attacker, Unit target)
	{
		MatchTypeException exc = new MatchTypeException(attacker,target);
		this.PostNotification(AutomaticHitCheckNotification,exc);
		return exc.toggle;
	}

	protected bool AutomaticMissCheck(Unit attacker, Unit target)
	{
		MatchTypeException exc = new MatchTypeException(attacker,target);
		this.PostNotification(AutomaticMissCheckNotification,exc);
		return exc.toggle;
	}

	protected int AdjustForStatusCheck(Unit attacker, Unit target,int rate)
	{
		Info<Unit,Unit,int> info = new Info<Unit,Unit,int> (attacker,target,rate);
		this.PostNotification(AutomaticHitCheckNotification,info);
		return info.arg2;
	}

	protected int Final(int rate)
	{
		return 100 - rate;
	}
}
