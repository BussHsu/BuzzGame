using UnityEngine;
using System.Collections;

public class ConstantAbilityRange : AbilityRange
{
	public override System.Collections.Generic.List<Tile> GetTilesInRange (Board board)
	{
		Tile origin = unit.tile;
		if(!centerAtUnit)
			origin = board.GetTile (centerPos);
		return board.Search(origin,ExpandSearch,OnAddTile);
	}

	protected virtual bool ExpandSearch(Tile from, Tile to)//限制移動範圍，先限制最基本的移動力
	{

		return from.distance+1< horizontal && Mathf.Abs(from.height - to.height)<vertical;

	}
	
	protected virtual void OnAddTile(Tile t,Tile next)	//When Search method add a new tile, do the process to new tile
	{
			next.distance = t.distance + 1;
	}
}
