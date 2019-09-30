using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;

	[Header ("Atributes setup")]
	public Wave[] waves;
	public float timeBetweenWaves = 4f;
	private float countdown = 2f;
	private int waveIndex = 0;

	[Header ("Setup")]
	public GameManager gameManager;
	public Text countdownText;

	public Transform startingPoint1;
	public Transform startingPoint2;
	public Targets path1;
	public Targets path2;

	void Start ()
	{
		EnemiesAlive = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		// if there is enemy return
		if (EnemiesAlive > 0) {
			return;
		}

		// finish level
		if (waveIndex == waves.Length) {
			this.enabled = false;
			gameManager.LevelWon ();
			return;
		}

		if (countdown <= 0) {
			StartCoroutine (spawnWave ());
			countdown = timeBetweenWaves;
			return;
		}

		countdown -= Time.deltaTime;
		countdown = Mathf.Clamp (countdown, 0f, Mathf.Infinity);

		countdownText.text = string.Format ("{0:00.00}", countdown);
	}

	/// <summary>
	/// Spawning an wave
	/// </summary>
	IEnumerator spawnWave ()
	{
		PlayerStats.Instance.rounds++;

		Wave wave = waves [waveIndex];
		EnemiesAlive = wave.enemyNumber;

		if (path2 == null) {
			for (int i = 0; i < wave.enemyNumber; i++) {
				spawnEnemy (wave.enemyPref, startingPoint1, path1.destination);
				yield return new WaitForSeconds (1f / wave.ratePerSec);
			}
		} else {
			// wave from two sides
			for (int i = 0; i < wave.enemyNumber; i++) {
				if (i % 2 == 0)
					spawnEnemy (wave.enemyPref, startingPoint2, path2.destination);
				else
					spawnEnemy (wave.enemyPref, startingPoint1, path1.destination);
				yield return new WaitForSeconds (1f / wave.ratePerSec);
			}
		}

		waveIndex++;
	}

	/// <summary>
	/// Spawns the enemy.
	/// </summary>
	/// <param name="enemyPref">Enemy prefab as game object.</param>
	/// <param name="_startPoint">Start point as transform.</param>
	/// <param name="_path">Path that enemy will folow as transform array.</param>
	void spawnEnemy (GameObject enemyPref, Transform _startPoint, Transform[] _path)
	{
		GameObject spawnedEnemy = (GameObject)Instantiate (enemyPref, _startPoint.position, _startPoint.rotation);
		spawnedEnemy.GetComponent<EnemyMovement> ().SetPath (_path);
	}
}
