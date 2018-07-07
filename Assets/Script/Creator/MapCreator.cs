using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapCreator : AssetCreator<Map>
{
	[SerializeField]
	public GameObject HexTile, Cursor;//定義地磚,游標

	[SerializeField]
	int mapSizex, mapSizey, mapSizez, Sizez, TileTypeChoise = 0;//宣告地圖的大小，x為寬y為長(深)z為高，跟Unity內的xyz定義不同

	[SerializeField]
	Point pos;//點

	public Map level;//

	public Dictionary <Point, Tile> map = new Dictionary <Point, Tile>();//建立一個字典由Point 和 Tile組成	

	public string assetName;



	int index = 0;

	void Update ()	
	{
		EditGridTile ();
	}

	Tile TileCreate()//就是生成地磚
	{
		Tile tile = ((GameObject)Instantiate(HexTile)).GetComponent<Tile>();
		tile.transform.parent = transform;
		return tile;
	}

	Tile GetTile(Point p)//如果已存在，抓取該地磚；不然生成一個
	{
		if (map.ContainsKey (p)) return map [p];

		Tile tile = TileCreate();
		tile.Load (p, mapSizez, TileTypeChoise);
		map.Add (p, tile);

		return tile;
	}	

	void CreateMap()
	{
		ClearMap ();
		for (int i = 0; i < mapSizey; i++) 
		{
			for (int j = 0; j < mapSizex; j++)
			{
				pos = new Point (j, i);
				Tile tile = GetTile(pos);
			}
		}
	}


	void RandomCreateMap()//隨機生成地圖
	{
		for (int i = 0; i < mapSizey; i++)
		{
			for(int j = 0; j < mapSizex; j++)
			{
				pos = new Point(j, i);
				Tile tile = GetTile(pos);

				for(int k = 0; k < Random.Range(0, mapSizez); k++){

					if(k % 2 != 0)
					{
						Rect r1 = RandomRect();
						ShrinkRect(r1);
					}
					else
					{
						Rect r2 = RandomRect();
						GrowRect(r2);
					}
				}
			}
		}
		RefreshMap ();
	}

	void ClearMap()//清除地圖，所以要刪除全部的地磚
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}		
		map.Clear ();
	}

	void RefreshMap()
	{
		List<Point> DelList = new List<Point>();

		foreach (Point p in map.Keys)
		{
			Tile t = map [p];
			if(p.x > mapSizex - 1 || p.y > mapSizey - 1 || t.height <= 0)
			{
				DelList.Add(p);
				Destroy (t.gameObject);
			}
		}

		for (int i = 0; i  < DelList.Count; i++)
			map.Remove (DelList[i]);
	}

//	void SaveAsset()//儲存地圖
//	{
//		string filePath = Application.dataPath + "/Resources/Levels";
//
//		if (!Directory.Exists(filePath)) CreateSaveDirectory ();
//		
//		Map board = ScriptableObject.CreateInstance<Map>();
//		board.TilesInMap = new List<Vector4>( map.Count );
//
//		foreach (Tile t in map.Values) 
//		{
//			board.TilesInMap.Add (new Vector4 (t.pos.x, t.height, t.pos.y, t.GridTileType));
//
//		}	
//		
//		string fileName = string.Format("{0}/Stage{1}.asset", filePath, name);
//		AssetDatabase.CreateAsset(board, fileName);
//	}

	protected override string FolderName {
		get {
			return "Levels";
		}
	}

	protected override void EditAssetBeforeSave (Map board)
	{
		board.TilesInMap = new List<Vector4>( map.Count );
		
		foreach (Tile t in map.Values) 
		{
			board.TilesInMap.Add (new Vector4 (t.pos.x, t.height, t.pos.y, t.GridTileType));
			
		}	
	}

	protected override void BuildAfterLoad (Map data)
	{
		foreach (Vector4 v in data.TilesInMap)
		{
			Tile t = TileCreate();
			t.Load(v);
			map.Add(t.pos, t);
		}
	}

//	void LoadMap()//載入地圖
//	{
//		if(level == null) {Debug.Log("Error"); return;}
//		
//		ClearMap ();//清除地圖
//
//		foreach (Vector4 v in level.TilesInMap)
//		{
//			Tile t = TileCreate();
//			t.Load(v);
//			map.Add(t.pos, t);
//		}
//	}

