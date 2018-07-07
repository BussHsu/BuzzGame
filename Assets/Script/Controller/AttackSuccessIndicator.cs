using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AttackSuccessIndicator : MonoBehaviour 
{
	const string ShowKey = "Show";
	const string HideKey = "Hide";


	[SerializeField] Panel panel;
	[SerializeField] Canvas canvas;
	[SerializeField] Image arrow;
	[SerializeField] Text label;
	Tweener transition;

	void Start()
	{
		panel.SetPosition(HideKey,false);
		canvas.gameObject.SetActive(false);
	}

	public void Show()
	{
		canvas.gameObject.SetActive(true);
		SetPosition(ShowKey);
	}

	public void Hide()
	{
		SetPosition(HideKey);
		transition.easingControl.completedEvent += delegate(object sender, System.EventArgs e) {
			canvas.gameObject.SetActive(false);
		};
	}

	public void SetStat(int chance, int amount)
	{
		arrow.fillAmount = (chance/100f);
		label.text = string.Format("{0}% {1} pt(S)",chance,amount);
	}

	void SetPosition(string pos)
	{
		if(transition!=null && transition.easingControl.IsPlaying)
			transition.easingControl.Stop ();

		transition = panel.SetPosition(pos,true);
		transition.easingControl.duration = 0.5f;
		transition.easingControl.equation = EasingEquations.EaseInOutQuad;
	}

}
