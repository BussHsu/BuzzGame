using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommandMenuController : MonoBehaviour
{	
	const float degOffset = 60f;

	string EntryPoolKey = "CommandButtons";
	int MenuCount = 6;

	[SerializeField] GameObject entryPrefab;
	[SerializeField] GameObject canvas;
	[SerializeField] Transform buttonCenter;
	List<CommandMenuButton> buttons = new List<CommandMenuButton>(6);
	RectTransform tempRT;

	public int selection{ get; private set;}

	void Awake()
	{
		GameObjectPoolController.AddEntry (EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
	}

	CommandMenuButton Dequeue ()
	{
		Poolable p = GameObjectPoolController.Dequeue (EntryPoolKey);
		CommandMenuButton entry = p.GetComponent<CommandMenuButton>();
		entry.transform.SetParent (buttonCenter, false);
		entry.transform.localScale = Vector3.one;
		entry.GetComponent<RectTransform>().localPosition = Vector3.zero;
		entry.gameObject.SetActive (true);
		entry.Reset ();
		return entry;
	}
	
	void Enqueue (CommandMenuButton entry)
	{
		Poolable p = entry.GetComponent<Poolable>();
		GameObjectPoolController.Enqueue (p);
	}

	void Clear()
	{
		for (int i = buttons.Count - 1; i >= 0; i--)
			Enqueue (buttons [i]);
		
		buttons.Clear ();
	}
	
	void Start()
	{

		canvas.SetActive (false);
	}
	

	
	bool SetSelection(int value)
	{
		if(buttons[value].IsLocked)
			return false;
		
		if (selection >= 0 && selection < buttons.Count)
			buttons [selection].IsSelected = false;
		
		selection = value;
		
		if (selection >= 0 && selection < buttons.Count)
			buttons [selection].IsSelected = true;
		
		return true;
	}

	public void SetLocked(int index, bool value)
	{
		if (index < 0 || index >= buttons.Count)
			return;
		
		buttons [index].IsLocked = value;
		
		if (value && selection == index)
			Next ();
	}
	
	public void Next()
	{
		for(int i = selection + 1; i < selection + buttons.Count; i++)
		{
			int index = i % buttons.Count;
			
			if(SetSelection(index))
				break;
		}
	}
	
	public void Previous()
	{
		for (int i = selection - 1 + buttons.Count; i > selection; i--) 
		{
			int index = i % buttons.Count;
			
			if(SetSelection(index))
				break;
		}
	}

	public void Show(string title, List<ButtonData> options) // string needs to be changed
	{
		canvas.SetActive (true);
		Clear ();

		float degree = -360/options.Count;
		for (int i = 0; i < options.Count; i++) 
		{
			CommandMenuButton button = Dequeue();

			//set selectionNum

			button.selectionNum = i;
			//set eulerAngle
			tempRT = button.GetComponent<RectTransform>();
			tempRT.localEulerAngles = new Vector3(0,0,degOffset+i*degree);
			button.childRT.localEulerAngles = new Vector3(0,0,-degOffset-i*degree);

			button.Setup(options[i]);
			buttons.Add(button);
		}
		SetSelection (0);
		TogglePos (true);
	}
	

	
	public void Hide()
	{
		TogglePos (false);


	}




	void TogglePos (bool show)
	{
		
		for(int i=0;i<buttons.Count;i++)
		{
			
			CommandMenuButton button = buttons[i];
			//enable anim
			button.gameObject.SetActive(true);
			button.anim.SetBool("show",show);
		}
	}


}

public class ButtonData
{
	public string title;
	public Sprite sprite;

	public ButtonData(string t, Sprite s)
	{
		title = t;
		sprite =s;
	}
}
