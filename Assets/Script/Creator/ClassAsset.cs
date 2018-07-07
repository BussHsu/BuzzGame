using UnityEngine;
using UnityEditor;

public class ClassAsset
{
	[MenuItem("Assets/Create/Conversation Data")]
	public static void CreateConversationData()
	{
		ScriptableObjectUtility.CreateAsset<ConversationData> ();
	}

	[MenuItem("Assets/Create/TileType Data")]
	public static void CreateTileTypeData()
	{
		ScriptableObjectUtility.CreateAsset<TileTypeData> ();
	}


}