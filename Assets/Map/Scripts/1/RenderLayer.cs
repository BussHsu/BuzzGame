using UnityEngine;
using System.Collections;

public class RenderLayer : MonoBehaviour 
{
	public Color color = Color.white;
	public Node[] filters;
	public Mapper mapper;

	public Color RenderMap(int x, int y)
	{
		float pix = mapper.Map(x,y);
		if(filters!=null)
		{
			for (int i=0;i<filters.Length;i++)
			{
				pix = filters[i].Calculate(pix);
			}
		}

		return color*pix;

	}

}
