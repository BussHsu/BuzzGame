using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

public class SettingsAutoConverter : AssetPostprocessor
{
	static Dictionary<string, Action> parsers;

	static SettingsAutoConverter()
	{
		parsers = new Dictionary<string, Action>();
		parsers.Add("Enemies.csv", ParseEnemies);
	}

	static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		for (int i = 0; i < importedAssets.Length; i++)
		{
			string fileName = Path.GetFileName(importedAssets[i]);
			if (parsers.ContainsKey(fileName))
				parsers[fileName]();
		}
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	static void ParseEnemies()
	{
		string filePath = Application.dataPath + "/Settings/Enemies.csv";
		if (!File.Exists(filePath))
		{
			Debug.LogError("Missing Enemies Data: " + filePath);
			return;
		}

		string[] readText = File.ReadAllLines("Assets/Settings/Enemies.csv");
		filePath = "Assets/Resources/EnemyData/";
		for (int i = 1; i < readText.Length; ++i)
		{
			EnemyData enemyData = ScriptableObject.CreateInstance<EnemyData>();
			enemyData.Load(readText[i]);
			string fileName = string.Format("{0}{1}.asset", filePath, enemyData.moster_name);
			AssetDatabase.CreateAsset(enemyData, fileName);
		}
	}
}