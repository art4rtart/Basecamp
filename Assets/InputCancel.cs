using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCancel : MonoBehaviour
{
	public GameObject LinkedItem;
	Vector3 originPos;

	private void Awake()
	{
		originPos = this.gameObject.transform.parent.gameObject.GetComponent<RectTransform>().localPosition;
	}
	public void CancelInput()
	{
		LinkedItem.SetActive(true);
		this.gameObject.transform.parent.gameObject.GetComponent<RectTransform>().localPosition = originPos;
		this.gameObject.transform.parent.gameObject.SetActive(false);
		Backpack.Instance.swipeAnimator.enabled = false;
		Backpack.Instance.swipeAnimator.GetComponent<CanvasGroup>().alpha = 0f;
		Backpack.Instance.canAddItem = false;
	}
}
