using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(EnemyStats))]
public class EnemyMovement : MonoBehaviour
{

	private Transform destination;
	private int destinationIndex = 0;

	private EnemyStats enemyStats;

	private Transform[] path;

	// Use this for initialization
	void Start ()
	{
		enemyStats = GetComponent<EnemyStats> ();
		destination = path [destinationIndex++];
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 dir = destination.position - transform.position;
		transform.Translate (dir.normalized * enemyStats.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, destination.position) <= 0.5f) {
			GetNextDestination ();
		}

		enemyStats.speed = enemyStats.startSpeed;
	}

	/// <summary>
	/// Sets the path for enemy to follow.
	/// </summary>
	/// <param name="_path">Setting a list of transform as path that enemy will follow.</param>
	public void SetPath (Transform[] _path)
	{
		path = _path;
	}

	/// <summary>
	/// Finding next destination.
	/// </summary>
	void GetNextDestination ()
	{
		if (destinationIndex >= path.Length) {
			ReachedLastDestination ();
			return;
		}
		destination = path [destinationIndex++];
	}

	/// <summary>
	/// In case last destionation is reached, reduce players life and destroy object.
	/// </summary>
	void ReachedLastDestination ()
	{
		PlayerStats.Instance.LoseLife ();
		EnemySpawner.EnemiesAlive--;
		Destroy (gameObject);
	}
}
