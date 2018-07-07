using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour
{
	public static readonly StatTypes[] statOrder = new StatTypes[] 
	{
		StatTypes.b_MHP,
		StatTypes.b_MSP,
		StatTypes.b_STR,
		StatTypes.b_CON,
		StatTypes.b_AGI,
		StatTypes.b_INT,
		StatTypes.b_WIS,
		StatTypes.b_LUK
	 };

	public int[] baseStats = new int[statOrder.Length];
	public float[] growStats = new float[statOrder.Length];
	Stats _stats;

	void OnDestroy()
	{
		this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.Lvl), _stats);
	}

	public void Employ() 
	{
		_stats = gameObject.GetComponentInParent<Stats>();
		this.AddObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.Lvl), _stats);

		Feature[] features = GetComponentsInChildren<Feature>();
		for (int i = 0; i < features.Length; i++)
			features[i].Activate(gameObject);
	 }

	public void UnEmploy() 
	{
		Feature[] features = GetComponentsInChildren<Feature>();
		for (int i = 0; i < features.Length; i++)
			features[i].Deactivate();

		this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.Lvl), _stats);
		_stats = null;
	 }

	public void LoadDefaultStats() 
	{
		for (int i = 0; i < statOrder.Length; i++) 
		{
			StatTypes type = statOrder[i];
			_stats.SetValue(type, baseStats[i], false);
		 }

		_stats.SetValue(StatTypes.b_HP, _stats[StatTypes.b_MHP], false);
		_stats.SetValue(StatTypes.b_SP, _stats[StatTypes.b_MSP], false);
	}

	protected virtual void OnLvlChangeNotification(object sender, object args) 
	{
		int oldValue = (int)args;
		int newValue = _stats[StatTypes.Lvl];

		for (int i = oldValue; i < newValue; i++)
			LevelUp();
	 }

	void LevelUp() 
	{
		for (int i = 0; i < statOrder.Length; i++) 
		{
			StatTypes type = statOrder[i];
			int whole = Mathf.FloorToInt(growStats[i]);
			float fraction = growStats[i] - whole;

			int value = _stats[type];
			value += whole;
			if (UnityEngine.Random.value > (1f - fraction))
				value++;

			_stats.SetValue(type, value, false);
		 }

		_stats.SetValue(StatTypes.b_HP, _stats[StatTypes.b_MHP], false);
		_stats.SetValue(StatTypes.b_SP, _stats[StatTypes.b_MSP], false);
	}
}