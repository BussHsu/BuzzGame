using UnityEngine;
using System.Collections;

public class RenderLayerPainter : Painter
{
	public RenderLayer[] layers;
	public override Color Render (int x, int y)
	{
		Color c = Color.black;
		for(int i=0;i<layers.Length;i++)
		{
			c+=layers[i].RenderMap(x,y);
		}

		c.r = Mathf.Clamp01(c.r);
		c.g = Mathf.Clamp01(c.g);
		c.b = Mathf.Clamp01(c.b);
		c.a = Mathf.Clamp01(c.a);
		return c;
	}
 
}
