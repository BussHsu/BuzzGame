using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : StateMachine
{
	public static GameManager instance;
	GameState state;

	[SerializeField] 
	SaveLoadController saveDataController;
		

	void Awake () 
	{
		if(instance == null)
			instance = this;
		else 
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);

//		saveDataController = gameObject.AddChildComponent<SaveLoadController>();
	}

//	void Start()
//	{
//		ChangeState<MainMenuState>();
//	}

}
