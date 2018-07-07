using UnityEngine;
using System.Collections;

public class FixedSpritePicker : SpritePicker
{
	public Sprite sprite;
	public override Sprite Pick (int x, int y)
	{
		return sprite;
	}
}



