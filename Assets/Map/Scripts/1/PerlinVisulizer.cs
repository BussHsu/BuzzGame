using UnityEngine;
using System.Collections;

public class PerlinVisulizer : MonoBehaviour 
{
	public RenderLayer[] layers;
	public Vector2 textureSize = new Vector2(32f,32f);
	public bool isPlaying {get; private set;}
	 
	Texture2D texture;
	Color[] pix;

	 


	
	void OnEnable()
	{
//		choose the node filter
		 
		texture = new Texture2D((int)textureSize.x,(int)textureSize.y,TextureFormat.ARGB32,false);
		texture.filterMode = FilterMode.Point;
		GetComponent<Renderer>().material.mainTexture = texture;
		pix = new Color[texture.width*texture.height];
	}

	void OnDisable ()
	{
		Destroy(texture);
		texture = null;
	}

	void Start()
	{
		Draw();
	}

	public void Play()
	{
		if(!isPlaying)
		{
			isPlaying = true;
			StartCoroutine("DrawLoop");
		}
	}

	public void Stop ()
	{
		if (isPlaying)
		{
			StopCoroutine("DrawLoop");
			isPlaying = false;
		}
	}

	IEnumerator DrawLoop ()
	{
		while (true)
		{
			yield return null;
			Draw ();
		}
	}

	public void Draw ()
	{
		for (int y = 0; y < texture.height; ++y)
		{
			for (int x = 0; x < texture.width; ++x)
			{
				Color c= Color.black;
				for(int i=0;i<layers.Length;i++)
				{
					c+=layers[i].RenderMap(x,y);
				}

				pix[y * texture.width + x] = c;
			}
		}
		texture.SetPixels(pix);
		texture.Apply();
	}
}
