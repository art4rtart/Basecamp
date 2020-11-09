using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
	public static UserManager Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<UserManager>();
			return instance;
		}
	}
	private static UserManager instance;

	[Header("Default")]
	public string name;
	public int age;
	public string position;
	public string department;

	public bool hasApplyed;
}
