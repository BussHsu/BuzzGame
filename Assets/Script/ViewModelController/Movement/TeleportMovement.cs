using UnityEngine;
using System.Collections;

public class TeleportMovement : Movement 
{
	protected override bool ExpandSearch (Tile from, Tile to)
	{
		return (from.TotalCost + 1) <= moveRange;
	}

	protected override void OnAddTile (Tile t, Tile next)
	{
		next.distance = t.distance + 1;
		next.TotalCost = t.TotalCost + 1;
	}

	public override IEnumerator Traverse(Tile tile)
	{
		unit.Place (tile);

		Tweener spin = Jumper.RotateToLocal (new Vector3 (0, 360, 0), 0.5f, EasingEquations.EaseInOutQuad);
		spin.easingControl.loopCount = 1;
		spin.easingControl.loopType = EasingControl.LoopType.PingPong;

		Tweener shrink = transform.ScaleTo (Vector3.zero, 0.5f, EasingEquations.EaseInBack);

		while (shrink != null)
			yield return null;

		transform.position = tile.GridTileCenter;

		Tweener grow = transform.ScaleTo (Vector3.one * 0.3f, 0.5f, EasingEquations.EaseOutBack);
		while (grow != null)
			yield return null;
	}
}