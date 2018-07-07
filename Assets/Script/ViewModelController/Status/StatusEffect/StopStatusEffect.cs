using UnityEngine;
using System.Collections;

public class StopStatusEffect : StatusEffect
{
	Stats myStats;
	
	void OnEnable()
	{
		myStats = GetComponentInParent<Stats>();
		if(myStats)
		{
			this.AddObserver(OnCTRWillChange,Stats.WillChangeNotification(StatTypes.CTR),myStats);
		}

		this.AddObserver(OnAutoHitCheck, HitRate.AutomaticHitCheckNotification);
	}

	void OnAutoHitCheck(object sender, object args)
	{
		Unit owner = GetComponentInParent<Unit>();
		MatchTypeException exc = args as MatchTypeException;
		if(exc.target == owner)
			exc.FlipToggle();
	}

	void Disable()
	{
		this.RemoveObserver( OnCTRWillChange, Stats.WillChangeNotification(StatTypes.CTR), myStats);
		this.RemoveObserver(OnAutoHitCheck,HitRate.AutomaticHitCheckNotification);
	}
	
	void OnCTRWillChange(object sender, object args)
	{
		ValueChangeException exc = args as ValueChangeException;
		exc.FlipToggle();
	}
}