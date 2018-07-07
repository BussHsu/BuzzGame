using UnityEngine;
using System.Collections;

public abstract class Vector3Tweener : Tweener
{
	protected Vector3 _startValue;
	protected Vector3 _endValue;

	public virtual Vector3 startValue{get; set;}
	public virtual Vector3 endValue{get;set;}
	public Vector3 currentValue { get; private set; }
	
	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		currentValue = (endValue - startValue) * easingControl.currentValue + startValue;		//move along a linear path
	}
}
