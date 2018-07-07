using UnityEngine;
using System.Collections;

public class RandomSpritePicker: SpritePicker
{
	public Sprite[] sprites;
	public bool persistant = true;
	
	public override Sprite Pick (int x, int y)
	{
		if(persistant)
			return PickPersistantRandom(x,y);
		else
			return PickRandom(x,y);
	}
	
	Sprite PickPersistantRandom(int x, int y)
	{
		int oldSeed = Random.seed;
		Random.seed = (x+3)^(y+7);
		Sprite retVal = sprites[Random.Range(0,sprites.Length)];
		Random.seed = oldSeed;
		return retVal;
	}
	
	Sprite PickRandom(int x, int y)
	{
		return sprites[Random.Range(0,sprites.Length)];
	}
}
