using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommandMenuButton : MonoBehaviour 
{

	[System.Flags]
	enum States 
	{
		None = 0,//00
		Selected = 1 << 0, 
		Locked = 1 << 1 
	}
		
	[SerializeField] Image lockImage;
	public Sprite sprite{set{button.GetComponent<Image>().sprite = value;}}
	[SerializeField] Text label;
	[SerializeField] Button button;
	Outline outline;
	RectTransform rt;


	public int selectionNum;

	public Animator anim
	{
		get{return GetComponentInChildren<Animator>();}
	}

	public string Title
	{
		get {return label.text;}
		set {label.text = value;}
	}
		
	public bool IsLocked
	{
		get{return (State & States.Locked) != States.None;}
		set
		{
			if(value)
				State |= States.Locked;
			else
				State &= ~States.Locked;
				
		}
	}
		
	public bool IsSelected
	{
		get{return (State & States.Selected) != States.None;}
		set
		{
			if(value)
				State |= States.Selected;
			else
				State &= ~States.Selected;
		}
	}

	public RectTransform childRT
	{
		get{return button.GetComponent<RectTransform>();}
	}

	States State
	{
		get{return _state;}
		set
		{
			if(_state == value)
				return;
				
			_state = value;
				
			if(IsLocked)
			{
				lockImage.enabled = true;
				label.color = Color.gray;
				outline.effectColor = new Color32(20, 36, 44, 255);
			}
			else if(IsSelected)
			{
				lockImage.enabled = false;
				label.color =  Color.red;
				outline.effectColor = Color.red;
			}
			else
			{
				lockImage.enabled = false;
				label.color = Color.blue;
				outline.effectColor = new Color32(20, 36, 44, 255);
			}
		}
	}
	States _state;
		
	void Awake()
	{
		outline = GetComponentInChildren<Outline>();
	}

	public void Setup(ButtonData data)
	{
		this.Title = data.title;
		this.sprite = data.sprite;
	}

	public void Reset()
	{
		State = States.None;
	}
}