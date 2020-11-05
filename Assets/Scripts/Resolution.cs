using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resolution : MonoBehaviour
{
	[Header("Build Resolution Size")]
	public Vector2 resolution;

	private void Start()
	{
		Screen.SetResolution((int)resolution.x, (int)resolution.y, false);
	}
}
