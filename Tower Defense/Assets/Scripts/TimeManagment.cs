using UnityEngine;
using UnityEngine.UI;

public class TimeManagment : MonoBehaviour
{
	/// <summary>
	/// Set time to regular.
	/// </summary>
	public void Play ()
	{
		if (Time.timeScale != 1f)
			Time.timeScale = 1f;
	}

	/// <summary>
	/// Stop time.
	/// </summary>
	public void Stop ()
	{
		if (Time.timeScale != 0f)
			Time.timeScale = 0f;
	}
}
