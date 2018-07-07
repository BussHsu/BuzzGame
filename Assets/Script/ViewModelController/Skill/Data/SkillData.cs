using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillData
{

	public string Name;
	public SkillType skillType;			//主動,選擇,被動
	public SkillGenre skillGenre;		//技能分類for UI
	public Sprite sprite;				//FOR技能表格
	public Skill skill;

	public string FullName {get{return skillGenre.ToString()+ Name+ skillType.ToString();}}
	public bool hasUI{get{return (skillType!=SkillType.Passive)?true:false;}}
}
