using UnityEngine;
using System.Collections;

public abstract class Feature : MonoBehaviour
{
	protected GameObject m_target{ get; private set;}

	public void Activate (GameObject target)
	{
		if (m_target == null) 
		{
			m_target = target;
			OnApply();
		}
	}

	public void Deactivate ()
	{
		if (m_target != null) 
		{
			OnRemove();
			m_target = null;
		}
	}


	/// <summary>
	/// use only once items
	/// </summary>
	/// <param name="target">Target.</param>
	public void Apply(GameObject target)
	{
		m_target = target;
		OnApply ();
		m_target = null;
	}

	protected abstract void OnApply();
	protected virtual void OnRemove(){}
}