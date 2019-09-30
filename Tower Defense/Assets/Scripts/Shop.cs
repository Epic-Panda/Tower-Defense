using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	[Header("just some settings")]
	public TurretBp standardTurret;
	public TurretBp missileTurret;
	public TurretBp laserBeamer;

	public Text standardTurretCost;
	public Text missileTurretCost;
	public Text laserBeamerCost;

	public GameObject aboutStandardTurretUI;
	public GameObject aboutMissileTurretUI;
	public GameObject aboutLaserBeamerUI;

	public Text aboutStandardTurretText;
	public Text aboutMissileTurretText;
	public Text aboutLaserBeamerText;

	TowerBuildManager towerBuildManager;

	void Start ()
	{
		towerBuildManager = TowerBuildManager.instance;

		standardTurretCost.text = standardTurret.cost.ToString ();
		missileTurretCost.text = missileTurret.cost.ToString ();
		laserBeamerCost.text = laserBeamer.cost.ToString ();

		aboutStandardTurretText.text = standardTurret.about;
		aboutMissileTurretText.text = missileTurret.about;
		aboutLaserBeamerText.text = laserBeamer.about;
	}

	public void SelectStandardTurret ()
	{
		towerBuildManager.SelectTurretToBuild (standardTurret);

		aboutStandardTurretUI.SetActive (!aboutStandardTurretUI.activeSelf);
		aboutMissileTurretUI.SetActive (false);
		aboutLaserBeamerUI.SetActive (false);
	}

	public void SelectMissileTurret ()
	{
		towerBuildManager.SelectTurretToBuild (missileTurret);

		aboutMissileTurretUI.SetActive (!aboutMissileTurretUI.activeSelf);
		aboutStandardTurretUI.SetActive (false);
		aboutLaserBeamerUI.SetActive (false);
	}

	public void SelectLaserBeamer ()
	{
		towerBuildManager.SelectTurretToBuild (laserBeamer);

		aboutLaserBeamerUI.SetActive (!aboutLaserBeamerUI.activeSelf);
		aboutMissileTurretUI.SetActive (false);
		aboutStandardTurretUI.SetActive (false);
	}
}
