using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionSelect : MonoBehaviour
{
	public int contentSize;
	public int contentIndex;

	Vector3 velocity;
	Vector3 targetPoint;
	public float smoothTime = .2f;

	bool isCoroutineRunning;

	public GameObject content;
	public Animator infoAnimator;

	public Image[] contentCircles;

	public Color defaultColor;
	public Color highlightColor;
	public GameObject card;

	private void OnEnable()
	{
		infoAnimator.enabled = true;
	}

	private void Awake()
	{

	}

	public void PreviousButtonClick()
	{
		if (contentIndex < 1) return;
		contentIndex = Mathf.Clamp(contentIndex -= 1, 0, contentSize);

		if (isCoroutineRunning) { StopAllCoroutines(); content.transform.position = targetPoint; }
		StartCoroutine(MoveContent(false));

		SetContentColor();
	}

	public void NextButtonClick()
	{
		if (contentIndex > contentSize - 2) return;
		contentIndex = Mathf.Clamp(contentIndex += 1, 0, contentSize);

		if (isCoroutineRunning) { StopAllCoroutines(); content.transform.position = targetPoint; }
		StartCoroutine(MoveContent(true));

		SetContentColor();
	}

	IEnumerator MoveContent(bool _dir)
	{
		float dir = _dir ? 1 : -1;
		isCoroutineRunning = true;
		targetPoint = content.transform.position - Vector3.right * 480f * dir;
		Debug.Log(targetPoint);
		while (Vector3.Distance(content.transform.position, targetPoint) > .05f)
		{
			content.transform.position = Vector3.SmoothDamp(content.transform.position, targetPoint, ref velocity, smoothTime);
			yield return null;
		}

		content.transform.position = targetPoint;
		isCoroutineRunning = false;
	}

	void SetContentColor()
	{
		for (int i = 0; i < contentCircles.Length; i++)
		{
			contentCircles[i].color = defaultColor;
		}
		contentCircles[contentIndex].color = highlightColor;
	}

	public void SelectThisPosition(string _positionName)
	{
		UserManager.Instance.position = _positionName;
		card.transform.gameObject.SetActive(true);
		card.transform.GetChild(0).GetComponent<Text>().text = _positionName;
		this.gameObject.SetActive(false);
	}
}
