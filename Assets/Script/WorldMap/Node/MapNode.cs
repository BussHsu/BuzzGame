using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]	[System.Serializable]
public abstract class MapNode : MonoBehaviour 
{
	public RectTransform RT{get{return GetComponent<RectTransform>();}}
	public Color color = Color.yellow;
	public bool revealed;
	[SerializeField]
	List<MapNode> neighbors;
	MapNode previous;
	public virtual int cost{get{return 0;}}

	public abstract bool ConnectCity(int cityNum);
}
