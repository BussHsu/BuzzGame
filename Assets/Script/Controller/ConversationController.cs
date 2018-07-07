using UnityEngine;
using System;
using System.Collections;

public class ConversationController : MonoBehaviour
{
	public static event EventHandler completeEvent;

	[SerializeField] ConversationPanel leftPanel;
	[SerializeField] ConversationPanel rightPanel;

	Canvas canvas;
	IEnumerator conversation;
	Tweener transition;

	const string ShowTop = "Show Top";
	const string ShowBottom = "Show Bottom";
	const string HideTop = "Hide Top";
	const string HideBottom = "Hide Bottom";

	void Start()
	{
		canvas = GetComponentInChildren<Canvas>();

		if (leftPanel.panel.CurrentPosition == null)
			leftPanel.panel.SetPosition (HideBottom, false);

		if (rightPanel.panel.CurrentPosition == null)
			rightPanel.panel.SetPosition (HideTop, false);

		canvas.gameObject.SetActive (false);
	}

	public void Show(ConversationData data)
	{
		canvas.gameObject.SetActive (true);
		conversation = Sequence (data);
		conversation.MoveNext ();
	}

	public void Next()
	{
		if (conversation == null || transition != null)
			return;

		conversation.MoveNext ();
	}

	IEnumerator Sequence(ConversationData data)
	{
		for (int i = 0; i < data.Messageslist.Count; i++) 
		{
			SpeakerData sd = data.Messageslist[i];

			ConversationPanel currentPanel = (sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.MiddleLeft || sd.anchor == TextAnchor.LowerLeft) ? leftPanel : rightPanel;
			IEnumerator presenter = currentPanel.Display(sd);
			presenter.MoveNext();

			string show, hide;
			if(sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.UpperCenter || sd.anchor == TextAnchor.UpperRight)
			{
				show = ShowTop;
				hide = HideTop;
			}
			else
			{
				show = ShowBottom;
				hide = HideBottom;
			}

			currentPanel.panel.SetPosition(hide, false);
			MovePanel(currentPanel, show);

			yield return null;

			while(presenter.MoveNext())
				yield return null;

			MovePanel(currentPanel, hide);
			transition.easingControl.completedEvent += delegate (object sender, EventArgs e) 
			{
				conversation.MoveNext();
			};		
			yield return null;
		}

		canvas.gameObject.SetActive (false);
		if (completeEvent != null)
			completeEvent (this, EventArgs.Empty);

	}
	void MovePanel(ConversationPanel obj, string pos)
	{
		transition = obj.panel.SetPosition (pos, true);
		transition.easingControl.duration = 0.2f;
		transition.easingControl.equation = EasingEquations.EaseOutQuad;
	}
}