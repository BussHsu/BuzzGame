using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PerlinVisulizer))]
public class PerlinVisualizerInspector : Editor 
{
	protected PerlinVisulizer perlinVisualizer{get{return (PerlinVisulizer) target;}}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Draw"))
		{
			perlinVisualizer.Draw();
		}

		if(perlinVisualizer.isPlaying && GUILayout.Button("Stop"))
			perlinVisualizer.Stop();
		if(!perlinVisualizer.isPlaying && GUILayout.Button("Play"))
			perlinVisualizer.Play();

	}
}
