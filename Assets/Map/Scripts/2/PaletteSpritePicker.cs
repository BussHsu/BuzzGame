using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaletteSpritePicker : SpritePicker
{
	public PaletteTheme[] theme;
	public Painter painter;
	Dictionary<Color,SpritePicker> map = new Dictionary<Color, SpritePicker>();

	void Start()
	{
		for(int i= theme.Length-1;i>=0;i--)
		{
			map.Add(theme[i].color,theme[i].spritePicker);
		}
	}
	
	public override Sprite Pick (int x, int y)
	{
		Color c = painter.Render(x,y);
		SpritePicker sp = map.ContainsKey(c)?map[c]:theme[0].spritePicker;
		return sp.Pick(x,y);
	}

}


[System.Serializable]
public class PaletteTheme
{
	public Color color;
	public SpritePicker spritePicker;
}