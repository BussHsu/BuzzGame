using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public abstract class AssetLoader<T> : MonoBehaviour where T:ScriptableObject
{

	protected virtual string FilePath {get{return "Assets/Resources/";}}	//Application.dataPath+
	protected abstract string FolderName{get;}

	protected void LoadAsset(string fileName) //載入地圖
	{	
		if(string.IsNullOrEmpty(fileName))
		{
			Debug.LogError("Please enter name");
			return;
		}
		
		PreLoadCleanUp();
		string fullLoadDirectory =string.Format("{0}{1}/{2}.asset",FilePath,FolderName,fileName);
		if(!File.Exists(fullLoadDirectory))//Directory.Exists(fullLoadDirectory))
		{
			Debug.LogError("File doesn't exist" + fullLoadDirectory);
			return;
		}
		
		T asset = AssetDatabase.LoadAssetAtPath<T>(fullLoadDirectory);
		BuildAfterLoad(asset);
		
	}
	protected virtual void PreLoadCleanUp(){}
	protected abstract void BuildAfterLoad(T data);
}
