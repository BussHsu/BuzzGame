using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbilityRange : MonoBehaviour 
{
	public int horizontal = 1;
	public int vertical = int.MaxValue;
	public bool centerAtUnit = true;
	public Point centerPos;
	public TargetFilter filter = TargetFilter.None;
	public virtual bool directionOriented{get{return false;}}
	protected Unit unit { get {return GetComponentInParent<Unit>();}}

	public virtual Tile SetCenter(Board board)
	{
		return centerAtUnit?unit.tile:board.GetTile(centerPos);
	}

	public abstract List<Tile> GetTilesInRange(Board board);
}
