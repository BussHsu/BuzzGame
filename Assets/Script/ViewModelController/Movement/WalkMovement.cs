using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkMovement : Movement 
{
	protected override void OnAddTile (Tile t, Tile next)
	{
		next.distance = t.distance + 1;
		next.TotalCost = t.TotalCost + next.TileCost;
	}

	//尋找移動路徑，高低差及Tile上是否有MapObject，未來追加地形影響
	protected override bool ExpandSearch(Tile from, Tile to)
	{
		if(((from.height - to.height) < -jumpToUp) || ((from.height - to.height) > jumpToDown))
			return false;

			//移動路徑排除友軍以外及障礙物
		if (to.content != null && to.content.GetComponent<Unit>().unitAlly != unit.unitAlly)
			return false;

			//add code ZOC追加

		return (from.TotalCost + to.TileCost) <= moveRange;
	}

	public override IEnumerator Traverse (Tile tile)
	{
		unit.Place (tile);

		List<Tile> targets = new List<Tile> ();
		while (tile != null) 
		{
			targets.Insert(0, tile);
			tile = tile.prev;
		}

		for (int i = 1; i < targets.Count; i++) 
		{
			Tile from = targets[i - 1];
			Tile to = targets[i];

			Directions sw = from.ChangeDirection(to);

			if(unit.dir != sw)
				yield return StartCoroutine(Turn (sw));

			if(from.height == to.height)
				yield return StartCoroutine(Walk(to));
			else
				yield return StartCoroutine(Jump(to));
		}
		yield return null;
	}

	IEnumerator Walk(Tile target)
	{
		Tweener tweener = transform.MoveTo (target.GridTileCenter, 0.33f, EasingEquations.Linear);
		while (tweener != null)
			yield return null;
	}

	IEnumerator Jump(Tile to)
	{
		Tweener tweener = transform.MoveTo (to.GridTileCenter, 0.5f, EasingEquations.Linear);

		Tweener tweener2 = Jumper.MoveToLocal (new Vector3 (0, to.HexHeight * to.height, 0), tweener.easingControl.duration / 2f, EasingEquations.EaseOutQuad);
		tweener2.easingControl.loopCount = 1;
		tweener2.easingControl.loopType = EasingControl.LoopType.PingPong;

		while (tweener != null)
			yield return null;
	}
}
