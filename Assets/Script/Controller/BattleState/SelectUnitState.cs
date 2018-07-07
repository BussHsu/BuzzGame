using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState
{

	public override void Enter()
	{
		base.Enter ();
		StartCoroutine ("ChangeCurrentUnit");
	}

	public override void Exit ()
	{
		statPanelController.HidePrimary();
		base.Exit ();
	}

	IEnumerator ChangeCurrentUnit()
	{

		owner.round.MoveNext();
		SelectTile(turn.actor.pos);
		//RefreshPrimaryStatPanel(turn.actor.pos);

		yield return null;
		owner.ChangeState<CommandSelectionState> ();
	}

//	protected override void OnMove (object sender, InfoEventArgs<Point> e)
//	{
//		SelectTile (e.info + pos);
//	}
//
//	//下面沒改，也沒用，但是不能砍
//	protected override void OnFire(object sender, InfoEventArgs<int> e)
//	{
//		GameObject MapObject = owner.currentTile.content;
//
//		if (MapObject != null) 
//		{
//			owner.currentUnit = MapObject.GetComponent<Unit>();
//			owner.ChangeState<CommandSelectionState>();
//		}
//	}
}