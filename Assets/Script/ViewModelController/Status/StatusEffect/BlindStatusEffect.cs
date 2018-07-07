using UnityEngine;
using System.Collections;

public class BlindStatusEffect : StatusEffect
{
	void OnEnable()
	{
		this.AddObserver(OnAdjustStatusCheck, HitRate.StatusCheckNotification);
	}
	
	void OnAdjustStatusCheck(object sender, object args)
	{
		Unit owner = GetComponentInParent<Unit>();
		Info<Unit,Unit,int> info = args as Info<Unit, Unit, int>;
		if(info.arg0 == owner)
			info.arg2 += 50;
		else if(info.arg1 == owner)
			info.arg2 -= 30;

	}
	
	void Disable()
	{
		this.RemoveObserver(OnAdjustStatusCheck,HitRate.StatusCheckNotification);
	}
}
