using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillOutUserInfo : MonoBehaviour
{
	public static FillOutUserInfo Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<FillOutUserInfo>();
			return instance;
		}
	}
	private static FillOutUserInfo instance;

	public InputField nameInputField;

	public void SetUserName()
	{

	}

	void SetUserAge()
	{

	}

	void SetUserPosition()
	{

	}

	void SetUserStudentNum()
	{

	}

	void SetUserDepartment()
	{

	}

	public void FillOut()
	{

	}

	public void ShowNextItem()
	{

	}
}
