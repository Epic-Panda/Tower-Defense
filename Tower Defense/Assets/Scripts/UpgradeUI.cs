using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{

	public GameObject ui;
	public Text upgradeCostText;
	public Button upgradeBtn;

	public Text sellCostText;

	private Platform targetedPlatform;

	/// <summary>
	/// Sets the target platform.
	/// </summary>
	/// <param name="p">Platform with turret.</param>
	public void setTargetPlatform (Platform p)
	{
		targetedPlatform = p;

		transform.position = targetedPlatform.GetBuildPosition ();

		if (!targetedPlatform.isUpgraded) {
			upgradeCostText.text = "$ " + p.turretBp.upgradeCost;
			sellCostText.text = "$ " + p.turretBp.sellCost;
			upgradeBtn.interactable = true;
		} else {
			upgradeCostText.text = "Max";
			sellCostText.text = "$ " + p.turretBp.upgradedSellCost;
			upgradeBtn.interactable = false;
		}

		ui.SetActive (true);
	}

	/// <summary>
	/// Hide upgrade UI.
	/// </summary>
	public void Hide ()
	{
		ui.SetActive (false);
	}

	/// <summary>
	/// Call upgrade method targeted platform and then deselect it
	/// </summary>
	public void Upgrade ()
	{
		targetedPlatform.UpgradeTurret ();
		TowerBuildManager.instance.DeselectPlatform ();
	}

	/// <summary>
	/// Sell turret on that platform and deselect platform
	/// </summary>
	public void Sell ()
	{
		targetedPlatform.SellTurret ();
		TowerBuildManager.instance.DeselectPlatform ();
	}
}
