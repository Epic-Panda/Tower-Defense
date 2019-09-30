using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadingController : MonoBehaviour
{

	public Image imageToFade;
	public AnimationCurve animCurve;

	void Start ()
	{
		StartCoroutine (FadeIn ());
	}

	/// <summary>
	/// Go to scene with some cool fade out
	/// </summary>
	/// <param name="scene">String scene name.</param>
	public void FadeToScene (string scene)
	{
		StartCoroutine (FadeOut (scene));
	}

	/// <summary>
	/// Fades the in.
	/// </summary>
	IEnumerator FadeIn ()
	{
		float t = 1f;

		while (t > 0f) {
			t -= Time.deltaTime * .5f;

			float alpha = animCurve.Evaluate (t);

			imageToFade.color = new Color (0f, 0f, 0f, alpha);
			yield return 0;
		}
	}

	/// <summary>
	/// Fades the out.
	/// </summary>
	/// <param name="scene">String scene name.</param>
	IEnumerator FadeOut (string scene)
	{
		float t = 0f;

		while (t < 1f) {
			t += Time.deltaTime * .5f;

			float alpha = animCurve.Evaluate (t);

			imageToFade.color = new Color (0f, 0f, 0f, alpha);
			yield return 0;
		}

		SceneManager.LoadScene (scene);
	}
}
