using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AbilityAreaSelectionState : BattleState
{

	List<Tile> tiles;
	List<Point> points;
	AbilityArea aa;
	int index = 0;
	
	public override void Enter ()
	{
		base.Enter ();
		aa = turn.phaser.GetComponent<AbilityArea>();
		tiles = aa.GetTilesInArea(board, pos);
		points = turn.abilityRange.Select(tile=>tile.pos).ToList();
		ConfineInTargetRange(points);
		board.SelectTiles(tiles, Color.magenta);
		// Find the targets in ability area
		FindTargets();
		RefreshPrimaryStatPanel(turn.actor.tile.pos);
	
	}
	
	public override void Exit ()
	{
		base.Exit ();
		board.DeSelectTiles(tiles);
		board.DeSelectTiles(turn.abilityRange);
		statPanelController.HidePrimary();
		statPanelController.HideSecondary();
	}
	
	protected override void OnMove (object sender, InfoEventArgs<Point> e)
	{

		Point p = pos+e.info;
		if(points.Contains(p))
		{
			SelectTile(p);
			board.DeSelectTiles(tiles);
			RefreshSelectArea();
		}
	}
	
	protected override void OnFire (object sender, InfoEventArgs<int> e)
	{
		if (e.info == 0)
		{
			if (turn.targets.Count > 0)
			{
				turn.SavePhaseRecord();
				if(turn.hasNextPhase)
				{
					turn.NextPhase();
					owner.ChangeState<AbilityTargetState>();
				}
				else
				{
					turn.NextPhase();
					owner.ChangeState<ConfirmAbilityTargetState>();
				}
			}
		}
		else if(turn.phaser.GetComponent<AbilityRange>().directionOriented)
			owner.ChangeState<AbilityTargetState>();
		else
		{
			turn.UndoPhase();
			Debug.Log(turn.toCatSelectState?"No Previous Phase":"To Previous Phase");
			if(turn.toCatSelectState)		
				owner.ChangeState<CategorySelectionState>();
			else
				owner.ChangeState<AbilityTargetState>();
		}
	}

	// setup tiles to be target
	void FindTargets ()
	{
		AbilityRange ar = turn.phaser.GetComponent<AbilityRange>();
		AbilityEffectTarget[] targeters = turn.phaser.GetComponents<AbilityEffectTarget>();

		if(ar is SelfAbilityRange)
		{
			if(IsTarget(turn.actor.tile, targeters))
			{
				owner.ChangeState<ConfirmAbilityTargetState>();
				return;
			}
			else
			{
				owner.ChangeState<CategorySelectionState>(); // the ability cannot use;
				return;
			}
		}

		else if (ar is InfiniteAbilityRange)
		{
			turn.targets = new List<Tile>();
			
			for (int i = 0; i < tiles.Count; ++i)
				if (IsTarget(tiles[i], targeters))
					turn.targets.Add(tiles[i]);

			OnFire(null,new InfoEventArgs<int>(0));
		}
		else 
		{
		turn.targets = new List<Tile>();

		for (int i = 0; i < tiles.Count; ++i)
			if (IsTarget(tiles[i], targeters))
				turn.targets.Add(tiles[i]);
		}
	}
	
	bool IsTarget (Tile tile, AbilityEffectTarget[] list)
	{
		for (int i = 0; i < list.Length; ++i)
			if (list[i].IsTarget(tile))
				return true;
		
		return false;
	}

	void RefreshSelectArea()
	{
		board.SelectTiles(turn.abilityRange);
		tiles = aa.GetTilesInArea(board, pos);
		board.SelectTiles(tiles, Color.magenta);
		// Find the targets in ability area
		FindTargets();

	}

	void ConfineInTargetRange(List<Point> points)
	{
		if(!points.Contains(pos))
			SelectTile(points[0]);
	}
	 
}