using UnityEngine;
using System.Collections;

public class StatPanelController : MonoBehaviour 
{
	#region Const
	const string ShowKey = "Show";
	const string HideKey = "Hide";
	#endregion

	#region Field
	[SerializeField] StatPanel primaryStatPanel;
	[SerializeField] StatPanel secondaryStatPanel;
	Tweener primaryPanelTweener;
	Tweener secondaryPanelTweener;
	#endregion

	#region MonoBehaviour
	void Start()
	{
//		if(primaryStatPanel.panel.CurrentPosition ==null)
//		{
			primaryStatPanel.panel.SetPosition(HideKey,false);
//		}

//		if(secondaryStatPanel.panel.CurrentPosition ==null)
//		{
			secondaryStatPanel.panel.SetPosition(HideKey,false);
//		}
	}
	#endregion

	#region Public
	public void ShowPrimary(GameObject obj)
	{
		primaryStatPanel.Display(obj);
		MovePanel(primaryStatPanel,ShowKey,ref primaryPanelTweener);
	}

	public void HidePrimary()
	{
		MovePanel(primaryStatPanel,HideKey,ref primaryPanelTweener);
	}

	public void ShowSecondary(GameObject obj)
	{
		secondaryStatPanel.Display(obj);
		MovePanel(secondaryStatPanel,ShowKey,ref secondaryPanelTweener);
	}
	
	public void HideSecondary()
	{
		MovePanel(secondaryStatPanel,HideKey,ref secondaryPanelTweener);
	}
	#endregion

	#region Private
	void MovePanel(StatPanel obj, string pos, ref Tweener t)
	{
		Panel.Position target = obj.panel[pos];
		if(obj.panel.CurrentPosition != target)
		{
			if(t!=null&& t.easingControl!=null)
				t.easingControl.Stop ();

			t= obj.panel.SetPosition(target,true);
			t.easingControl.duration = 0.5f;
			t.easingControl.equation = EasingEquations.EaseOutQuad;

		}
	}
	#endregion
}
