using UnityEngine;
using System.Collections;

public class FacingController : MonoBehaviour
{
	[SerializeField] Renderer[] _ArrowRenderer;
	[SerializeField] Material Normal;
	[SerializeField] Material Select;

	public void SetSixWayToOnBattleField (Directions sw)//改變面向
	{
		int index = (int)sw;

		for (int i = 0; i <= _ArrowRenderer.Length - 1; i++) 
			_ArrowRenderer [i].material = (i == index) ? Select : Normal;
	}

}