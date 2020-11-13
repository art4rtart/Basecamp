using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance
	{
		get
		{
			if (instance != null)
				return instance;
			instance = FindObjectOfType<UIManager>();
			return instance;
		}
	}
	private static UIManager instance;

	public GameObject viewMain;
    public TMP_InputField usernameField;

	public GameObject menuFriend;
	public GameObject menuChat;
	public GameObject menuSetting;


	private void Start()
	{
		ConnectToServer();
	}
	/// <summary>Attempts to connect to the server.</summary>
	public void ConnectToServer()
    {
        // viewMain.SetActive(true);
        // usernameField.interactable = false;
        Client.instance.ConnectToServer();
    }
}
