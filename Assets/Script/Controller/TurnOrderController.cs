using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TurnOrderController : MonoBehaviour 
{
	#region Constants
	const int turnActivationThreshold = 100;
	const int turnCost = 50;
	const int moveCost = 30;
	const int actionCost = 20;
	#endregion

	#region Notifications
	public const string RoundBeganNotification = "TurnOrderController.roundBegan";
	public const string TurnCheckNotification = "TurnOrderController.turnCheck";
	public const string TurnBeganNotification = "TurnOrderController.turnBegan";
	public const string TurnCompletedNotification = "TurnOrderController.turnCompleted";
	public const string RoundEndedNotification = "TurnOrderController.roundEnded";
	#endregion

	#region Public
	public IEnumerator Round()
	{
		BattleController bc = GetComponent<BattleController>();

		while(true)
		{
			this.PostNotification(RoundBeganNotification);
			List<Unit> units = new List<Unit>(bc.UnitList);

			for (int i=0;i<units.Count;i++)
			{
				Stats s = units[i].GetComponent<Stats>();
				s[StatTypes.CTR] += s[StatTypes.b_AGI];
			}

			units.Sort((a,b)=>GetCounter(a).CompareTo(GetCounter(b)));	// intA.ComapareTo(intB) =>return + if intA>intB, sort(+)=>ascending

			for(int i = units.Count-1; i>=0;i--)
			{
				if(CanTakeTurn(units[i]))
				{
					bc.turn.Change(units[i]);
					units[i].PostNotification(TurnBeganNotification);
					yield return units[i];				//?

					int cost = turnCost;
					if (bc.turn.hasUnitActed)
						cost += actionCost;
					if(bc.turn.hasUnitMoved)
						cost += moveCost;
					Stats s =units[i].GetComponent<Stats>();
					s.SetValue(StatTypes.CTR,s[StatTypes.CTR]- cost,false);

					units[i].PostNotification(TurnCompletedNotification);
				}
			}
			this.PostNotification(RoundEndedNotification);
		}
	}
	#endregion

	#region Private
	bool CanTakeTurn(Unit target)
	{
		BaseException exception = new BaseException(GetCounter(target) >= turnActivationThreshold);
		target.PostNotification(TurnCheckNotification,exception);										// need to check exceptions like dead unit
		return exception.toggle;
	}

	int GetCounter(Unit target)
	{
		return target.GetComponent<Stats>()[StatTypes.CTR];
	}
	#endregion
}
