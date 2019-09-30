using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{

	public static TowerBuildManager instance;

	public UpgradeUI upgradeUI;

	private TurretBp turretToBuild;
	private Platform selectedPlatform;

	void Awake ()
	{
		if (instance != null) {
			Debug.Log ("Can have only one tower build manager");
			return;
		}
		instance = this;
	}

	/// <summary>
	/// Does player can build this turret.
	/// </summary>
	/// <value><c>true</c> if this turret can build; otherwise, <c>false</c>.</value>
	public bool CanBuild { get { return turretToBuild != null; } }

	/// <summary>
	/// Does player have enough money to build.
	/// </summary>
	/// <value><c>true</c> if player have has enough money; otherwise, <c>false</c>.</value>
	public bool HasMoneyToBuild{ get { return PlayerStats.Instance.money >= turretToBuild.cost; } }

	/// <summary>
	/// Returns blueprint of turret that will be created.
	/// </summary>
	/// <returns>The blueprint of turret to build.</returns>
	public TurretBp GetTurretToBuild ()
	{
		return turretToBuild;
	}

	/// <summary>
	/// Selects the platform.
	/// </summary>
	/// <param name="platform">Selected platform.</param>
	public void SelectPlatform (Platform platform)
	{
		if (selectedPlatform == platform) {
			DeselectPlatform ();
			return;
		}

		selectedPlatform = platform;
		turretToBuild = null;

		upgradeUI.setTargetPlatform (selectedPlatform);
	}

	/// <summary>
	/// Deselects the platform.
	/// </summary>
	public void DeselectPlatform ()
	{
		selectedPlatform = null;
		upgradeUI.Hide ();
	}

	/// <summary>
	/// Sets selected turret blueprint.
	/// </summary>
	/// <param name="turret">Turret blueprint.</param>
	public void SelectTurretToBuild (TurretBp turret)
	{
		turretToBuild = turret;
		selectedPlatform = null;
		upgradeUI.Hide ();
	}
}