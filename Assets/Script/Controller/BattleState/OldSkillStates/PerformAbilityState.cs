using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerformAbilityState : BattleState 
{
	public override void Enter ()
	{
		base.Enter ();
		turn.hasUnitActed = true;
		if (turn.hasUnitMoved)
			turn.lockMove = true;
		StartCoroutine(Animate());
	}
	
	IEnumerator Animate ()
	{
		// TODO play animations, etc
		yield return null;
		// TODO apply ability effect, etc
//		TemporaryAttackExample();

		RenderResults(turn.records);
		
		if (turn.hasUnitMoved)
			owner.ChangeState<EndFacingState>();
		else
			owner.ChangeState<CommandSelectionState>();
	}

	void RenderResults(List<SkillTargetRecord> records)
	{


		for(int i=0; i< records.Count; i++)
		{
			SkillTargetRecord record =records[i];
			Debug.Log("Record: "+ record.phaseNum);
			string targetNames = "";
			for(int j =0; j<record.targets.Length;j++)
				targetNames = string.Format("{0} , {1}",targetNames,record.targets[j].ToString());
			Debug.Log("Targets: "+targetNames);
			Debug.Log("Effect: " +record.effect);
		}
	}

//	void TemporaryAttackExample ()
//	{
//		for (int i = 0; i < turn.targets.Count; ++i)
//		{
//			GameObject obj = turn.targets[i].content;
//			Stats stats = obj != null ? obj.GetComponentInChildren<Stats>() : null;
//			if (stats != null)
//			{
//				stats[StatTypes.b_HP] -= 50;
//				if (stats[StatTypes.b_HP] <= 0)
//					Debug.Log("KO'd Unit!", obj);
//			}
//		}
//	}
}