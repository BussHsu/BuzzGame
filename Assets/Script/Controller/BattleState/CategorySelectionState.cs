using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CategorySelectionState : BaseCommandButtonState
{
	public override void Enter ()
	{
		base.Enter ();
		statPanelController.ShowPrimary(turn.actor.gameObject);
	}

	public override void Exit ()
	{
		base.Exit ();
		statPanelController.HidePrimary();
	}



	protected override void LoadMenu()
	{
		Skills skills =turn.actor.transform.GetComponentInChildren<Skills> ();
		List<SkillData> list = skills.activeSkills;

		if (menuOptions == null) 
		{
			menuTitle = "Action";
			menuOptions = new List<ButtonData>();
			for(int i=0;i<list.Count;i++)
			{
				menuOptions.Add(new ButtonData(list[i].skill.skillName,list[i].sprite));
			}
//			menuOptions.Add ("SkillA");
//			menuOptions.Add("SkillB");

		}

		commandButtonController.Show (menuTitle, menuOptions);
		
	}

	protected override void Confirm()
	{
		Debug.Log(menuOptions[commandButtonController.selection].title);
		UseSkill(menuOptions[commandButtonController.selection].title);
	}

	protected override void Cancel()
	{
		owner.ChangeState<CommandSelectionState>();
	}

	void UseSkill(string skillName)
	{

		turn.ability = turn.actor.transform.Find("Skills/"+skillName).gameObject;
		turn.NextPhase();
		owner.ChangeState<AbilityTargetState>();
	}

	void Attack ()
	{

		//記下 turn.ability
		turn.ability = turn.actor.GetComponentInChildren<AbilityRange>().gameObject;
		owner.ChangeState<AbilityTargetState>();
	}
}