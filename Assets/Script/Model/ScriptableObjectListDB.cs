using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptableObjectListDB<T> : ScriptableObject
{
	List<T> list;

	public static ScriptableObjectListDB<U> GetDB<U> (string folderPath,string folderName, string assetName)
	{
		if(!AssetDatabase.IsValidFolder(folderPath+"/"+folderName))
		{
			AssetDatabase.CreateFolder(folderPath,folderName);
		}
		string fullPath = folderPath + "/" + folderName + "/" + assetName + ".asset";
		ScriptableObjectListDB<U> asset = AssetDatabase.LoadAssetAtPath (fullPath,typeof(ScriptableObjectListDB<U>)) as ScriptableObjectListDB<U>;
		if (asset == null) 
		{
			asset = ScriptableObject.CreateInstance<ScriptableObjectListDB<U>> ();
			AssetDatabase.CreateAsset(asset,fullPath);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		return asset;

	}
	 
	public void Add(T item)
	{
		list.Add (item);
		EditorUtility.SetDirty (this);
	}

	public void Remove(T item)
	{
		list.Remove (item);
		EditorUtility.SetDirty (this);
	}
	
}
