using UnityEngine;
using System.Collections;

public class StatMaxFeature : Feature {

	public StatTypes type;
	public int amount;
	
	Stats m_stats
	{
		get
		{
			return m_target.GetComponentInParent<Stats>();
		}
	}
	
	protected override void OnApply()
	{
		this.AddObserver (MaxModification,Stats.WillChangeNotification(type));
		m_stats [type] = m_stats[type];
	}
	
	protected override void OnRemove()
	{
		this.RemoveObserver (MaxModification,Stats.WillChangeNotification(type));
	}

	void MaxModification(object sender, object args)
	{
		ValueMaxException vme = args as ValueMaxException;
		vme.AddModifier (new AddValueModifier (1, amount));
	}
}
