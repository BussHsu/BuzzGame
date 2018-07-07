using UnityEngine;
using System.Collections;

public class FlyMovement : Movement 
{	
	const float FLY_HEIGHT = 3f;
	const float SPEED = 3f;

	protected override void OnAddTile (Tile t, Tile next)
	{
		next.distance = t.distance + 1;
		next.TotalCost = t.TotalCost + 1;
	}

	protected override bool ExpandSearch (Tile from, Tile to)
	{
		return (from.TotalCost + 1)<= moveRange;
	}

	public override IEnumerator Traverse (Tile tile)
	{

		Tile fromTile = unit.tile;
		unit.Place (tile);

		// take off
		float flyHeightY = tile.HexHeight * tile.height + FLY_HEIGHT;//飛行高度
		float duration = (flyHeightY - Jumper.position.y) /SPEED;//
		Tweener tweener = Jumper.MoveToLocal (new Vector3 (0, flyHeightY, 0), duration, EasingEquations.EaseInOutQuad);

		while (tweener != null)
			yield return null;

		//turn
		Directions dir= fromTile.ChangeDirection (tile); 
		yield return StartCoroutine (Turn (dir));

		//move
		float dist = Mathf.Sqrt (Mathf.Pow (tile.pos.x - fromTile.pos.x, 2) + Mathf.Pow (tile.pos.y - fromTile.pos.y, 2));//畢氏定理求直線
		duration = dist /SPEED;
		tweener = transform.MoveTo (tile.GridTileCenter, duration, EasingEquations.EaseInOutQuad);
		while (tweener != null)
			yield return null;

		//landing
		duration = (flyHeightY - tile.GridTileCenter.y) /SPEED;
		tweener = Jumper.MoveToLocal (Vector3.zero, duration, EasingEquations.EaseInOutQuad);
		while (tweener != null)
			yield return null;
	}
}