//	void CreateSaveDirectory ()
//	{
//		string filePath = Application.dataPath + "/Resources";
//		if (!Directory.Exists(filePath)) AssetDatabase.CreateFolder("Assets", "Resources");
//
//		filePath += "/Levels";
//		if (!Directory.Exists(filePath)) AssetDatabase.CreateFolder("Assets/Resources", "Levels");
//			
//		AssetDatabase.Refresh();
//	}

	//不知道放哪

	Rect RandomRect ()
	{
		int x = UnityEngine.Random.Range(0, mapSizex);
		int y = UnityEngine.Random.Range(0, mapSizey);
		int w = UnityEngine.Random.Range(1, mapSizex - x);
		int h = UnityEngine.Random.Range(1, mapSizey - y);
		return new Rect(x, y, w, h);
	}
	
	void GrowRect (Rect rect)
	{
		for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
		{
			for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
			{
				Point p = new Point(x, y);
				GrowSingle(p);
			}
		}
	}

	void ShrinkRect (Rect rect)
	{
		for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
		{
			for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
			{
				Point p = new Point(x, y);
				ShrinkSingle(p);
			}
		}
	}

	void GrowSingle (Point p)
	{
		Tile t = GetTile(p);
		if (t.height < mapSizez)
			t.Grow(p);
	}


	void ShrinkSingle (Point p)
	{
		if (!map.ContainsKey(p))
			return;
		
		Tile t = map[p];
		t.Shrink(p);	
	}

	void ChangeAll()
	{
		foreach (Tile t in map.Values)
		{
		
			t.Load (t.pos, Sizez, TileTypeChoise);
		}

	}


	void OnGUI()
	{
		TileTypeData data = Resources.Load<TileTypeData>("TileTypeData/TileTypeData");//編輯用
		string[] showtext = {"Change Height :　", "Change TileType : "};

		if(GUILayout.Button("Random Create Map")) RandomCreateMap ();

		if(GUI.Button(new Rect(10, 50, 100, 30), "Create Map")) CreateMap ();

		if(GUI.Button(new Rect(120, 10, 100, 30), "Refresh Map")) RefreshMap ();

		if(GUI.Button(new Rect(120, 50, 100, 30), "ChangeAll")) ChangeAll ();

		if(GUI.Button(new Rect(230, 10, 100, 30), "Clear Map")) ClearMap ();

		if(GUI.Button(new Rect(230, 50, 100, 30), showtext[0])) index = 0;

		if (GUI.Button (new Rect (340, 10, 100, 30), "Save Map")) SaveAsset(assetName);

		if (GUI.Button (new Rect (340, 50, 100, 30), showtext[1])) index = 1;

		if (GUI.Button (new Rect (450, 10, 100, 30), "Load Map")) LoadAsset(assetName);

		assetName = GUILayout.TextField(assetName,GUILayout.Width(150));

		GUI.Label(new Rect(Screen.width * 0.8f, Screen.height * 0.9f, 200, 30), showtext[index] + data.TileTypeList[TileTypeChoise].Name);
	}

	void EditGridTile()
	{
		TileTypeData data = Resources.Load<TileTypeData>("TileTypeData/TileTypeData");//編輯用

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;		


		TileTypeChoise += (int)(Input.GetAxis ("Mouse ScrollWheel") * 10);
		TileTypeChoise = Mathf.Clamp (TileTypeChoise, 0, data.TileTypeList.Count - 1);
		Sizez = Mathf.Clamp (Sizez, 1, mapSizez);

		if (index == 1)
		{
			if (Input.GetMouseButton (0) && Physics.Raycast (ray, out hit) && hit.transform.tag == "Tile")
			{
				Tile t = hit.transform.GetComponent<Tile> ();
				t.SetTileType (TileTypeChoise);
			}

			if(Input.GetMouseButton (1) && Physics.Raycast (ray, out hit) && hit.transform.tag == "Tile")
			{
				Tile t = hit.transform.GetComponent<Tile> ();
				Point pos = hit.transform.GetComponent<Tile>().pos;

				if(t.height > Sizez)
					ShrinkSingle(pos);
			}
		}
		else
		{
			if (Input.GetMouseButton (0) && Physics.Raycast (ray, out hit) && hit.transform.tag == "Tile")
			{
				Tile t = hit.transform.GetComponent<Tile> ();
				Point pos = hit.transform.GetComponent<Tile> ().pos;
				GrowSingle (pos);
			}
			
			if(Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit) && hit.transform.tag == "Tile")
			{
				Tile t = hit.transform.GetComponent<Tile>();
				Point pos = hit.transform.GetComponent<Tile>().pos;
				ShrinkSingle(pos);
			}
		}
	}
}