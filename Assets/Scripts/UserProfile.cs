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
		if (userName) userName.text = UserManager.Instance.name;
		if (userPosition) userPosition.text = UserManager.Instance.position;
		if (userLevel) userLevel.text = "Lv." + UserManager.Instance.level;
		if(userExp) userExp.text = UserManager.Instance.exp.ToString();
	}
}
