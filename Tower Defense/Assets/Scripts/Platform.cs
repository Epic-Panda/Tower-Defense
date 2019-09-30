using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
	public bool isEnabled = true;
	public int platformPrice = 5;
	public Color disabledColor = Color.red;
	public Color purchasedColor = Color.white;
	public PurchasePlatformUI purchasePlatformUI;

	public Color hoverColor = Color.green;
	public Color lowMoneyColor = Color.red;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBp turretBp;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	private TowerBuildManager towerBildManager;

	void Start ()
	{
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;

		towerBildManager = TowerBuildManager.instance;
	}

	/// <summary>
	/// Gets position where to build turret.
	/// </summary>
	/// <returns>The build position.</returns>
	public Vector3 GetBuildPosition ()
	{
		return transform.position + Vector3.up * transform.localScale.y / 2;
	}

	/// <summary>
	/// Builds the turret.
	/// </summary>
	/// <param name="_turretBp">Turret's blueprint.</param>
	void BuildTurret (TurretBp _turretBp)
	{
		if (!towerBildManager.HasMoneyToBuild) {
			return;
		}

		PlayerStats.Instance.SpendMoney (_turretBp.cost);

		GameObject _turret = (GameObject)Instantiate (_turretBp.turretPrefab, GetBuildPosition (), Quaternion.identity);
		turret = _turret;
		turretBp = _turretBp;

		GameObject buildEffect = (GameObject)Instantiate (_turretBp.buildEffect, GetBuildPosition (), Quaternion.identity);
		Destroy (buildEffect, 5f);
	}

	/// <summary>
	/// Upgrades the turret.
	/// </summary>
	public void UpgradeTurret ()
	{
		if (PlayerStats.Instance.money < turretBp.upgradeCost) {
			return;
		}

		PlayerStats.Instance.SpendMoney (turretBp.upgradeCost);

		// destroy previous turret
		Destroy (turret);

		// build upgraded version
		GameObject _turret = (GameObject)Instantiate (turretBp.turretUpgradedPrefab, GetBuildPosition (), Quaternion.identity);
		turret = _turret;

		GameObject upgradeEffect = (GameObject)Instantiate (turretBp.turretUpgradeEffect, GetBuildPosition (), Quaternion.identity);
		Destroy (upgradeEffect, 5f);

		isUpgraded = true;
	}

	/// <summary>
	/// Sells the turret.
	/// </summary>
	public void SellTurret ()
	{
		if (!isUpgraded) {
			PlayerStats.Instance.SpendMoney (-turretBp.sellCost);
		} else {
			PlayerStats.Instance.SpendMoney (-turretBp.upgradedSellCost);
			isUpgraded = false;
		}

		GameObject sellEffect = (GameObject)Instantiate (turretBp.sellEffect, GetBuildPosition (), Quaternion.identity);
		Destroy (sellEffect, 5f);

		Destroy (turret);
	}

	/// <summary>
	/// Purchases the platform.
	/// </summary>
	public void PurchasePlatform ()
	{
		PlayerStats.Instance.SpendMoney (platformPrice);
		rend.material.color = purchasedColor;
		startColor = purchasedColor;
		isEnabled = true;
	}

	/// <summary>
	/// On mouse down checking if building is possible and calling function to build turret.
	/// </summary>
	void OnMouseDown ()
	{		
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		// if platform is not purchased show purchase UI
		if (!isEnabled && purchasePlatformUI != null) {
			purchasePlatformUI.SetPlatformToBuy (this);
			// hide upgrade UI
			towerBildManager.upgradeUI.Hide ();
			return;
		}

		if (turret != null) {
			towerBildManager.SelectPlatform (this);
			if (purchasePlatformUI != null)
				purchasePlatformUI.Hide ();
			return;
		}

		if (!towerBildManager.CanBuild)
			return;
			
		// build a turret on node
		//towerBildManager.BuildTurret(this);
		BuildTurret (towerBildManager.GetTurretToBuild ());
	}

	/// <summary>
	/// If some turret is selected selected platform changes color.
	/// </summary>
	void OnMouseEnter ()
	{		
		if (EventSystem.current.IsPointerOverGameObject ())
			return;
		
		if (turret != null)
			return;

		if (!towerBildManager.CanBuild)
			return;

		if (!isEnabled)
			rend.material.color = disabledColor;
		
		if (towerBildManager.HasMoneyToBuild)
			rend.material.color = hoverColor;
		else
			rend.material.color = lowMoneyColor;
	}

	/// <summary>
	/// On mouse exit collor of platform is reset.
	/// </summary>
	void OnMouseExit ()
	{
		rend.material.color = startColor;
	}
}
