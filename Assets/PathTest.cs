using UnityEngine;
using System.Collections;

public class PathTest : MonoBehaviour 
{
	public RectTransform heroRT;
	PathCtrl path;
	void Awake()
	{
		PathData data = Resources.Load<WorldMapData>("WorldMap/map1").Paths[0];
		path =GetComponent<PathCtrl>();
		path.LoadPath(data);
	}

	void Start()
	{
		RectTransformPathTwenner tweener = heroRT.gameObject.AddComponent<RectTransformPathTwenner>();
		tweener.path = path;
		tweener.isReverse = true;
		tweener.easingControl.duration = 5;
		tweener.easingControl.equation = EasingEquations.Linear;


	}
}
