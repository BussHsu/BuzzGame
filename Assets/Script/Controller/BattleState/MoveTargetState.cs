using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTargetState : BattleState
{
	List<Tile> tiles;

	public override void Enter()
	{
		base.Enter();
		Movement mover = turn.actor.GetComponent<Movement> ();
		tiles = mover.GetTileInRange (board);//儲存可移動的範圍
		board.SelectTiles (tiles);//顯示移動範圍
		RefreshPrimaryStatPanel(pos);
	}

	public override void Exit()
	{
		base.Exit ();
		board.DeSelectTiles (tiles);//取消顯示的移動範圍
		tiles = null;//清空儲存的移動範圍
		statPanelController.HidePrimary();
	}

	protected override void OnMove(object sender, InfoEventArgs<Point> e)
	{
		SelectTile(e.info + pos);
		RefreshPrimaryStatPanel(pos);
	}

	protected override void OnFire(object sender, InfoEventArgs<int> e)
	{
		if (e.info == 0) 
		{
			if (tiles.Contains (owner.currentTile))
				owner.ChangeState<MoveSequenceState> ();
		}
		else 
		{
			owner.ChangeState<CommandSelectionState>();
		}
	}
}