using UnityEngine;
using System.Collections;

public class RectTransformPathTwenner : Tweener
{
	public PathCtrl path;
	public bool isReverse = false;
	RectTransform rt;

	protected override void Awake ()
	{
		base.Awake ();
		rt = transform as RectTransform;

	}

	IEnumerator Start()
	{
		path.PresetPath(isReverse); 
		yield return new WaitForSeconds(1f);
		easingControl.Play();
	}

	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		Debug.Log(easingControl.currentTime);
		if(path!=null)
		rt.anchoredPosition = path.GetPosition(easingControl.currentValue,isReverse);
	}
}
