using UnityEngine;
using System.Collections;

public class PoisonStatusEffect : StatusEffect
{
	Unit owner;
	
	void OnEnable ()
	{
		owner = GetComponentInParent<Unit>();
		if (owner)
			this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
	}
	
	void OnDisable ()
	{
		this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
	}
	
	void OnNewTurn (object sender, object args)
	{
		Stats s = GetComponentInParent<Stats>();
		int currentHP = s[StatTypes.b_HP];
		int maxHP = s[StatTypes.b_MHP];
		int reduce = Mathf.Min(currentHP-1, Mathf.FloorToInt(maxHP * 0.1f));
		s.SetValue(StatTypes.b_HP, (currentHP - reduce), false);
	}
}
