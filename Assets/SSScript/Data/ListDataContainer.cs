using UnityEngine;
using System.Collections;

public abstract class ListDataContainer<T> : MonoBehaviour
{
	protected T data;
	public int order;

	public abstract void LoadAndDisplayData();

}
