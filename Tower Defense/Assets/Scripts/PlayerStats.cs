using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;

	[Header ("Starting settings")]
	public int money = 21;
	public int lives = 10;
	[HideInInspector]
	public int maxLives;

	[HideInInspector]
	public int rounds;

	[Header ("Atributes")]
	public Text moneyStatsText;
	public Text lifeStatsText;

	void Start ()
	{
		if (Instance != null) {
			Debug.Log ("error, you can have only one instance of PlayerStats.class");
			return;
		}

		maxLives = lives;

		rounds = 0;
		lifeStatsText.text = "Lives: " + lives;
		moneyStatsText.text = "$ " + money;

		Instance = this;
	}

	/// <summary>
	/// Loses the life and checks if player has some life left.
	/// </summary>
	public void LoseLife ()
	{
		lives--;

		if (lives >= 0)
			lifeStatsText.text = "Lives: " + lives;

		if (lives <= 0)
			GameManager.Instance.GameEnd ();
	}

	/// <summary>
	/// Spends the money.
	/// </summary>
	/// <param name="amount">int amount of money to spent </param>
	public void SpendMoney (int amount)
	{
		money -= amount;
		moneyStatsText.text = "$ " + money;
	}
}
