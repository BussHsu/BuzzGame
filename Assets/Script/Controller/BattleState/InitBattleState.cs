using UnityEngine;
using System.Collections;

public class InitBattleState : BattleState
{
	public override void Enter () 
	{
		base.Enter();
		StartCoroutine (Init ());
	}

	IEnumerator Init () 
	{
		owner.round = gameObject.AddComponent<TurnOrderController>().Round();
		board.LoadMap (level);
		Point p = new Point ((int)level.TilesInMap [0].x, (int)level.TilesInMap [0].z);
		pos = new Point(100,100);
		SelectTile (p);

		SpawnTestUnits ();

		yield return null;

		owner.ChangeState<CutSceneState> ();
	}

	void SpawnTestUnits()
	{
		System.Type[] movementTypes = new System.Type[]{typeof(WalkMovement), typeof(FlyMovement), typeof(TeleportMovement)};
		string[] jobs = new string[] {"Hero", "Heroine", "Bob", "Zoey"};

		for (int i = 0; i < jobs.Length; i++) 
		{
			GameObject instance = Instantiate(owner.heroPrefab) as GameObject;

			//setup stat
			Stats s = instance.AddComponent<Stats>();
			s[StatTypes.Lvl] = 1;

			//setup job
			GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
			GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
			jobInstance.transform.SetParent(instance.transform);

			Job job = jobInstance.GetComponent<Job>();
			job.Employ();
			job.LoadDefaultStats();

			Point p = new Point((int)level.TilesInMap[i].x, (int)level.TilesInMap[i].z);

			//setup unit
			Unit unit = instance.GetComponent<Unit>();
			unit.unitName = jobs[i];
			unit.gameObject.name = unit.unitName;
			unit.unitAlly = (Alliances)i;
			unit.Place(board.GetTile(p));
			unit.Match();

			//Equip something
			Equipment equipment = instance.GetComponent<Equipment>();
			Equippable item = RandomCreateEquipment();
			if(item!=null)
				equipment.Equip(item, item.defaultSlots);


			instance.AddComponent<WalkMovement>();

			UnitList.Add(unit);

			Rank rank = instance.AddComponent<Rank>();
			rank.Init(10);
		}
	}

	Equippable RandomCreateEquipment()
	{
		int rand = Random.Range(0,11);
		if(rand>5)
		{
			return CreateEquipment("Claymore",StatTypes.eq_Atk, 15, EquipSlots.Primary&EquipSlots.Secondary);
		}
		else if(rand<5&&rand>2)
		{
			return CreateEquipment("long sword",StatTypes.eq_Atk, 10, EquipSlots.Primary);
		}
		else 
			return null;
	}

	Equippable CreateEquipment(string name, StatTypes stat, int amount, EquipSlots slot)
	{
		GameObject instance = new GameObject(name);
		Equippable item = instance.AddComponent<Equippable>();
		item.defaultSlots = slot;
		StatModifierFeature feature =  instance.AddComponent<StatModifierFeature>();
		feature.amount = amount;
		feature.type = stat;
		return item;
	}
}