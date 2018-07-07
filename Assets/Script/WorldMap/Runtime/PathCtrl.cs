using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathCtrl:MonoBehaviour 
{
	internal class SplineNode
	{
		internal Vector2 point;
		internal Vector2 rot;
		internal float portion;
		internal Vector2 easeIO;

		internal SplineNode(Vector2 p,Vector2 r, float t, Vector2 io) {point = p; rot = r; portion = t; easeIO = io;}
		internal SplineNode(SplineNode node) {point = node.point; rot = node.rot; portion = node.portion; easeIO = node.easeIO;}
	}

	PathData data;
	List<SplineNode> nodes;
	List<SplineNode> reverseNodes;

	int index = 1;

	public void LoadPath(PathData data)
	{
		this.data = data;
		nodes = new List<SplineNode>();

		//adds point to list
		int count = data.points.Length;
		float portionUnit = 1f/(count-1);

		if(count<2)
		{ Debug.LogError("path must have at least 2 points"); return;}

		for(int i=0;i<count;i++)
		{
			if(i<count-1)
				nodes.Add(new SplineNode(data.points[i],data.points[i+1]-data.points[i],i*portionUnit,new Vector2(0,1)));
			else
				nodes.Add(new SplineNode(data.points[i],data.points[i]-data.points[i-1],i*portionUnit,new Vector2(0,1)));

			Debug.Log(nodes[i].portion);
		}

		//add head and tail for hermite spline
		nodes.Insert(0,nodes[0]);
		nodes.Add(nodes[count-1]);

		reverseNodes = new List<SplineNode>(nodes);
		reverseNodes.Reverse();

		index =1;
	}


	public void PresetPath(bool isReverse)
	{
		index =1; 

	}

	/// <summary>
	/// Gets the position.
	/// </summary>
	/// <returns>The position.</returns>
	/// <param name="t"> t is proportion of the curve.</param>
	/// <param name="isReverse">If set to <c>true</c> is reverse.</param>
	public Vector2 GetPosition(float t,bool isReverse)
	{

		if(t<1f && t>0)
		{
			return FowardTween(t,isReverse); 
		}


		if(t>=1f)
		{
			if(!isReverse)
				return nodes[nodes.Count-2].point;
			else 
				return nodes[1].point;
		}
		else
			return nodes[1].point;
	}



	Vector2 FowardTween(float t, bool reverse)
	{
		if(t>nodes[index+1].portion)								//since the time is equally divided, no need to change for reverse
			index++;
		t= (t-nodes[index].portion)/(nodes[index+1].portion - nodes[index].portion);
		if(!reverse)
			return GetHerminteInternal(nodes,index,t);
		else
			return GetHerminteInternal(reverseNodes,index,t);

	}

	Vector2 GetHerminteInternal(List<SplineNode> nodes,int index, float t)
	{


		float t2 = t * t;
		float t3 = t2 * t;
		
		Vector2 P0 = nodes[index - 1].point;
		Vector2 P1 = nodes[index].point;
		Vector2 P2 = nodes[index + 1].point;
		Vector2 P3 = nodes[index + 2].point;
		
		float tension = 0.5f;	// 0.5 equivale a catmull-rom
		
		Vector2 T1 = tension * (P2 - P0);
		Vector2 T2 = tension * (P3 - P1);
		
		float Blend1 = 2 * t3 - 3 * t2 + 1;
		float Blend2 = -2 * t3 + 3 * t2;
		float Blend3 = t3 - 2 * t2 + t;
		float Blend4 = t3 - t2;
		
		return Blend1 * P1 + Blend2 * P2 + Blend3 * T1 + Blend4 * T2;
	}
	
}
