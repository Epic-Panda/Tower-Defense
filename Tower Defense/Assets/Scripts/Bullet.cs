using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	private Transform target;

	public GameObject bulletImpactEffect;
	public float speed = 70f;
	public float explosionRadius = 0;
	public int directHitDamage = 35;
	public int explosionDamage = 10;

	/// <summary>
	/// Set target to shoot at	
	/// <param name="_target">Target.</param>
    /// </summary>
	public void ShootTarget (Transform _target)
	{
		target = _target;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null) {
			Destroy (gameObject);
			return;
		}
			
		Vector3 dir = target.position - transform.position;
		float speedPerFrame = speed * Time.deltaTime;

		// if bullet is close to target so next frame it is in front of target, target got hit
		if (dir.magnitude <= speedPerFrame) {
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * speedPerFrame, Space.World);
		transform.LookAt (target);
	}

	/// <summary>
	/// Bullet is hitting target. Bullet deals direct hit damage + explosion damage to all enemy in range and destroy itself
	/// </summary>
	void HitTarget ()
	{
		GameObject impactEffect = (GameObject)Instantiate (bulletImpactEffect, transform.position, transform.rotation);
		Destroy (impactEffect, 5f);

		DealDamage (target, directHitDamage);
		if (explosionRadius > 0) {
			Explode ();
		}

		Destroy (gameObject);
	}

	/// <summary>
	/// On explode deal damage to enemies in range
	/// </summary>
	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider collider in colliders) {
			if (collider.tag == "Enemy")
				DealDamage (collider.transform, explosionDamage);
		}
	}

	/// <summary>
	/// Deal damage to enemy
	/// </summary>
	/// <param name="enemy">Transform enemy.</param>
	/// <param name="damageToDeal">An int amount of damage.</param>
	void DealDamage (Transform enemy, int damageToDeal)
	{
		EnemyStats enemyStats = enemy.GetComponent<EnemyStats> ();

		if (enemyStats != null)
			enemyStats.TakeDamage (damageToDeal);
	}

	/// <summary>
	/// Shows range of explosion radius on select - for unity editor only
	/// </summary>
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}
