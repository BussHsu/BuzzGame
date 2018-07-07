using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PerlinSpriteVisualizer : MonoBehaviour 
{
	#region Properties
	public GameObject tilePrefab;
	public SpritePicker spritePicker;
	public Image[] tiles;
	public int xPos, yPos;
	int _w,_h;
	#endregion

	#region Public
	public void Refresh()
	{
		for(int y = 0; y<_h;y++)
		{
			for(int x=0;x<_w;x++)
			{
				int index = x + _w*y;
				tiles[index].sprite = spritePicker.Pick(x+xPos,y+yPos);
			}
		}
	}
	#endregion

	#region MonoBehaviour
	void Start ()
	{
		Init ();
		Refresh();
	}
	
	void Update ()
	{
		bool dirty = false;
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			yPos++;
			dirty = true;
		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			yPos--;
			dirty = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			xPos--;
			dirty = true;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			xPos++;
			dirty = true;
		}
		if (dirty)
			Refresh();
	}
	#endregion

	#region Private
	void Init()
	{
		RectTransform rt = GetComponent<RectTransform>();
		GridLayoutGroup glg = GetComponent<GridLayoutGroup>();
		_w = Mathf.FloorToInt(rt.rect.width/glg.cellSize.x);
		_h = Mathf.FloorToInt(rt.rect.height/glg.cellSize.y);
		int size = _w*_h;
		tiles = new Image[size];
		for(int i=0;i<size;i++)
		{
			tiles[i]=CreateTile();
		}
	}

	Image CreateTile ()
	{
		GameObject instance = Instantiate(tilePrefab) as GameObject;
		instance.transform.SetParent(transform, false);
		return instance.GetComponent<Image>();
	}

	#endregion
}
