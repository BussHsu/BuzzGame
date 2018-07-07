using UnityEngine;
using System.Collections;

public class TurnStatusCondition : StatusCondition
{
	public int durationTurns =3;
	Unit owner;

	void OnEnable()
	{
		owner = GetComponentInParent<Unit>();
		this.AddObserver(OnTurnBegan,TurnOrderController.TurnBeganNotification,owner);
	}

	void OnDisable()
	{
		this.RemoveObserver(OnTurnEnd,TurnOrderController.TurnCompletedNotification,owner);
		this.RemoveObserver(OnTurnBegan, TurnOrderController.TurnBeganNotification,owner);
	}

	void OnTurnBegan(object sender, object args)
	{
		durationTurns --;
		if(durationTurns<=0)
		{
			this.RemoveObserver(OnTurnBegan, TurnOrderController.TurnBeganNotification,owner);
			this.AddObserver(OnTurnEnd,TurnOrderController.TurnCompletedNotification,owner);

		}
	}

	void OnTurnEnd(object sender,object args)
	{
		Remove();
	}
}
