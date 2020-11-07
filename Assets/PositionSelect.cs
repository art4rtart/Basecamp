using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSelect : MonoBehaviour
{
	public int contentSize;
	public int contentIndex;

	public GameObject content;

	public void PreviousButtonClick()
	{
		if (contentIndex < 1) return;
		contentIndex = Mathf.Clamp(contentIndex -= 1, 0, contentSize);
		Debug.Log(contentIndex);

		if (isCoroutineRunning) { StopAllCoroutines(); content.transform.position = targetPoint; }
		StartCoroutine(MoveContent(false));
	}

	public void NextButtonClick()
	{
		if (contentIndex > contentSize - 2) return;
		contentIndex = Mathf.Clamp(contentIndex += 1, 0, contentSize);
		Debug.Log(contentIndex);

		if (isCoroutineRunning) { StopAllCoroutines(); content.transform.position = targetPoint; }
		StartCoroutine(MoveContent(true));
	}

	Vector3 velocity;
	Vector3 targetPoint;
	public float smoothTime = .2f;

	bool isCoroutineRunning;

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
}
