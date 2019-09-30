using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{

	public Transform[] destination;

	public void Awake ()
	{
		destination = new Transform[transform.childCount];

		for (int i = 0; i < destination.Length; i++) {
			destination [i] = transform.GetChild (i);
		}
	}
}
