using UnityEngine;
using System.Collections;

public abstract class SpritePicker : MonoBehaviour 
{
	public abstract Sprite Pick(int x, int y);
}
