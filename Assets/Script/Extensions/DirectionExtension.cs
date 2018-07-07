using UnityEngine;
using System.Collections;

public static class DirectionExtension
{
	public static Directions ChangeDirection (this Tile from, Tile to)
	{
		if (from.pos.y == to.pos.y) 
		{
			if(from.pos.x == to.pos.x+1)
				return Directions.f_Left;
			else if (from.pos.x+1 == to.pos.x)
				return Directions.c_Right;
			else 
			{Debug.LogError("GetDirection Error: X difference is too big, two tiles must be adjascent to each other."); return Directions.None;}
		}
		if (from.pos.y % 2 == 0)
		{
			if(to.pos.y == from.pos.y+1) // target is higher
			{
				if(to.pos.x == from.pos.x)
					return Directions.b_UpRight;
				else if(to.pos.x == from.pos.x-1)
					return Directions.a_UpLeft;
				else 
				{Debug.LogError("GetDirection Error: X pos is wrong, two tiles must be adjascent to each other."); return Directions.None;}
			}
			else if (to.pos.y == from.pos.y-1) // target is lower
			{
				if(to.pos.x == from.pos.x)
					return Directions.d_DownRight;
				else if(to.pos.x == from.pos.x-1)
					return Directions.e_DownLeft;
				else 
				{Debug.LogError("GetDirection Error: X pos is wrong, two tiles must be adjascent to each other."); return Directions.None;}
			}
			else
			{
				Debug.LogError("GetDirection Error: Y difference is too big, two tiles must be adjascent to each other."); return Directions.None;
			}
		}
		else 
		{
			if(to.pos.y == from.pos.y+1) // target is higher
			{
				if(to.pos.x == from.pos.x)
					return Directions.a_UpLeft;
				else if(to.pos.x == from.pos.x+1)
					return Directions.b_UpRight;
				else 
				{Debug.LogError("GetDirection Error: X pos is wrong, two tiles must be adjascent to each other."); return Directions.None;}
			}
			else if (to.pos.y == from.pos.y-1) // target is lower
			{
				if(to.pos.x == from.pos.x)
					return Directions.e_DownLeft;
				else if(to.pos.x == from.pos.x+1)
					return Directions.d_DownRight;
				else 
				{Debug.LogError("GetDirection Error: X pos is wrong, two tiles must be adjascent to each other."); return Directions.None;}
			}
			else
			{
				Debug.LogError("GetDirection Error: Y difference is too big, two tiles must be adjascent to each other."); return Directions.None;
			}
		}
	}

	public static Directions ChangeDirection (this Point p, int i)
	{
		int index = i;

		if(p.x  > 0 || p.y > 0)
			index++;
		else if(p.x  < 0 || p.y < 0)
			index--;

		if (index > 5)
			index = 0;

		if (index < 0)
			index = 5;

		return (Directions)index;
	}	

	public static Vector3 ToEuler (this Directions sw)
	{
		return new Vector3 (0, -30f + (int)sw * 60f, 0);//轉向角度
	}

	/// <summary>
	/// Gets the direction of to Tile. Tiles Must be adjascent
	/// </summary>

	public static Tile MoveWithDirection(this Tile from, Directions dir, Board board)
	{
		switch (dir) {
		case Directions.f_Left:			
			return GetTileWithOffset (from, board, new Point (-1, 0));

		case Directions.c_Right:
			return GetTileWithOffset (from, board, new Point (1, 0));
			
		case Directions.a_UpLeft:
			if (from.pos.y % 2 == 0) {
				return GetTileWithOffset (from, board, new Point (-1, 1));
			} else {
				return GetTileWithOffset (from, board, new Point (0, 1));
			}

		case Directions.b_UpRight:
			if (from.pos.y % 2 == 0) {
				return GetTileWithOffset (from, board, new Point (0, 1));
			} else {
				return GetTileWithOffset (from, board, new Point (1, 1));
			}

		case Directions.d_DownRight:
			if (from.pos.y % 2 == 0) {
				return GetTileWithOffset (from, board, new Point (0, -1));
			} else {
				return GetTileWithOffset (from, board, new Point (1, -1));
			}

		case Directions.e_DownLeft:
			if (from.pos.y % 2 == 0) {
				return GetTileWithOffset (from, board, new Point (-1, -1));
			} else {
				return GetTileWithOffset (from, board, new Point (0, -1));
			}
	
		default:
			Debug.LogError ("No Direction input");
			return null;
		}
	}

	public static Directions PreviousDirection(this Directions dir)
	{
		return (Directions)(((int) dir +5)%6);
	}

	public static Directions NextDirection(this Directions dir)
	{
		return (Directions)(((int) dir +1)%6);
	}

	public static Vector2 GetNormal(this Directions dir)
	{
		switch(dir)
		{
		case Directions.a_UpLeft:
			return new Vector2(-0.5f,Mathf.Sqrt(3/4f));
		case Directions.b_UpRight:
			return new Vector2(0.5f,Mathf.Sqrt(3/4f));
		case Directions.c_Right:
			return new Vector2(1,0);
		case Directions.d_DownRight:
			return new Vector2(0.5f,-Mathf.Sqrt(3/4f));
		case Directions.e_DownLeft:
			return new Vector2(-0.5f,-Mathf.Sqrt(3/4f));
		default:
			return Vector2.left;
		}
	}


	private static Tile GetTileWithOffset(Tile from, Board board, Point p)
	{
		Point targetPos = p + from.pos;
		return board.GetTile(targetPos);
	}
}