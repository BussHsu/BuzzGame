using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Movement : MonoBehaviour 
{
	public int moveRange { get { return _stats[StatTypes.Mov]; } }
	public int jumpToUp { get { return _stats[StatTypes.JtU]; } }
	public int jumpToDown { get { return _stats[StatTypes.JtD]; } }
	protected Unit unit;
	protected Transform Jumper;
	protected Stats _stats;

	protected virtual void Start () 	
	{
		_stats = GetComponent<Stats>();
	}	



	protected virtual void Awake()
	{
		unit = GetComponent<Unit> ();
		Jumper = transform.FindChild ("Jumper");
	}

	public virtual List<Tile> GetTileInRange (Board board)//由單位所在的Tile開始尋找範圍內可抵達的終點
	{
		List<Tile> retValue = board.Search (unit.tile, ExpandSearch, OnAddTile);
		Filter (retValue);
		return retValue;
	}

	protected abstract bool ExpandSearch(Tile from, Tile to);//限制移動範圍，先限制最基本的移動力
//	{
//		if((int)unit.moveType == 0)
//			return (from.TotalCost + to.TileCost) <= moveRange;
//		else
//			return (from.TotalCost + 1) <= moveRange;
//	}

	protected abstract void OnAddTile(Tile t,Tile next);	//When Search method add a new tile, do the process to new tile
//	{
//		if ((int)unit.moveType == 0)//步兵用
//		{
//			next.distance = t.distance + 1;
//			next.TotalCost = t.TotalCost + next.TileCost;
//		}
//		else
//		{
//			next.distance = t.distance + 1;
//			next.TotalCost = t.TotalCost + 1;
//		}
//
//	}

	protected virtual void Filter (List<Tile> tlist)//略過有MapObject的Tile
	{
		for (int i = tlist.Count - 1; i >= 0; i--)
			if (tlist[i].content != null)
				tlist.RemoveAt (i);
	}

	public abstract IEnumerator Traverse (Tile tile);//在路徑上移動的動畫，根據不同移動類型有不同的演出(腳本)

	protected virtual IEnumerator Turn (Directions sw)//轉向
	{
		TransformLocalEulerTweener tweener = (TransformLocalEulerTweener)transform.RotateToLocal(sw.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

		Vector3 temp = (tweener.startValue - tweener.endValue);

		if (temp.y > 180f)
			tweener.startValue = new Vector3 (tweener.startValue.x, tweener.startValue.y - 360f, tweener.startValue.z);
		else if(temp.y < -180f)
			tweener.startValue = new Vector3 (tweener.startValue.x, tweener.startValue.y + 360f, tweener.startValue.z);

		unit.dir = sw;

		while (tweener != null)
			yield return null;
	}
}