using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseCommandButtonState
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
		if (menuOptions == null) 
		{
			menuTitle = "Commands";
			menuOptions = new List<ButtonData>(5);
			menuOptions.Add(new ButtonData("Move",DataHolder.instance.moveSprite));
			menuOptions.Add(new ButtonData("Action",DataHolder.instance.actionSprite));
			menuOptions.Add(new ButtonData("Item",DataHolder.instance.itemSprite));
			menuOptions.Add(new ButtonData("Status",DataHolder.instance.statusSprite));
			menuOptions.Add(new ButtonData("Wait",DataHolder.instance.waitSprite));
		}


//		abilityMeunPanelController.Show (menuTitle, menuOptions);
//		abilityMeunPanelController.SetLocked (0, turn.hasUnitMoved);
//		abilityMeunPanelController.SetLocked (1, turn.hasUnitActed);
//		abilityMeunPanelController.SetLocked (2, turn.hasUnitActed);
		commandButtonController.Show (menuTitle, menuOptions);
		commandButtonController.SetLocked (0, turn.hasUnitMoved);
		commandButtonController.SetLocked (1, turn.hasUnitActed);
		commandButtonController.SetLocked (2, turn.hasUnitActed);
	}

	protected override void Confirm()
	{
		switch (/*abilityMeunPanelController.selection*/commandButtonController.selection) 
		{
		case 0:
		    	owner.ChangeState<MoveTargetState>();
		    	break;
		case 1:
		    	owner.ChangeState<CategorySelectionState>();
		    	break;
		case 2:
                Debug.Log("使用道具");
				//owner.ChangeState<UseItemState>();
				break;
		case 3:
			    Debug.Log("顯示角色狀態視窗");//顯示角色狀態視窗
			    break;
		case 4:
               owner.ChangeState<EndFacingState>();
			break;
		}
	}

	protected override void Cancel()
	{
		if (turn.hasUnitMoved && !turn.lockMove) {
			turn.UndoMove ();
			commandButtonController.SetLocked(0,false);
//			abilityMeunPanelController.SetLocked (0, false);
			SelectTile (turn.actor.tile.pos);
		} 
		else 
		{
			commandButtonController.Hide();
			//abilityMeunPanelController.Hide();
			owner.ChangeState<ExploreState>();
		}
	}
}