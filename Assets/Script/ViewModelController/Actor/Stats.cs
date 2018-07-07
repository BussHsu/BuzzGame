using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour 
{
	[SerializeField]
	int[] _data = new int[(int)StatTypes.Count];

	public static string WillChangeNotification(StatTypes type)
	{
		if (!_willChangeNotifications.ContainsKey (type))
			_willChangeNotifications.Add (type, string.Format ("Stats.{0}WillChange", type.ToString ()));

		return _willChangeNotifications [type];
	}
	static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string> ();

	public static string DidChangeNotification (StatTypes type)
	{
		if (!_didChangeNotifications.ContainsKey (type))
			_didChangeNotifications.Add (type, string.Format ("Stats.{0}DidChange", type.ToString ()));

		return _didChangeNotifications [type];
	}
	static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string> ();

	public int this[StatTypes s]
	{
		get{return _data[(int)s];}
		set{SetValue(s, value, true);}
	}



	public void SetValue(StatTypes type, int value, bool allowExceptions)
	{
		int oldValue = this [type];

		if (oldValue == value)
			return;

		if (allowExceptions) 
		{
			ValueChangeException exc = new ValueChangeException(oldValue, value);

			this.PostNotification(WillChangeNotification(type), exc);

			value = Mathf.FloorToInt(exc.GetModifiedValue());

			if(exc.toggle == false || value == oldValue)
				return;
		}

		_data [(int)type] = value;
		this.PostNotification(DidChangeNotification(type), oldValue);
	} 
}