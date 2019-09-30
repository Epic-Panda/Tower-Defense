using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	private Transform target;
	private EnemyStats targetEnemy;

	[Header ("Tower setup")]
	public float rotationSpeed = 10;
	public bool xRotation = false;
	public float range = 15f;

	[Header ("Use Bullets (Default)")]
	public GameObject bulletPref;
	public float rateOfFire = 1f;
	private float countdown = 0f;

	[Header ("Use laser")]
	public bool useLaser = false;

	public int damageOverTime = 30;
	public float slowEnemyProcent = 0.5f;

	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light lightImpact;

	[Header ("Setup")]
	public Transform rotationPart;
	public string enemyTag = "Enemy";
	public Transform firePoint;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	/// <summary>
	/// Each 0.5 seconds this function checks for nearest enemy.
	/// </summary>
	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies) {
			float distance = Vector3.Distance (transform.position, enemy.transform.position);

			if (distance < shortestDistance) {
				shortestDistance = distance;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<EnemyStats> ();
		} else
			target = null;
	}

	// Fires at enemy
	void Update ()
	{
		if (countdown >= 0)
			countdown -= Time.deltaTime;
		
		if (target == null) {
			if (useLaser) {
				if (lineRenderer.enabled) {
					lineRenderer.enabled = false;
					impactEffect.Stop ();
					lightImpact.enabled = false;
				}
			}
			return;
		}

		// look at enemy
		LockEnemy ();

		// if using laser fire with laser, otherwise use bullets
		if (useLaser) {
			Laser ();
		} else {
			if (countdown <= 0) {
				Fire ();
				countdown = 1f / rateOfFire;
			}
		}
	}

	/// <summary>
	/// Look at enemy and rotate.
	/// </summary>
	void LockEnemy ()
	{
		Vector3 dir = target.position - new Vector3 (transform.position.x, firePoint.position.y, transform.position.z);
		Quaternion lookAt = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp (rotationPart.rotation, lookAt, rotationSpeed * Time.deltaTime).eulerAngles;
		if (!xRotation)
			rotationPart.rotation = Quaternion.Euler (0, rotation.y, 0f);
		else
			rotationPart.rotation = Quaternion.Euler (rotation.x, rotation.y, 0f);
	}

	/// <summary>
	/// Using line renderer as laser and dealing damage over time.
	/// </summary>
	void Laser ()
	{
		// dealing damage
		targetEnemy.TakeDamage (damageOverTime * Time.deltaTime);
		targetEnemy.Slow (slowEnemyProcent);

		// graphics part
		if (!lineRenderer.enabled) {
			lineRenderer.enabled = true;
			impactEffect.Play ();
			lightImpact.enabled = true;
		}
		
		lineRenderer.SetPosition (0, firePoint.position);
		lineRenderer.SetPosition (1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized * 1.4f;

		impactEffect.transform.rotation = Quaternion.LookRotation (dir);
	}

	/// <summary>
	/// Fires regular bullets.
	/// </summary>
	void Fire ()
	{
		GameObject bulletGo = (GameObject)Instantiate (bulletPref, firePoint.position, firePoint.rotation);
		Bullet bulletCs = bulletGo.GetComponent<Bullet> ();

		if (bulletCs != null)
			bulletCs.ShootTarget (target);
	}

	/// <summary>
	/// When is selected shows range of turret, drawn as gizmos - visible only in editor.
	/// </summary>
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
