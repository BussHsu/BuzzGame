using UnityEngine;
using System.Collections;

public class BallisticV3Tweener : Tweener
{
	public Transform startPoint;
	public Transform endPoint;
	public float heightFromStart;

	public Vector3 startValue {
		get {
			if(startPoint!=null)
				return startPoint.position;
			else 
				return Vector3.zero;
		}

	}

	public  Vector3 endValue {
		get {
			if(endPoint!=null)
				return endPoint.position;
			else
				return Vector3.zero;
		}

	}

	const float g = 9.8f;
	float t1,t2,total;
	Vector3 vec;

	// Use this for initialization
	// need to assue linear time pass
	void Start () 
	{
		if(startValue.y - endValue.y+heightFromStart < 2)
			heightFromStart = endValue.y-startValue.y +2;



		t1 = Mathf.Sqrt(2*heightFromStart/g);
		t2 = Mathf.Sqrt(2*((startValue.y - endValue.y)+heightFromStart)/g);
		total = t1+t2;
////		t2/=total;

		vec = (endValue-startValue)/total;  //(normalized speed)
		vec.y = g*t1;
		easingControl.duration = total;

	}
	
	float t ;
	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		t=easingControl.currentValue*total;
		transform.position = vec*t-0.5f*g*t*t*Vector3.up + startValue;	//vt - 0.5gt^2
//		Debug.Log(transform.position);


//		currentValue = (endValue - startValue) * easingControl.currentValue + startValue;
	}
}
