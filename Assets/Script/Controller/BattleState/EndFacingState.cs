using UnityEngine;
using System.Collections;

public class EndFacingState : BattleState
{
	Directions _startSW;

	public override void Enter()
	{
		base.Enter ();
		_startSW = turn.actor.dir;
		faceind.transform.localScale = Vector3.one;
		faceind.transform.position = turn.actor.tile.GridTileCenter;//要抓當前Unit的位置，所以不用turn.startTile
		faceind.SetSixWayToOnBattleField (_startSW);
	}

	public override void Exit ()
	{
		base.Exit ();
		//將指示物消失
		faceind.transform.localScale = Vector3.zero;
	}

	protected override void OnMove(object sender, InfoEventArgs<Point> e)
	{
		turn.actor.dir = e.info.ChangeDirection((int)turn.actor.dir);
		turn.actor.Match ();
		faceind.SetSixWayToOnBattleField (turn.actor.dir);
	}

	protected override void OnFire(object sender, InfoEventArgs<int> e)
	{
		switch (e.info) 
		{
		case 0:
			owner.ChangeState<SelectUnitState>();
			break;
		case 1:
			turn.startDir = _startSW;
			owner.ChangeState<CommandSelectionState>();
			break;
		}
	}
}