using UnityEngine;
using System.Collections;

public class TestStatus : MonoBehaviour 
{
	Unit cursedUnit;
	Equippable cursedItem;
	int step;

	void OnEnable()
	{
		this.AddObserver(OnTurnCheck,TurnOrderController.TurnCheckNotification);
	}

	void OnDisable()
	{
		this.RemoveObserver(OnTurnCheck, TurnOrderController.TurnCheckNotification);
	}

	void OnTurnCheck(object sender, object args)
	{
		BaseException exc = args as BaseException;
		if(!exc.toggle)
			return;

		Unit target = sender as Unit;


		switch(step)
		{
		case 0:
			EquipCursedItem(target);
			break;
		case 1:
			AddTurnStatus<SlowStatusEffect>(target,3);
			break;
		case 2:
			AddTurnStatus<HasteStatusEffect>(target,3);
			break;
		case 3:
			AddTurnStatus<StopStatusEffect>(target,3);
			break;
		default:
			UnEquipCursedItem(target);
			break;
		}

		step++;
	}

	void AddTurnStatus<T>(Unit target, int turnNum) where T:StatusEffect
	{
		Status s = target.GetComponent<Status>();
		TurnStatusCondition cond = s.Add<T,TurnStatusCondition>();
		cond.durationTurns = turnNum;
	}

	void AddRoundStatus<T>(Unit target, int num) where T:StatusEffect
	{
		Status s = target.GetComponent<Status>();
		DurationStatusCondition d= s.Add<T,DurationStatusCondition>();
		d.duration = num;
	}

	void EquipCursedItem(Unit target)
	{
		cursedUnit = target;

		GameObject obj = new GameObject("Cursed Sword");
		obj.AddComponent<AddPoisonStatusFeature>();
		cursedItem = obj.AddComponent<Equippable>();
		cursedItem.defaultSlots = EquipSlots.Primary;

		Equipment equipment = target.GetComponent<Equipment>();
		equipment.Equip(cursedItem,EquipSlots.Primary);
	}

	void UnEquipCursedItem(Unit target)
	{

		if (target != cursedUnit || step < 10)
			return;
			
		Equipment equipment = target.GetComponent<Equipment>();
		equipment.UnEquip(cursedItem);
		Destroy(cursedItem.gameObject);
			
		Destroy(this);
		
	}
}
