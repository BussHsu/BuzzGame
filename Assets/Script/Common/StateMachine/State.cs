using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour 
{
	public virtual void Enter()
	{
//		Debug.Log("Enter: "+this.GetType().ToString());
		AddListeners ();
	}

	public virtual void Exit()
	{
//		Debug.Log("Exit: "+ this.GetType().ToString());
		RemoveListeners ();
	}

	protected virtual void OnDestroy()
	{
		RemoveListeners ();
	}

	protected virtual void AddListeners()
	{

	}

	protected virtual void RemoveListeners()
	{
		
	}
}