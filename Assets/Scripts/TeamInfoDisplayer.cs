using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamInfoDisplayer : MonoBehaviour
{
	public static TeamInfoDisplayer Instance
	{
		get {
			if (instance != null) return instance;
			instance = FindObjectOfType<TeamInfoDisplayer>();
			return instance;
		}
	} private static TeamInfoDisplayer instance;

	public TeamInfo teamInfo;
}
