using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
	public Text userName;
	public Text userPosition;
	public Text userLevel;
	public Text userExp;

	private void Awake()
	{
		userName.text = UserManager.Instance.name;
		//userPosition.text = UserManager.Instance.position;
		//userLevel.text = UserManager.Instance.level.ToString();
		//userExp.text = UserManager.Instance.exp.ToString();
	}
}
