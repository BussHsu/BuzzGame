using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatPanel : MonoBehaviour 
{
	public Panel panel;
	public Sprite allyBackground;
	public Sprite enemyBackground;
	public Image background;
	public Image avatar;
	public Text nameLabel;
	public Text hpLabel;
	public Text mpLabel;
	public Text lvLabel;
	public Image hpFill;
	public Image mpFill;

	public void Display(GameObject obj)
	{
		Unit unit = obj.GetComponent<Unit>();
		background.sprite = (unit.unitAlly == Alliances.Player)?allyBackground:enemyBackground; //may have to add neutral
		//avatar.sprite = null;           // avatar sprite shoud add in the unit
		nameLabel.text = unit.unitName;
		Stats stat = obj.GetComponent<Stats>();
		if(stat)
		{
			int MHP = stat[StatTypes.b_MHP]; int HP = stat[StatTypes.b_HP]; int MMP = stat[StatTypes.b_MSP]; int MP = stat[StatTypes.b_SP];
			hpLabel.text = string.Format("HP: {0}/{1}",HP,MHP);
			mpLabel.text = string.Format("SP: {0}/{1}",MP,MMP);
			lvLabel.text = string.Format("Lv. {0}",stat[StatTypes.Lvl]);
			hpFill.fillAmount = HP/(float) MHP;
			mpFill.fillAmount = MP/(float) MMP;
		}
	}
	 
}
