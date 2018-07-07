using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour 
{
	public const int minLevel = 1;
	public const int maxLevel = 20;
	public const int maxExperience = 1900;
	//給最高等級和最低等級跟最多經驗值，下面的程式會自動計算出每個等級的所需經驗值

	public int LVL
	{
		get {return _stats[StatTypes.Lvl];}
	}

	public int EXP
	{
		get{return _stats[StatTypes.exp];}
		set{_stats[StatTypes.exp] = value;}
	}

	public float LevelPercent
	{
		get{return (float)(LVL - minLevel) / (float)(maxLevel - minLevel);}
	}

	Stats _stats;

	void Awake()
	{
		_stats = GetComponent<Stats> ();
	}

	void OnEnable()
	{
		this.AddObserver (OnExpWillChange, Stats.WillChangeNotification (StatTypes.exp), _stats);
		this.AddObserver (OnExpDidChange, Stats.DidChangeNotification (StatTypes.exp), _stats);
	}

	void OnDisable()
	{
		this.RemoveObserver (OnExpWillChange, Stats.WillChangeNotification (StatTypes.exp), _stats);
		this.RemoveObserver (OnExpDidChange, Stats.DidChangeNotification (StatTypes.exp), _stats);
	}

	void OnExpWillChange(object sender, object args)
	{
		ValueChangeException vce = args as ValueChangeException;
		vce.AddModifier (new ClampValueModifier (int.MaxValue, EXP, maxExperience));
	}

	void OnExpDidChange(object sender, object args)
	{
		_stats.SetValue (StatTypes.Lvl, LevelForExperience (EXP), false);
	}

	public static int ExperienceForLevel(int level)
	{
		float levelPercent = Mathf.Clamp01 ((float)(level - minLevel) / (float)(maxLevel - minLevel));

		return (int)EasingEquations.Linear (0, maxExperience, levelPercent);
		//利用函數來控制升級所需經驗值，我在這裡用線性
	}

	public static int LevelForExperience(int exp)
	{
		int lvl = maxLevel;
		for (; lvl >= minLevel; lvl--)//原來for迴圈可以這樣用...
			if (exp >= ExperienceForLevel (lvl))
				break;

		return lvl;
	}

	public void Init (int level)
	{
		_stats.SetValue (StatTypes.Lvl, level, false);
		_stats.SetValue (StatTypes.exp, ExperienceForLevel (level), false);
	}
}