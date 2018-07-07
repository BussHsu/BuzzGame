using UnityEngine;
using System.Collections;

public abstract class Effect : MonoBehaviour 
{
	public virtual void Apply(GameObject[] targets)
	{
		for(int i=0;i<targets.Length;i++)
			Apply (targets[i]);
	}
	public abstract void Apply(GameObject target); 
}
