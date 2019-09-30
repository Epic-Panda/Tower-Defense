using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
	[Header ("Atributes setup")]
	public float startSpeed = 10f;

	[HideInInspector]
	public float speed;

	public float maxHealth = 100f;
	[HideInInspector]
	public float health;

	public int money = 2;

	[Header ("Object setup")]
	public GameObject deathEffect;
	private bool alive = true;

	public Image healthBar;

	void Start ()
	{
		speed = startSpeed;
		health = maxHealth;
	}

	/// <summary>
	/// Deals damage to enemy.
	/// </summary>
	/// <param name="amount">An float amount of damage to deal.</param>
	public void TakeDamage (float amount)
	{
		if (!alive)
			return;
		
		health -= amount;

		healthBar.fillAmount = health / maxHealth;

		if (health <= 0)
			Destruction ();
	}

	/// <summary>
	/// Slow the enemy by procent.
	/// </summary>
	/// <param name="procentToSlow">Procent to slow as float.</param>
	public void Slow (float procentToSlow)
	{
		speed = startSpeed * (1f - procentToSlow);
	}

	/// <summary>
	/// Destruction of an enemy.
	/// </summary>
	void Destruction ()
	{
		alive = false;

		EnemySpawner.EnemiesAlive--;
		PlayerStats.Instance.SpendMoney (-money);

		GameObject deathEffectGo = (GameObject)Instantiate (deathEffect, transform.position, transform.rotation);
		Destroy (deathEffectGo, 5f);
		Destroy (gameObject);
	}
}