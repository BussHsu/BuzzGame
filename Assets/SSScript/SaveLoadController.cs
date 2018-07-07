using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadController : MonoBehaviour 
{
	const int SIZE = 10;
//	readonly string SAVEFILE_FOLDER = Application.persistentDataPath+"/SaveData";

	[SerializeField]
	List<SaveData> _saveList;
	public List<SaveData> SaveList{get{return _saveList;}}

	GameObject prefab;
	public GameObject listParentObject;

	void OnEnable()
	{
		_saveList = new List<SaveData>();
		prefab = Resources.Load<GameObject> ("Prefab/DataHolder/SaveSlot");
		Init();
	}
	

//	void Start()
//	{
//		GameObject go = Instantiate<GameObject>(prefab);
//		go.transform.SetParent(listParentObject.transform,false);
//		GridLayoutGroup group = listParentObject.GetComponent<GridLayoutGroup>();
//		go.GetComponent<SaveDataContainer>().order =1;
//
//	}

	void Init()
	{
		for(int i=0;i<SIZE;i++)
		{
			GameObject go = Instantiate<GameObject>(prefab);
			go.transform.SetParent(listParentObject.transform,false);		//!! need to set false!!
			go.GetComponent<SaveDataContainer>().order =i;
		}
	}

//	void DeserilaizeSaveData()
//	{
//		BinaryFormatter bf = new BinaryFormatter();
//		for(int i=0;i<SIZE; i++)
//		{
//			SaveData data;
//			if(File.Exists(string.Format("{0}/save{1}.dat",SAVEFILE_FOLDER,i)))
//			{
//				FileStream  file = File.Open(string.Format("{0}/save{1}.dat",SAVEFILE_FOLDER,i),FileMode.Open);
//				data = (SaveData) bf.Deserialize(file);
//				file.Close();
//			}
//
//			else
//			{
//				data = new SaveData(i);
//			}
//
//			_saveList.Add(data);
//		}
//	}
//
//	void SerializeSaveData(int index,SaveData data)
//	{
//		BinaryFormatter bf = new BinaryFormatter();
//		FileStream  file = File.Open(string.Format("{0}/save{1}.dat",SAVEFILE_FOLDER,index),FileMode.Open);
//		bf.Serialize(file,data);
//		file.Close();
//	}
//
//	public SaveData Load(int index)
//	{
//		return _saveList[index];
//	}
//
//	public void Save(int index, SaveData data)
//	{
//		_saveList[index] = data;
//		SerializeSaveData(index,data);
//	}
}
