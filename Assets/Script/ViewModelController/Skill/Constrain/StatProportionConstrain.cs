using UnityEngine;
using System.Collections;

public class StatProportionConstrain : Constrain
{
	Stats stat {get{return GetComponentInParent<Stats>();}}
	public StatTypes type;
	public CompareOperator op;
	public float threshold = 0.2f;
	
	public override bool CanUseSkill ()
	{
		switch(type)
		{
		case StatTypes.b_HP:
		case StatTypes.b_MHP:
			int mhp = stat[StatTypes.b_MHP];
			int hp = stat[StatTypes.b_HP];
			return Condition(hp,(int) (mhp*threshold));
		case StatTypes.b_SP:
		case StatTypes.b_MSP:
			int msp = stat[StatTypes.b_MSP];
			int sp = stat[StatTypes.b_SP];
			return Condition(sp,(int) (msp*threshold));
		default:
			return false;
		}
	}

	bool Condition(int a, int b)
	{
		switch(op)
		{
		case CompareOperator.greater:
			return a > b? true:false; 
		case CompareOperator.equal:
			return a == b? true:false;
		case CompareOperator.less:
			return a < b? true:false;
			
		default:
			return false;
		}


	}
}
