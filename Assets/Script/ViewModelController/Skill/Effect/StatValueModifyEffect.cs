using UnityEngine;
using System.Collections;

public class StatValueModifyEffect : Effect 
{
	public StatTypes statType;
	public int amount;

	public override void Apply (GameObject target)
	{
		Stats stats = target.GetComponent<Stats>();
		stats[statType] += amount;
	}

}
