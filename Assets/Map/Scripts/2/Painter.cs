using UnityEngine;
using System.Collections;

public abstract class Painter : MonoBehaviour
{
	public abstract Color Render(int x, int y);
}
