using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Turn
{
	public Unit actor;
	public bool hasUnitMoved;
	public bool hasUnitActed;
	public bool lockMove;
	public Tile startTile;
	public Directions startDir;
	public GameObject ability;
	public Skill skill{get{return ability!=null?ability.GetComponent<Skill>():null;}}	//editing

	public List<Tile> abilityRange;
	public List<Tile> targets;

	public bool toCatSelectState{get {return phaseIndex < 0;}}
	public bool hasNextPhase{get{return phaseIndex<(skill.phasers.Length-1);}}
	public GameObject phaser;
	int phaseIndex=-1;
	public List <SkillTargetRecord> records; //editing


	public void Change(Unit current)
	{
		//選擇Unit，並記錄選擇時的狀態
		actor = current;
		hasUnitMoved = false;
		hasUnitActed = false;
		lockMove = false;
		startTile = actor.tile;
		startDir = actor.dir;
		phaseIndex = -1;
		SetupSkillRecord();
	}

	public void UndoMove()
	{
		hasUnitMoved = false;
		actor.Place (startTile);
		actor.dir = startDir;
		actor.Match ();
	}
	

	public void SavePhaseRecord()
	{
		SkillTargetRecord newRecord = new SkillTargetRecord();
		newRecord.targets = targets.Select(x => x.content).ToArray();
		newRecord.phaseNum = phaseIndex;
		newRecord.effect = phaser.GetComponent<Effect>();

		records.Add(newRecord);
		Debug.Log("Records: " + records.Count);
	}

	public void NextPhase()
	{
		phaseIndex++;

		if(phaseIndex<skill.phaseNum)
		phaser = skill.phasers[phaseIndex];
		Debug.Log("+pahseIndex: "+phaseIndex);
	}

	/// <summary>
	/// phase2 --> phase1   || phase0(0)--> CategorySelectionState(-1)
	/// </summary>
	public void UndoPhase()
	{
		phaseIndex--;
		if(phaseIndex>=0)
		{
			records.RemoveAt(phaseIndex);
			phaser = skill.phasers[phaseIndex];
		}
		Debug.Log("-pahseIndex: "+phaseIndex);
	}

	void SetupSkillRecord()
	{
		if(records == null)
			records = new List<SkillTargetRecord>();
		else 
			records.Clear();
	}
}