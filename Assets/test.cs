using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	BallisticV3Tweener tweener;
//	public float duration;
	void Start()
	{
		tweener = GetComponent<BallisticV3Tweener>();
		tweener.easingControl.Play ();
//		tweener.easingControl.duration =duration;
		tweener.easingControl.loopType = EasingControl.LoopType.PingPong;
//		tweener.easingControl.loopCount = -1;
	}
}
