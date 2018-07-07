using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveDataContainer : ListDataContainer<SaveData>
{
	public SaveLoadController owner{get{return GetComponentInParent<SaveLoadController>();}}

	public Text slotNumLabel;
	public Text stageNameLabel;
	public Text dateTimeLabel;
	public bool isSelected;

	string SAVEFILE_FOLDER;
	const string BLANK="Blank";

	void OnEnable()
	{
		slotNumLabel.text=string.Format("Save No. {0}",order.ToString("00"));

	}

	void Awake()
	{
		SAVEFILE_FOLDER = Application.persistentDataPath+"/SaveData";
		LoadAndDisplayData();
	}

	public override void LoadAndDisplayData ()
	{
		LoadSaveData();
		if(data==null)
		{
			stageNameLabel.text = BLANK;
			dateTimeLabel.text = BLANK;
		}

		else
		{
			stageNameLabel.text = data.stageInfo;
			dateTimeLabel.text = data.time.ToShortTimeString();
		}
	}

	void LoadSaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		data = null;

		if(File.Exists(string.Format("{0}/save{1}.dat",SAVEFILE_FOLDER,order)))
		{
			FileStream  file = File.Open(string.Format("{0}/save{1}.dat",SAVEFILE_FOLDER,order),FileMode.Open);
			data = (SaveData) bf.Deserialize(file);
			file.Close();
		}
	}

	void OnClick()
	{
		isSelected = true;
	}
}
