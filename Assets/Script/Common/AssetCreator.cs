using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Create assets with name under Assets/Resources/FOLDERNAME
/// </summary>
public abstract class AssetCreator<T> : MonoBehaviour where T:ScriptableObject 
{
	protected virtual string FilePath {get{return "Assets/Resources/";}}	//Application.dataPath+
	protected abstract string FolderName{get;}

	protected void SaveAsset(string name)//儲存 asset
	{
		if(string.IsNullOrEmpty(name))
		{
			Debug.LogError("Please enter name");
			return;
		}
				
		if (!Directory.Exists(FilePath+FolderName)) CreateSaveDirectory ();
		
		T asset = ScriptableObject.CreateInstance<T>();

		EditAssetBeforeSave(asset);
		
		string fileName = string.Format("{0}/{1}.asset", FilePath+FolderName, name);
		AssetDatabase.CreateAsset(asset, fileName);
	}

	protected abstract void EditAssetBeforeSave(T target);

	void CreateSaveDirectory ()
	{
		if (!Directory.Exists(FilePath))
			AssetDatabase.CreateFolder(Application.dataPath, "Resources");
		
		if (!Directory.Exists(FilePath+FolderName)) 
			AssetDatabase.CreateFolder(FilePath, FolderName);
		
		AssetDatabase.Refresh();
	}

	protected virtual void PreLoadCleanUp(){}

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

	protected abstract void BuildAfterLoad(T data);
}
