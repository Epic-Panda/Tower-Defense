using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBp
{

	[Header ("Standard setup")]
	public GameObject turretPrefab;
	public int cost;
	public int sellCost;
	public string about;

	[Header ("Upgrade setup")]
	public GameObject turretUpgradedPrefab;
	public int upgradeCost;
	public int upgradedSellCost;

	[Header ("Effects")]
	public GameObject buildEffect;
	public GameObject turretUpgradeEffect;
	public GameObject sellEffect;
}
