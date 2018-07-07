using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour 
{
	GUITexture fadeImage;

	bool _fadeIn;
	bool _fadeOut;
	public bool isFadingIn{get{return _fadeIn;}}
	public bool isFadingOut{get{return _fadeOut;}}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		fadeImage = GetComponent<GUITexture>();
	}

	// Use this for initialization
	void Start () 
	{
		fadeImage.pixelInset = new Rect(0,0,Screen.width,Screen.height);
		FadeIn(2f,Color.black);
	}


	#region Fade In
	IEnumerator FadeInCoroutin(float fadeInTime,Color c)
	{
		_fadeIn = true;
		float alpha =1f;
		fadeImage.enabled = true;
		while(alpha > 0.01f)
		{
			yield return null;
			alpha =Mathf.Clamp01( alpha - Time.deltaTime/fadeInTime);
			c.a = alpha;
			fadeImage.color= c;
		}
		fadeImage.enabled = false;
		_fadeIn = false;
	}

	public void FadeIn(float duration,Color c)
	{
		fadeImage.color = c;
		StartCoroutine(FadeInCoroutin(duration,c));
	}
	#endregion

	#region Fade Out
	IEnumerator FadeOutCoroutin(float fadeOutTime,Color c)
	{
		_fadeOut = true;
		float alpha =0f;
		fadeImage.enabled = true;
		while(alpha < 0.99f)
		{
			yield return null;
			alpha =Mathf.Clamp01(alpha + Time.deltaTime/fadeOutTime);
			c.a = alpha;
			fadeImage.color= c;

		}
		fadeImage.enabled = false;
		_fadeOut = false;
	}
	
	public void FadeOut(float duration,Color c)
	{
		fadeImage.color = c;
		StartCoroutine(FadeOutCoroutin(duration,c));

	}
	#endregion
}
