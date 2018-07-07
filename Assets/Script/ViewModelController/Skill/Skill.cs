using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill : MonoBehaviour
{

	public SkillType type;                                                                                                                                                                                                                                                                                                                    
	public string skillName;
	public int costSP;
	public Constrain[] constrains;

	public int phaseNum;
	public GameObject[] phasers;

	public GameObject owner{get{return unit.gameObject;}}
	public Point origin {get{return owner.GetComponent<Unit>().pos;}}
	public Unit unit{get{return GetComponentInParent<Unit>();}}
	public Stats stats{get{return owner.GetComponent<Stats>();}}

	void Start()
	{
		Init();
	}

	public bool CanUse
	{
		get{
			for (int i =0; i<constrains.Length;i++)
			{
				if(!constrains[i].CanUseSkill())
				return false;
			}
			return true;
		}
	}

	void Init()
	{
		phasers = new GameObject[phaseNum];
		for(int i=0;i<phaseNum;i++)
		{
			phasers[i] = transform.Find("Phase"+i.ToString()).gameObject;
		}

		if (constrains == null)
		{
			constrains = GetComponentsInChildren<Constrain>();
			if(constrains.Length<=0)
			{
				StatValueConstrain svc = gameObject.AddComponent<StatValueConstrain>();
				svc.op = CompareOperator.greater| CompareOperator.equal;
				svc.value = costSP;
				svc.type = StatTypes.b_SP;
			}
		}
	}


}