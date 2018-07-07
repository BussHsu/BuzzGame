using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine
{
	public static BattleController instance;
	public string StateName{get{return _currentState.ToString();}}
	public GameObject heroPrefab;
	//public GameObject foePrefab;

	public CameraRig cameraRig;
	public Board board;
	public Map level;
	public Transform Cursor;
	public Point pos;

	public FacingController faceind;

	public Tile currentTile{get{return board.GetTile(pos);}}
//	public AbilityMenuPanelController abilityMeunPanelController;
	public CommandMenuController commandButtonController;
	public AttackSuccessIndicator attackSuccessIndicator;
	public StatPanelController statPanelController;
	[SerializeField]
	public Turn turn = new Turn();

	public Unit currentUnit;
	public List<Unit> UnitList = new List<Unit> ();

	public IEnumerator round;

	void Start()
	{
		instance = this;
		ChangeState<InitBattleState>();
	}
}