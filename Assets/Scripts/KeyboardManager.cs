using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
	InputField inputField;
	private void Awake()
	{
		inputField = GetComponent<InputField>();
	}

	void Start()
    {
		inputField.ActivateInputField();
		inputField.Select();
	}
}
