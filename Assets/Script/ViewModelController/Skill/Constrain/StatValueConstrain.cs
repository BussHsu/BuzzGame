using UnityEngine;
using System.Collections;

public class StatValueConstrain : Constrain
{
	Stats stat {get{return GetComponentInParent<Stats>();}}
	public StatTypes type;
	public CompareOperator op;
	public int value;

	public override bool CanUseSkill ()
	{
		switch(op)
		{
		case CompareOperator.greater:
			return stat[type] > value? true:false; 
		case CompareOperator.equal:
			return stat[type] == value? true:false;
		case CompareOperator.less:
			return stat[type] < value? true:false;
			
		default:
			return false;
		}
	}
}

[System.Flags]
public enum CompareOperator
{
	greater ,
	equal,
	less
}