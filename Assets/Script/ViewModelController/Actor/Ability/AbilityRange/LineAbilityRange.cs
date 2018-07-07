using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineAbilityRange : AbilityRange 
{
	public int range = int.MaxValue;

	public override bool directionOriented {
		get {
			return true;
		}
	}
	
	public override List<Tile> GetTilesInRange (Board board)
	{
		Tile origin = unit.tile;
		Tile curTile = origin;
		Tile nextTile= origin;
		List<Tile> retValue = new List<Tile>();
		int rangeCount = 0;

		while (nextTile!=null && rangeCount<range&& Mathf.Abs(nextTile.height-curTile.height)<vertical)
		{
			curTile = nextTile;
			retValue.Add(curTile);
			nextTile = curTile.MoveWithDirection(unit.dir,board);
			rangeCount++;
		}

		retValue.Remove(origin);

	
		
		return retValue;
	}
	 
}
