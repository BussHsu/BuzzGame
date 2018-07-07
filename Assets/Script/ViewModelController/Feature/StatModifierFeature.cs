using UnityEngine;
using System.Collections;

public class StatModifierFeature : Feature
{
	public StatTypes type;
	public int amount;

	Stats m_stats{get{return m_target.GetComponentInParent<Stats>();}}

	protected override void OnApply()
	{
		m_stats [type] += amount;
	}

	protected override void OnRemove()
	{
		m_stats [type] -= amount;
	}
}