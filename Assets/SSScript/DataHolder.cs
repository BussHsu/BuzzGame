using UnityEngine;
using System.Collections;

public class DataHolder : MonoBehaviour 
{
	public static DataHolder instance ;
	public Sprite moveSprite;
	public Sprite actionSprite;
	public Sprite itemSprite;
	public Sprite statusSprite;
	public Sprite waitSprite;

	void Awake()
	{
		instance = this;
	}
	
}
