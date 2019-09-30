using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePlatformUI : MonoBehaviour
{

	public GameObject ui;
	public Text cost;

	private Platform platformToBuy;

	/// <summary>
	/// Sets the platform to buy, if platform was selected before, ui is disabled.
	/// </summary>
	/// <param name="platform">Platform that is selected.</param>
	public void SetPlatformToBuy (Platform platform)
	{
		if (platform == platformToBuy) {
			Hide ();
			return;
		}

		platformToBuy = platform;
		transform.position = platformToBuy.GetBuildPosition ();

		cost.text = platformToBuy.platformPrice.ToString ();

		ui.SetActive (true);
	}

	/// <summary>
	/// Purchases the platform.
	/// </summary>
	public void PurchasePlatform ()
	{
		if (PlayerStats.Instance.money >= platformToBuy.platformPrice) {
			platformToBuy.PurchasePlatform ();
			Hide ();
		}
	}

	/// <summary>
	/// Hide UI, and set platform to null.
	/// </summary>
	public void Hide ()
	{
		platformToBuy = null;
		ui.SetActive (false);
	}
}
