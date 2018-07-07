using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class LayoutAnchor : MonoBehaviour
{
	RectTransform myRT;
	RectTransform parentRT;

	void Awake()
	{
		myRT = transform as RectTransform;
		parentRT = transform.parent as RectTransform;

		if (parentRT == null)
			Debug.LogError ("This component requires a RectTransform parent to work.", gameObject);
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	Vector2 GetRectPosition(RectTransform rt, TextAnchor anchor)	//lower left is origin
	{
		Vector2 retValue = Vector2.zero;

		switch (anchor) 
		{
		case TextAnchor.LowerCenter:
		case TextAnchor.MiddleCenter:
		case TextAnchor.UpperCenter:
			retValue.x += rt.rect.width * 0.5f;
			break;
		case TextAnchor.LowerRight:
		case TextAnchor.MiddleRight:
		case TextAnchor.UpperRight:
			retValue.x += rt.rect.width;
			break;
		}

		switch (anchor) 
		{
		case TextAnchor.MiddleLeft:
		case TextAnchor.MiddleCenter:
		case TextAnchor.MiddleRight:
			retValue.y += rt.rect.height * 0.5f;
			break;
		case TextAnchor.UpperLeft:
		case TextAnchor.UpperCenter:
		case TextAnchor.UpperRight:
			retValue.y += rt.rect.height;
			break;
		}
		return retValue;
	}

	public Vector2 AnchorPosition (TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
	{
		Vector2 myOffset = GetRectPosition (myRT, myAnchor);
		Vector2 parentOffset = GetRectPosition (parentRT, parentAnchor);
		Vector2 anchorCenter = new Vector2 (Mathf.Lerp(myRT.anchorMin.x, myRT.anchorMax.x, myRT.pivot.x), Mathf.Lerp(myRT.anchorMin.y, myRT.anchorMax.y, myRT.pivot.y));
		Vector2 myAnchorOffset = new Vector2 (parentRT.rect.width * anchorCenter.x, parentRT.rect.height * anchorCenter.y);
		Vector2 myPivotOffset = new Vector2 (myRT.rect.width * myRT.pivot.x, myRT.rect.height * myRT.pivot.y);
		Vector2 pos = parentOffset  - myOffset + myPivotOffset - myAnchorOffset + offset;
		pos.x = Mathf.RoundToInt (pos.x);
		pos.y = Mathf.RoundToInt (pos.y);

		return pos;
	}

	public void SnapToAnchorPosition (TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
	{
		myRT.anchoredPosition = AnchorPosition (myAnchor, parentAnchor, offset);
	}

	public Tweener MoveToAnchorPosition (TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
	{
		return myRT.AnchorTo (AnchorPosition(myAnchor, parentAnchor, offset));
	}
}