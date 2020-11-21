using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{
	RectTransform rect;
	public Vector3 originPos;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();
	}

	public void PointerDown()
	{
		Backpack.Instance.itemPicked = true;
		Backpack.Instance.itemDrag = this.gameObject.GetComponent<ItemDrag>();
		originPos = rect.localPosition;
	}

	public void PointerUp()
	{
		Backpack.Instance.itemPicked = false;
		Backpack.Instance.itemDrag = null;
		rect.localPosition = originPos;
	}

	public void DragItem()
	{
		// rect.transform.position = Input.mousePosition;      //pc
		rect.transform.position = Input.GetTouch(0).position;
	}
}
