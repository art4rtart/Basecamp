using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoDisplayer : MonoBehaviour
{
	public static UserInfoDisplayer Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<UserInfoDisplayer>();
			return instance;
		}
	}
	private static UserInfoDisplayer instance;

	public UserInfo userInfo;
}
