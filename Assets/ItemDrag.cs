using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{
	RectTransform rect;
	public Vector3 originPos;
	public Text itemSubtitle;

	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		if (itemSubtitle) itemSubtitle.text = UserManager.Instance.department + "네요!";
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
		rect.transform.position = Input.mousePosition;      //pc
															// rect.transform.position = Input.GetTouch(0).position;
	}


	public void AddItem()
	{
		// item move Animation
		StartCoroutine(Move());
	}

	public float moveSpeed = 100f;
	public float multiplier = 100f;
	public bool isFirstItem;

	IEnumerator Move()
	{
		Vector3 target;
		if (isFirstItem) target = Backpack.Instance.backpackRect.position + Vector3.up * 195f;
		else target = Backpack.Instance.backpackRect.position + Vector3.up * 150f;

		while (Vector3.Distance(rect.position, target) > 0.05f)
		{
			rect.position = Vector3.Lerp(rect.position, target, moveSpeed * Time.deltaTime);
			rect.localScale -= (Vector3.right * multiplier + Vector3.up * multiplier) * Time.deltaTime;
			rect.localScale = new Vector2(Mathf.Clamp(rect.localScale.x, 0, 600f), Mathf.Clamp(rect.localScale.y, 0, 300f));
			yield return null;
		}

		rect.position = target;
		this.gameObject.SetActive(false);
	}
}
