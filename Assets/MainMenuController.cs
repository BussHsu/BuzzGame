using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour 
{
	public GameObject startMenuPanel;
	public GameObject loadSaveMenuPanel;
	public ParticleSystem flame;
	public Button startGameButton;
	public Button loadGameButton;

	void OnEnable()
	{
		startGameButton.onClick.AddListener(StartGame);
		loadGameButton.onClick.AddListener(ShowSaveLoadMenu);
	}

	void StartGame()
	{
		//Application.LoadLevel();
	}


	public void ShowStartMenu()
	{
		startMenuPanel.SetActive(true);
		loadSaveMenuPanel.SetActive(false);
	}

	public void ShowSaveLoadMenu()
	{
		startMenuPanel.SetActive(false);
		loadSaveMenuPanel.SetActive(true);
		flame.Pause();
		InitializeSaveDataList();
	}

	void InitializeSaveDataList()
	{

	}
}
