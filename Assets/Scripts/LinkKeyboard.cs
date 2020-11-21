using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkKeyboard : MonoBehaviour
{
	InputField inputField;
	public float appearTime;
	
	IEnumerator ShowKeyboardCor;

	public enum KeyboardType { None = -1, KR, ENG, Num }
	public KeyboardType KeyType = KeyboardType.KR;

	public bool showKeyboardInput;
	GameObject KeyboardInput;

	bool itemShow;
	public GameObject item;

	private void OnEnable()
	{
		inputField = GetComponent<InputField>();
		ShowKeyboardCor = ShowKeyboard(appearTime, (int)KeyType);
		StartCoroutine(ShowKeyboardCor);
		VirtualKeyboard.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(showKeyboardInput);
		VirtualKeyboard.Instance.inputField = inputField;
	}

	private void Update()
	{
		if (active) {
			if (!VirtualKeyboard.Instance.keyboardAppearStatus)
			{
				item.SetActive(true);
				active = false;
				Backpack.Instance.canAddItem = true;
				Backpack.Instance.swipeAnimator.gameObject.SetActive(false);
				Backpack.Instance.swipeAnimator.gameObject.SetActive(true);
				this.gameObject.transform.parent.gameObject.SetActive(false);
			}
		}
	}

	IEnumerator ShowKeyboard(float _appearTime, int _keyboardType)
	{
		yield return new WaitForSeconds(_appearTime);
		InputFieldClicked(_keyboardType);
	}

	bool active;
	public void InputFieldClicked(int _keyboardType)
	{
		active = !active;
		VirtualKeyboard.Instance.KeyboardApper(active, _keyboardType);
	}

	private void OnDisable()
	{
		VirtualKeyboard.Instance.inputField = null;
	}
}
