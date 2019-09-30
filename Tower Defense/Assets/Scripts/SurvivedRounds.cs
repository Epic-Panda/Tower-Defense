using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivedRounds : MonoBehaviour
{

	public Text roundsText;

	void OnEnable ()
	{
		StartCoroutine (AnimateText ());
	}

	/// <summary>
	/// Start text animation 0 to number of survived rounds.
	/// </summary>
	/// <returns>The text.</returns>
	IEnumerator AnimateText ()
	{
		roundsText.text = "0";
		int round = 0;

		yield return new WaitForSeconds (0.7f);

		while (round < PlayerStats.Instance.rounds) {
			round++;
			roundsText.text = round.ToString ();

			yield return new WaitForSeconds (0.05f);
		}
	}
}
