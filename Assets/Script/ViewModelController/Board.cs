using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour//將Map化為棋盤
{
	[SerializeField] GameObject HexTile;

	public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();//統一跟MapCreator用一樣的
	public Point[] Neighbor;//建一個Array用來找鄰格

	public Point min { get { return _min; }}
	public Point max { get { return _max; }}

	Point _min;
	Point _max;
	Point pos;

	Color selectedTileColor = new Color (0, 0.5f, 1, 0.5f);
	Color selectedTargetColor = new Color (1, 0.5f, 0, 0.5f);

	public void LoadMap(Map data)
	{			
		_min = new Point(int.MaxValue, int.MaxValue);
		_max = new Point(int.MinValue, int.MinValue);

		for (int i = 0; i < data.TilesInMap.Count; i++) 
		{
			Tile tile = ((GameObject)Instantiate(HexTile)).GetComponent<Tile>();
			tile.Load (data.TilesInMap[i]);
			tiles.Add (tile.pos, tile);
			tile.transform.SetParent(transform);
			_min.x = Mathf.Min(_min.x, tile.pos.x);
			_min.y = Mathf.Min(_min.y, tile.pos.y);
			_max.x = Mathf.Max(_max.x, tile.pos.x);
			_max.y = Mathf.Max(_max.y, tile.pos.y);
		}
	}

	Point[] FindNeighbor(Point p)
	{
		if(p.y % 2 == 0)
		{
			Neighbor = new Point[]
			{
				new Point(-1, -1),
				new Point(0, -1),
				new Point (1, 0),
				new Point(0, 1),
				new Point(-1, 1),
				new Point (-1, 0)
			};
			
			return Neighbor;
		}
		else
		{
			Neighbor = new Point[]
			{
				new Point(0, -1),
				new Point(1, -1),
				new Point (1, 0),
				new Point(1, 1),
				new Point(0, 1),
				new Point (-1, 0)
			};
			
			return Neighbor;
		}
	}

	public List<Tile> Search (Point p, Func<Tile, Tile, bool> AddTile, Action<Tile,Tile> onAddTile)
	{
		if (tiles.ContainsKey (p))
			return Search (tiles [p], AddTile, onAddTile);
		else 
		{
			Debug.LogError("start position didn't exist");
			return null;
		}
	}

	public List<Tile> Search (Tile start, Func<Tile, Tile, bool> AddTile, Action<Tile,Tile> onAddTile)//Func是個有回傳値的委派??
	{
		List<Tile> retValue = new List<Tile> ();
		retValue.Add (start);

		ClearSearch ();
		Queue<Tile> checkNext = new Queue<Tile> ();//定義一個Queue<Tile>佇列，這個Class是先進先出的
		Queue<Tile> checkNow = new Queue<Tile> ();

		start.distance = 0;
        start.TotalCost = 0;
        checkNow.Enqueue (start);//加入一個元素到最尾端

		while (checkNow.Count > 0) 
		{
			Tile t = checkNow.Dequeue();

			FindNeighbor(t.pos);//依位置設定鄰Tile

			for (int i = 0; i < Neighbor.Length; i++)
			{
				Tile next = GetTile(t.pos + Neighbor[i]);//尋鄰

				if(next == null || next.distance <= t.distance + 1) //如果不存在或是已尋過就略過
					continue;

				if(AddTile(t, next))//Func回傳一個bool，此値是判斷是否可繼續尋鄰(超過移動範圍)，如果可(沒有超過移動範圍)就加到CheckNext內
				{

					onAddTile(t,next); 

                    
                    next.prev = t;
					checkNext.Enqueue(next);
					retValue.Add (next);
				}
			}
			if(checkNow.Count == 0)
				SwapReferance(ref checkNow, ref checkNext);
		}

		return retValue;
	}
	/// <summary>
	/// Searchs the radial.
	/// </summary>
	/// <returns>The radial.</returns>
	/// <param name="p">P.</param>
	/// <param name="AddTile">Add tile.</param>
	/// <param name="OnAddTile">On add tile.</param>
	public List<Tile> SearchRadial (Point p, Func<Tile, Tile, bool> AddTile, Action<Tile,Tile> OnAddTile)
	{
		if (tiles.ContainsKey (p))
			return SearchRadial (tiles [p], AddTile, OnAddTile);
		else 
		{
			Debug.LogError("start position didn't exist");
			return null;
		}
	}

	public List<Tile> SearchRadial (Tile start, Func<Tile, Tile, bool> AddTile, Action<Tile,Tile> OnAddTile)
	{
		List<Tile> retValue = new List<Tile>();

		for (int i=0; i<=(int)Directions.None; i++) 
		{
			List<Tile> temp = SearchLine(start,AddTile,OnAddTile,(Directions) i);
			for(int j=0;j<temp.Count;j++)
			{
				retValue.Add(temp[i]);
			}
			retValue.Remove(start);
		}


		return retValue;
	}

	public List<Tile> SearchLine (Tile start, Func<Tile, Tile, bool> AddTile, Action<Tile,Tile> OnAddTile,Directions dir)
	{
		List<Tile> retValue = new List<Tile>();
		
		retValue.Add (start);
		
		ClearSearch ();

		Tile from = start;

		Tile to = start.MoveWithDirection (dir, this);

		while (AddTile(from,to)) 
		{
			retValue.Add(to);
			OnAddTile(from,to);
			to.prev = from;
			from = to;
		}
		return retValue;
	}

	public List<Tile> SearchAttackRange(Tile start, Func<Tile, Tile, bool> AddTile)
	{
		List<Tile> retValue = new List<Tile>();
		retValue.Add(start);

		ClearSearch();
		Queue<Tile> checkNext = new Queue<Tile>();
		Queue<Tile> checkNow = new Queue<Tile>();

		start.distance = 0;
		checkNow.Enqueue(start);

		while (checkNow.Count > 0)
		{
			Tile t = checkNow.Dequeue();

			FindNeighbor(t.pos);

			for (int i = 0; i < Neighbor.Length; i++)
			{
				Tile next = GetTile(t.pos + Neighbor[i]);//尋鄰

				if (next == null || next.distance <= t.distance + 1) 
					continue;

				if (AddTile(t, next))
				{
					next.distance = t.distance + 1;
					next.prev = t;
					checkNext.Enqueue(next);
					retValue.Add(next);
				}
			}
			if (checkNow.Count == 0)
				SwapReferance(ref checkNow, ref checkNext);
		}

		return retValue;
	}

	void SwapReferance (ref Queue<Tile> a, ref Queue<Tile> b)
	{
		Queue<Tile> temp = a;
		a = b;
		b = temp;
	}

	public Tile GetTile(Point p)
	{
		return tiles.ContainsKey (p) ? tiles [p] : null;
	}

	void ClearSearch()	
	{
		foreach(Tile t in tiles.Values)
		{
			t.prev = null;
			t.distance = int.MaxValue;
		}
	}

	public void SelectTiles (List<Tile> tlist)
	{
		for (int i  = tlist.Count - 1; i >= 0; i--)		
		{
			tlist [i].GetComponent<Renderer> ().materials [1].SetColor ("_Color", selectedTileColor);
		}
	}

	public void SelectTiles (List<Tile> tlist , Color color)
	{
		for (int i  = tlist.Count - 1; i >= 0; i--)		
		{
			tlist [i].GetComponent<Renderer> ().materials [1].SetColor ("_Color", color);
		}
	}

	public void DeSelectTiles (List<Tile> tlist)
	{
		for (int i = tlist.Count - 1; i >= 0; i--) 
		{
			Tile t = tlist[i];
			t.SetTileType(t.GridTileType);
		}
	}
}
