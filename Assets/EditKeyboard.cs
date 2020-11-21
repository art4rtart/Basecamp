using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditKeyboard : MonoBehaviour
{
	public enum KeyboardType { None = -1, KR, ENG, Num }
	public KeyboardType KeyType = KeyboardType.KR;

	bool active;

	public bool isParent;
	GameObject parent;
	Vector3 parentOrigin;
	Vector3 parentTarget;

	RectTransform rect;
	[HideInInspector] public float keyboardPosY;
	public float paddingY;

	Vector3 perfectPos;
	float targetValue;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();

		if (!isParent) parent = this.transform.parent.gameObject;
		else parent = this.gameObject;
	}

	private void Start()
	{
		parentOrigin = parent.transform.position;

		keyboardPosY = rect.position.y - (rect.sizeDelta.y / 2) - paddingY;
		targetValue = -(keyboardPosY - VirtualKeyboard.Instance.keyboardHeight);
		Debug.Log(targetValue);
	}

	IEnumerator cor;
	public void ChangeInfo()
    {
		cor = MoveContent(active, targetValue);
		if(!VirtualKeyboard.Instance.isActive)	VirtualKeyboard.Instance.KeyboardApper(true, (int)KeyType);

		if (targetValue < 0) return;
		else StartCoroutine(cor);
	}

	public void ChangeInfoEnd()
	{
		cor = MoveContent(active, 0f);
		if (VirtualKeyboard.Instance.isActive) VirtualKeyboard.Instance.KeyboardApper(false, (int)KeyType);
		StartCoroutine(cor);
	}

	IEnumerator MoveContent(bool _isDown, float _target)
	{
		float lerpSpeed = 0f;

		Vector3 target = parentOrigin + Vector3.up * _target;

		while (Vector3.Distance(parent.transform.position, target) > 0.05f)
		{
			lerpSpeed += Time.deltaTime * .1f;
			parent.transform.position = Vector3.Lerp(parent.transform.position, target, lerpSpeed);
			yield return null;
		}

		parent.transform.position = target;
	}
}
