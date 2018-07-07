using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeightedSprite
{
	public Sprite sprite;
	public int weight;
}

public class WeightedRandomSpritePicker : SpritePicker
{
	public WeightedSprite[] options;
	public bool persistant =true;
	int wheelSize = 0;
	int optionCount;
	
	void Start()
	{
		BuildWheel();
	}
	
	void BuildWheel()
	{
		optionCount = options.Length;
		for(int i=0;i< optionCount;i++)
		{
			wheelSize += options[i].weight;
		}
		
	}
	
	public override Sprite Pick (int x, int y)
	{
		if(persistant)
		{
			int oldSeed = Random.seed;
			Random.seed = (x+3)^(y+7);
			Sprite retVal = SpinWheel();
			Random.seed = oldSeed;
			return retVal;
		}
		else
			return SpinWheel( );
	}
	
	Sprite SpinWheel( )
	{
		int roll = UnityEngine.Random.Range(1, wheelSize);
		int sum = 0;
		
		for (int i = 0; i < optionCount; ++i)
		{
			WeightedSprite option = options[i];
			sum += option.weight;
			if (sum >= roll)
				return option.sprite;
		}
		
		return null;
	}
}
