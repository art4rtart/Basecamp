using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
	public static Backpack Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<Backpack>();
			return instance;
		}
	}
	private static Backpack instance;

	Image image;
	
	public int index;
	public int contentIndex;

	public Slider slider;
	public Animator dragAnimator;
	public Animator backpackAnimator;
	public Animator itemAddAnimator;
	public Animator swipeAnimator;

	RectTransform dragPanelRect;
	public RectTransform backpackRect;
	public Vector3 position;

	float positionY;
	public bool canAddItem;
	public bool itemPicked;

	[Header("ItemAdded")]
	public bool nameAdded;

	public Text userNameText;
	public ItemDrag itemDrag;

	public Text nameCardText;
	public Text ageCardText;
	public Text studentNumCardText;

	private void Awake()
	{
		image = GetComponent<Image>();
		dragPanelRect = GetComponent<RectTransform>();

		positionY = 1110 + dragPanelRect.localPosition.y - dragPanelRect.sizeDelta.y * .5f;
	}

	private void Update()
	{
		if (!canAddItem) return;

		if (!swipeAnimator.enabled) swipeAnimator.enabled = true;

		if (Input.GetTouch(0).phase == TouchPhase.Began) // and swipe down || Input.GetTouch(0).phase == TouchPhase.Began Input.GetMouseButtonDown(0)
		{
			if((Input.mousePosition.y < positionY || SwipeDetector.Instance.touchPos.y < positionY) && itemPicked)
			{
				dragAnimator.SetBool("Drag", true);
				backpackAnimator.SetBool("Open", true);
			}
		}

		else if (Input.GetTouch(0).phase == TouchPhase.Ended)  // || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)
		{
			if ((SwipeDetector.Instance.touchPos.y > positionY || Input.mousePosition.y > positionY) && itemPicked)
			{
				index++;
				slider.value = index;
				image.color = defaultColor;
				itemAddAnimator.SetTrigger("Add");
				
				swipeAnimator.SetTrigger("Hide");

				// what item is added
				if (!nameDataChecked)
				{
					nameAdded = true;
				}

				// show next Item after 1 second
				Invoke("ShowNextItem", 1f);

				// disapper item
				if (itemDrag != null) itemDrag.gameObject.SetActive(false);
			}
			dragAnimator.SetBool("Drag", false);
			backpackAnimator.SetBool("Open", false);

			itemPicked = false;
		}

		if((SwipeDetector.Instance.touchPos.y > positionY || Input.mousePosition.y > positionY) && itemPicked) image.color = highlightColor; 
		else image.color = defaultColor;
		CheckAddedItem();
	}

	public Color defaultColor;
	public Color highlightColor;

	[HideInInspector] public int askItemIndex;
	public GameObject[] askItems;
	public Animator filloutAnimator;

	public void ShowNextItem()
	{
		if (askItems[askItemIndex].activeSelf) askItems[askItemIndex].SetActive(false);
		askItemIndex++;
		filloutAnimator.SetInteger("ItemIndex", askItemIndex);
		askItems[askItemIndex].SetActive(true);
	}

	public void ShowPrevItem()
	{
		if (!askItems[askItemIndex].activeSelf) askItems[askItemIndex].SetActive(false);
		askItemIndex--;
		filloutAnimator.SetInteger("ItemIndex", askItemIndex);
		askItems[askItemIndex].SetActive(true);
	}

	[HideInInspector] public bool nameDataChecked;
	
	public void CheckAddedItem()
	{
		if(nameAdded && !nameDataChecked)
		{
			StartCoroutine(MoveBackpackPosition());
			userNameText.enabled = true;
			userNameText.text = UserManager.Instance.name + "의 배낭";
			nameAdded = false;
			nameDataChecked = true;
		}
	}

	IEnumerator MoveBackpackPosition()
	{
		Vector3 target = backpackRect.transform.localPosition + Vector3.up * 40f;
		while (backpackRect.transform.localPosition.y < target.y)
		{
			backpackRect.transform.localPosition += Vector3.up * Time.deltaTime * 120f;
			yield return null;
		}
	}
}
