using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

	public GameObject levelSelectionUI;
	public FadingController fadingController;

	public Button[] levelButtons;

	public Image[] butonImage;

	void Start ()
	{
		int reachedLevel = PlayerPrefs.GetInt ("reachedLevel", 1);

		for (int i = 0; i < reachedLevel; i++) {
			if (i == levelButtons.Length)
				break;
			levelButtons [i].interactable = true;
			butonImage [i].fillAmount = PlayerPrefs.GetFloat ("level_" + (i + 1) + "_done", 0);
		}
	}

	/// <summary>
	/// Select the specified levelName.
	/// </summary>
	/// <param name="levelName">Level name.</param>
	public void Select (string levelName)
	{
		levelSelectionUI.SetActive (false);
		fadingController.FadeToScene (levelName);
	}
}
