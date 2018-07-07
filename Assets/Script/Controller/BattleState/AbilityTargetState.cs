using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityTargetState : BattleState 
{
	List<Tile> tiles;
	AbilityRange ar;
	
	public override void Enter ()
	{
		base.Enter ();


		ar = turn.phaser.GetComponent<AbilityRange>();
		//選取技能範圍
		SelectTiles ();
		statPanelController.ShowPrimary(turn.actor.gameObject);





		if (ar.directionOriented)
			RefreshSecondaryStatPanel(pos);
		else
		{
			StartCoroutine(DirectToAreaSelection()); 
			
			
		}
	}
	
	IEnumerator DirectToAreaSelection()
	{
		turn.abilityRange = tiles;
		yield return null;
		
		owner.ChangeState<AbilityAreaSelectionState>();
		
	}
	
	public override void Exit ()
	{
		base.Exit ();
		StopCoroutine("DirectToAreaSelection");
		//board.DeSelectTiles(tiles);
		statPanelController.HidePrimary();
		statPanelController.HideSecondary();
	}
	
	protected override void OnMove (object sender, InfoEventArgs<Point> e)
	{
		if (ar.directionOriented)
		{
			ChangeDirection(e.info);
		}
		else
		{
			SelectTile(e.info + pos);
			RefreshSecondaryStatPanel(pos);
		}
	}
	
	protected override void OnFire (object sender, InfoEventArgs<int> e)
	{
		if (e.info == 0)
		{
			if (ar.directionOriented || tiles.Contains(board.GetTile(pos)))
			{
				turn.abilityRange = tiles;
				owner.ChangeState<AbilityAreaSelectionState>();
				
			}
		}
		else
		{
			turn.UndoPhase();



			if(turn.toCatSelectState)		
				owner.ChangeState<CategorySelectionState>();
			else
				owner.ChangeState<AbilityTargetState>();
		}
	}
	
	void ChangeDirection (Point p)
	{
		Directions dir = p.ChangeDirection((int)turn.actor.dir);
		if (turn.actor.dir != dir)
		{
			board.DeSelectTiles(tiles);
			turn.actor.dir = dir;
			turn.actor.Match();
			SelectTiles ();
		}
	}
	
	void SelectTiles ()
	{
		tiles = ar.GetTilesInRange(board);
		board.SelectTiles(tiles);
	}
}