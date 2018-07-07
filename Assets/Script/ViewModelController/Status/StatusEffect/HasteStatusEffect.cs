using UnityEngine;
using System.Collections;

public class HasteStatusEffect : StatusEffect 
{
	Stats myStats;

	void OnEnable()
	{
		myStats = GetComponentInParent<Stats>();
		if(myStats)
		{
			this.AddObserver(OnCTRWillChange,Stats.WillChangeNotification(StatTypes.CTR),myStats);
		}
	}

	void Disable()
	{
		this.RemoveObserver( OnCTRWillChange, Stats.WillChangeNotification(StatTypes.CTR), myStats);
	}

	void OnCTRWillChange(object sender, object args)
	{
		ValueChangeException exc = args as ValueChangeException;
		MultDeltaModifier mod = new MultDeltaModifier(0,2);
		exc.AddModifier(mod);
	}
}
