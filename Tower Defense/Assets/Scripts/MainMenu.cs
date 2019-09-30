using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string levelToLoad = "LevelSelectMenu";

	public FadingController fadingController;

	/// <summary>
	/// Play game - go to selection menu.
	/// </summary>
	public void Play ()
	{
		fadingController.FadeToScene (levelToLoad);
	}

	/// <summary>
	/// Quit game.
	/// </summary>
	public void Quit ()
	{
		Application.Quit ();
	}
}
