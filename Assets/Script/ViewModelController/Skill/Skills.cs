using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Skills : MonoBehaviour 
{

	public List<SkillData> equippedSkills = new List<SkillData>();
	public List<SkillData> activeSkills{get{return equippedSkills.Where(x=>x.skillType == SkillType.Active && x.skill!=null).ToList();}}
//	public List<SkillData> equippedSkills{ get; private set;}		//TODO private set
	public List<SkillData> unEquippedSkills { get; private set; }

	void OnEnable()
	{
		this.AddObserver (OnEquip,"SkillEquippedNotification",this); //TODO	 UI system must use this object to PostNotification 
		this.AddObserver (OnUnEquip,"SkillUnEquippedNotification",this); 
	}

	void OnDisable()
	{
		this.RemoveObserver (OnEquip,"SkillEquippedNotification",this); //TODO	 UI system must use this object to PostNotification 
		this.RemoveObserver (OnUnEquip,"SkillUnEquippedNotification",this); 
	}

	void OnDestroy()
	{
		this.RemoveObserver (OnEquip,"SkillEquippedNotification",this); //TODO	 UI system must use this object to PostNotification 
		this.RemoveObserver (OnUnEquip,"SkillUnEquippedNotification",this); 
	}

	public void OnEquip(object sender, object args)
	{
		SkillData skill = args as SkillData;

		equippedSkills.Add (skill);

		unEquippedSkills.Remove (skill);

		GameObject instance = CreateSkillInstance (skill);


	}

	public void OnUnEquip(object sender, object args)
	{
		SkillData skill = args as SkillData;
		unEquippedSkills.Add (skill);
		equippedSkills.Remove (skill);		

		GameObject instance = gameObject.transform.FindChild (skill.FullName).gameObject;
		Destroy (instance);

	}

	public void Initailize(List<SkillData> equippedSkills, List<SkillData> unEquippedSkills)
	{
		this.equippedSkills = equippedSkills;
		this.unEquippedSkills = unEquippedSkills;

		for (int i =0; i< this.equippedSkills.Count; i++) 
		{
			CreateSkillInstance(this.equippedSkills[i]);
		}


	}

	GameObject CreateSkillInstance(SkillData skill)
	{
		GameObject skillPrefab = Resources.Load<GameObject>("Skills/"+skill.FullName); //TODO skill prefab has to use the fullname 
		GameObject instance = GameObject.Instantiate(skillPrefab)as GameObject;
		instance.transform.SetParent(gameObject.transform); 			// set skills as parent
		if (skill.skillType == SkillType.Passive) 
		{
			Skill skillComponent = instance.GetComponent<Skill>();
			//skillcomponent.OnEquip()  or  postnotification?
		}

		return instance;
	}
}

