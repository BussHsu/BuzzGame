using UnityEngine;
using System.Collections;

public abstract class AddStatusFeature<T> : Feature where T:StatusEffect
{
	StatusCondition condition;

	protected override void OnApply ()
	{
		Status s = GetComponentInParent<Status>();
		condition = s.Add<T,StatusCondition>();
	}

	protected override void OnRemove ()
	{
		if(condition!=null)
			condition.Remove();
	}
}
