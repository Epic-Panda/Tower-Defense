using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance;

	[Header ("GameObjects")]
	public GameObject gameEndUI;
	public GameObject pauseMeniUI;
	public GameObject levelWonUI;
	public GameObject timeUI;
	public GameObject uis;

	[Header ("Scipts")]
	public FadingController fadingController;

	[Header ("Atributes")]
	public string menuSceneName = "MainMenu";
	public string nextLevelName = "Level_2";
	public int currentLevel = 1;

	// currently only stops camera movements and blocks player to pause game
	public bool gameOver = false;

	void Start ()
	{
		if (Instance != null) {
			Debug.Log ("error, you can have only one instance of GameManager.class");
			return;
		}

		Instance = this;
	}

	void Update ()
	{
		if (gameOver) {
			timeUI.SetActive (false);
			return;
		}
		
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown ("p")) {
			Pause ();
		}
	}

	/// <summary>
	/// Pauses game and activating pause menu UI
	/// </summary>
	public void Pause ()
	{
		pauseMeniUI.SetActive (!pauseMeniUI.activeSelf);
		timeUI.SetActive (!pauseMeniUI.activeSelf);

		if (pauseMeniUI.activeSelf) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	/// <summary>
	/// Finishing game and activating gameover UI
	/// </summary>
	public void GameEnd ()
	{
		gameOver = true;
		gameEndUI.SetActive (true);
	}

	/// <summary>
	/// Finishing level and activating levelwon UI
	/// </summary>
	public void LevelWon ()
	{
		if (PlayerPrefs.GetFloat ("level_" + currentLevel + "_done", 0) < (float)PlayerStats.Instance.lives / PlayerStats.Instance.maxLives)
			PlayerPrefs.SetFloat ("level_" + currentLevel + "_done", (float)PlayerStats.Instance.lives / PlayerStats.Instance.maxLives);

		if (PlayerPrefs.GetInt ("reachedLevel", 1) < currentLevel + 1)
			PlayerPrefs.SetInt ("reachedLevel", currentLevel + 1);
		
		gameOver = true;
		levelWonUI.SetActive (true);
	}

	// hide UIes
	void HideUI ()
	{
		if (gameEndUI.activeSelf)
			gameEndUI.SetActive (false);
		else if (levelWonUI.activeSelf)
			levelWonUI.SetActive (false);
		else if (pauseMeniUI.activeSelf)
			pauseMeniUI.SetActive (false);

		uis.SetActive (false);
	}

	// button functions

	/// <summary>
	/// Retry this leve.
	/// </summary>
	public void Retry ()
	{
		if (Time.timeScale != 1f)
			Time.timeScale = 1f;
		
		HideUI ();
		fadingController.FadeToScene (SceneManager.GetActiveScene ().name);
	}

	/// <summary>
	/// Go to menu.
	/// </summary>
	public void Menu ()
	{
		if (Time.timeScale != 1f)
			Time.timeScale = 1f;
		
		HideUI ();
		fadingController.FadeToScene (menuSceneName);
	}

	/// <summary>
	/// Go to next level.
	/// </summary>
	public void NextLevel ()
	{
		HideUI ();
		fadingController.FadeToScene (nextLevelName);
	}
}
