//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class ActionSelectionState : BaseAbilityMenuState
//{
//	public static int category;
//	string[] axeSkillOptions = new string[] { "Slash", "Break", "Battle Cry" };
//	string[] whiteMagicOptions = new string[] { "Cure", "Raise", "Holy" };
//	string[] blackMagicOptions = new string[] { "Fire", "Ice", "Lightning" };
//
//	public override void Enter ()
//	{
//		base.Enter ();
//		statPanelController.ShowPrimary(turn.actor.gameObject);
//	}
//	
//	public override void Exit ()
//	{
//		base.Exit ();
//		statPanelController.HidePrimary();
//	}
//
//	protected override void LoadMenu()
//	{
//		if (menuOptions == null)
//			menuOptions = new List<string> (3);
//
//		if (category == 0) 
//		{
//			menuTitle = "Axe Skill";
//			SetOption(axeSkillOptions);
//		}
//		else if (category == 1) 
//		{
//			menuTitle = "White Magic";
//			SetOption(whiteMagicOptions);
//		}
//		else
//		{
//			menuTitle = "Black Magic";
//			SetOption(blackMagicOptions);
//		}
//
////		abilityMeunPanelController.Show (menuTitle, menuOptions);
//		commandButtonController.Show (menuTitle, menuOptions);
//	}
//
//	protected override void Confirm()
//	{
//
//
//		if (turn.hasUnitMoved)
//			turn.lockMove = true;
//
//		owner.ChangeState<AbilityTargetState>();
//
//	}
//
//	protected override void Cancel()
//	{
//		owner.ChangeState<CategorySelectionState>();
//	}
//
//	void SetOption(string[] options)
//	{
//		menuOptions.Clear ();
//
//		for (int i = 0; i < options.Length; i++) 
//			menuOptions.Add (options [i]);
//	}
//}