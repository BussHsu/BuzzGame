using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(WorldMapCreator))]
public class WorldMapCreatorInspector : Editor 
{
	public WorldMapCreator current{get{return (WorldMapCreator) target;}}

	string fileName = "";
	string cityName ="";
	int dest1,dest2,nodes;
	Color c = Color.black;
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		#region Save & Load WorldMap

		GUILayout.BeginVertical("Box");
		GUILayout.Box("Save & Load");
			GUILayout.BeginHorizontal();
			GUILayout.Label("FileName: ",GUILayout.Width(100));
			fileName = GUILayout.TextArea(fileName);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
		if(GUILayout.Button("Save")){current.SaveMap(fileName);}
		if(GUILayout.Button("Load")){current.LoadMap(fileName);}
			GUILayout.EndHorizontal();

		GUILayout.EndVertical();
		#endregion

		#region add new City
		GUILayout.BeginVertical("Box");
		GUILayout.BeginHorizontal();
		GUILayout.Label("CityName: ",GUILayout.Width(100));
		cityName = GUILayout.TextArea(cityName);
		GUILayout.EndHorizontal();
		if(GUILayout.Button("Add New City")){current.CreateNewCity(cityName); cityName = "";}

		
		GUILayout.EndVertical();
		#endregion

		#region add new Path
		GUILayout.BeginVertical("Box");

		GUILayout.BeginHorizontal();
		dest1 = EditorGUILayout.IntField("From: ",dest1);
		dest2 = EditorGUILayout.IntField("To: ",dest2);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		nodes = EditorGUILayout.IntField("Nodes: ",nodes);
		c = EditorGUILayout.ColorField(c);
		GUILayout.EndHorizontal();

		if(GUILayout.Button("Add New Path")){current.CreateNewPath(dest1,dest2,nodes,c);}
		
		GUILayout.EndVertical();
		#endregion

		if(GUILayout.Button("Clear")){current.ClearMap();}



	}

}
