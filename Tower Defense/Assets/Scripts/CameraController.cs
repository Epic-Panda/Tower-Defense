using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public float panSpeed = 30f;
	public float panBorder = 10f;

	public float scrollSpeed = 5f;

	[Header ("Min and max movement value")]
	public Vector2 minMaxX = new Vector2 (0f, 35f);
	public Vector2 minMaxY = new Vector2 (25f, 52f);
	public Vector2 minMaxZ = new Vector2 (-75f, 0f);

	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.gameOver)
			return;
		
		if (Input.GetKey ("w") || Input.mousePosition.y >= Screen.height - panBorder) {
			transform.Translate (Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("s") || Input.mousePosition.y <= panBorder) {
			transform.Translate (Vector3.forward * -panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("a") || Input.mousePosition.x <= panBorder) {
			transform.Translate (Vector3.right * -panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey ("d") || Input.mousePosition.x >= Screen.width - panBorder) {
			transform.Translate (Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis ("Mouse ScrollWheel");

		Vector3 pos = transform.position;
		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, minMaxX.x, minMaxX.y);
		pos.y = Mathf.Clamp (pos.y, minMaxY.x, minMaxY.y);
		pos.z = Mathf.Clamp (pos.z, minMaxZ.x, minMaxZ.y);

		transform.position = pos;
	}
}