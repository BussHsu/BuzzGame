using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConeAbilityRange : AbilityRange 
{
	public override bool directionOriented {
		get {
			return true;
		}
	}


	#region implemented abstract members of AbilityRange

	public override  List<Tile> GetTilesInRange (Board board)
	{
		List<Tile> retVal = new List<Tile>();
		Directions dir = unit.dir;
		Tile origin = unit.tile;
		if(!centerAtUnit)
			origin = board.GetTile(centerPos);
	
		Queue<Tile> Red = new Queue<Tile>();
		Queue<Tile> Green = new Queue<Tile>();
		Green.Enqueue(origin);
		for(int i=1; i<= horizontal; i++)
		{
			while(Green.Count>0)
			{
				Tile greenTile = Green.Dequeue();
				Tile next = greenTile.MoveWithDirection(dir,board);
				if(ValidTile(next))
				{
					retVal.Add(next);
					Red.Enqueue(next);
				}
			}
		

		int redCount = Red.Count;

		for(int a =0; a<redCount;a++)
		{
			Tile redTile = Red.Dequeue();
			if(a==0)
			{
				Tile upTile = redTile.MoveWithDirection(dir.PreviousDirection(),board);
				if(ValidTile(upTile))
				{
					retVal.Add(upTile);
					Green.Enqueue(upTile);
				}
			}

			Tile midTile = redTile.MoveWithDirection(dir,board);
			if(ValidTile (midTile))
			{
				retVal.Add(midTile);
				Green.Enqueue(midTile);
			}

			if(a== redCount-1)
			{
				Tile lowTile = redTile.MoveWithDirection(dir.NextDirection(),board);
				if(ValidTile(lowTile))
				{
					retVal.Add(lowTile);
					Green.Enqueue(lowTile);
				}
			}
		}
		
		}
		retVal.Remove(origin);
		return retVal;

	}

	#endregion


	bool ValidTile(Tile t)
	{
		return t!=null&& Mathf.Abs(t.height - unit.tile.height)<= vertical;
	}
}
