using UnityEngine;
using System.Collections;

public class DurationStatusCondition : StatusCondition 
{
	public int duration;

	void OnEnable()
	{
		this.AddObserver(OnTurnBegan,TurnOrderController.RoundBeganNotification);
	}

	void OnDisable()
	{
		this.RemoveObserver(OnTurnBegan,TurnOrderController.RoundBeganNotification);
	}

	void OnTurnBegan(object sender, object args)
	{
		duration --;
		if(duration<=0)
			Remove();
	}
}
