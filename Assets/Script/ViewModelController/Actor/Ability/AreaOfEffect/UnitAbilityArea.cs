using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitAbilityArea : AbilityArea
{
	public override List<Tile> GetTilesInArea (Board board, Point pos)
	{
		List<Tile> retVal = new List<Tile>();
		Tile tile = board.GetTile(pos);
		if(tile!=null)
			retVal.Add(tile);
		return retVal;
	}
}
