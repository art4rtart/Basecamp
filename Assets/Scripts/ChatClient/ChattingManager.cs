using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class ChattingManager : MonoBehaviour
{
	public static ChattingManager Instance
	{
		get
		{
			if (instance != null)
				return instance;
			instance = FindObjectOfType<ChattingManager>();
			return instance;
		}
	}
	private static ChattingManager instance;
	public InputField messageInputField;
	public string myMessage;
	private RectTransform contentUserRectTransform;

	public GameObject messageFrame;
	public List<GameObject> messages = new List<GameObject>();
	public GameObject contents;
	public Scrollbar ChattingScrollbar;

	public float posY;

	float maxValue;

	public int memberCount;

	int hours;
	int minutes;

	[Header("HelperPanel")]
	public GameObject helperPanel;
	bool isPanelPressed;
	float pressedTimer;

	public float targetPressTime = .5f;
	private void Awake()
	{
		contentUserRectTransform = contents.GetComponent<RectTransform>();
	}

	private void Update()
	{
		if(!SwipeDetector.Instance.isSwiping) scrollrect.verticalNormalizedPosition = 0f;

		if (!isPanelPressed) return;
		if(pressedTimer < targetPressTime) pressedTimer = Mathf.Clamp(pressedTimer += Time.deltaTime, 0, targetPressTime);
		else helperPanel.SetActive(true);
	}

	string GetTime()
	{
		hours = DateTime.Now.Hour;
		minutes = DateTime.Now.Minute;

		string am = "오전";
		if (hours > 12) { hours -= 12; am = "오후"; }

		string text = am + string.Format(" {0:D1}:{1:D2}", hours, minutes);

		return text;
	}

	Text messageTimeText;
	Text nameText;


	public bool userChatted;

	public void CreateMessageBox(bool _isMyMessage, string _otherMessage)
	{
		GameObject _message = Instantiate(messageFrame, transform.position, Quaternion.identity);
		_message.transform.SetParent(contents.transform);
		_message.transform.localPosition = Vector3.zero;
		_message.transform.localScale = Vector3.one;

		RectTransform imageBox = _message.transform.GetChild(0).GetComponent<RectTransform>();
		RectTransform messageText = _message.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
		RectTransform imageBoxCap = _message.transform.GetChild(1).GetComponent<RectTransform>();

		RectTransform imageBoxCapTime = _message.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
		RectTransform imageBoxCapRead = _message.transform.GetChild(1).GetChild(1).GetComponent<RectTransform>();
		Text name = _message.transform.GetChild(2).GetComponent<Text>();

		string message = _isMyMessage ? myMessage : _otherMessage;

		// Calculate Space Size
		string source = message;
		int spaceCount = source.Split(' ').Length - 1;

		float spaceSize;
		if (spaceCount < 1) spaceSize = spaceCount * 0f;
		else spaceSize = spaceCount * 10f;

		// Calculate Letter Size
		string noSpaeMessage = message;
		noSpaeMessage = noSpaeMessage.Replace(" ", "");
		float letterCount = noSpaeMessage.Length;
		float letterSize = 32 * letterCount;

		// Calculate Special Letter Size
		float specialCount = CheckingSpecialText(message);
		float specialSize = specialCount * 5f;

		_message.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;

		if (_isMyMessage)
		{
			messageText.sizeDelta = new Vector2(letterSize + spaceSize + specialSize, 75f);

			imageBox.sizeDelta = messageText.sizeDelta + Vector2.right * 20f;
			imageBox.localPosition = new Vector2(460f - (imageBox.sizeDelta.x - 50) / 2, 0f);
			imageBoxCap.localPosition = new Vector2(460f - messageText.sizeDelta.x, 0f);

			if (messageTimeText != null && messageTimeText.text == GetTime())
			{
				if(userChatted) messageTimeText.enabled = false;
				name.enabled = false;

				if (!userChatted && !name.enabled) name.enabled = true;

				if (!userChatted)
				{
					_message.transform.GetComponent<RectTransform>().sizeDelta += Vector2.up * _message.transform.GetComponent<RectTransform>().sizeDelta.y;
					_message.transform.GetChild(0).GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
					imageBoxCap.localPosition -= Vector3.up * 50f;
					name.GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;

					minusCount++;
				}
			}

			else
			{
				if (!name.enabled) name.enabled = true;
				if (nameText)
				{
					_message.transform.GetComponent<RectTransform>().sizeDelta += Vector2.up * _message.transform.GetComponent<RectTransform>().sizeDelta.y;
					_message.transform.GetChild(0).GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
					imageBoxCap.localPosition -= Vector3.up * 50f;
					name.GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
				}

				minusCount++;
			}

			userChatted = true;
		}

		else
		{
			_message.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = _otherMessage;

			messageText.sizeDelta = new Vector2(32 * letterCount + spaceSize + specialSize, 75f);

			imageBox.sizeDelta = messageText.sizeDelta + Vector2.right * 20f;
			imageBox.localPosition = new Vector2(-460f + (imageBox.sizeDelta.x - 50) / 2, 0f);
			imageBoxCap.localPosition = new Vector2(-460f + messageText.sizeDelta.x, 0f);

			imageBoxCapTime.localPosition *= (Vector2.left + Vector2.up);
			imageBoxCapRead.localPosition *= (Vector2.left + Vector2.up);
			name.GetComponent<RectTransform>().localPosition *= (Vector2.left + Vector2.up);

			name.alignment = TextAnchor.MiddleLeft;
			imageBoxCapTime.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
			imageBoxCapRead.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

			if (messageTimeText != null && messageTimeText.text == GetTime())
			{
				if (!userChatted) messageTimeText.enabled = false;
				name.enabled = false;

				if (userChatted && !name.enabled) name.enabled = true;
				if (userChatted)
				{
					_message.transform.GetComponent<RectTransform>().sizeDelta += Vector2.up * _message.transform.GetComponent<RectTransform>().sizeDelta.y;
					_message.transform.GetChild(0).GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
					imageBoxCap.localPosition -= Vector3.up * 50f;
					name.GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
					minusCount++;
				}
			}

			else
			{
				if (!name.enabled) name.enabled = true;
				if (nameText)
				{
					_message.transform.GetComponent<RectTransform>().sizeDelta += Vector2.up * _message.transform.GetComponent<RectTransform>().sizeDelta.y;
					_message.transform.GetChild(0).GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
					imageBoxCap.localPosition -= Vector3.up * 50f;
					name.GetComponent<RectTransform>().localPosition -= Vector3.up * 50f;
				}
				minusCount++;
			}

			userChatted = false;
		}

		nameText = name;

		imageBoxCapRead.GetComponent<Text>().text = memberCount.ToString();
		messageTimeText = imageBoxCapTime.GetComponent<Text>();
		messageTimeText.text = GetTime();

		if (ChattingScrollbar.size != 1) { contentUserRectTransform.anchoredPosition += Vector2.up * 110f; posY = posY + 110f + minusCount * 50f; }
		
		messages.Add(_message);

		scrollrect.verticalNormalizedPosition = 0f;
	}

	public ScrollRect scrollrect;
	int minusCount;

	public void SendMessage()
	{
		myMessage = messageInputField.text;
		ClientSend.MessageData(myMessage);
		messageInputField.text = null;
	}

	public void SaveChattingData()
	{
		Debug.Log("Chat Saved");
	}

	public int CheckingSpecialText(string _text)
	{
		string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
		MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(_text, str);

		int count = matches.Count;
		return count;
	}

	public void ChatPanelDown()
	{
		isPanelPressed = true;
	}

	public void ChatPanelUp()
	{
		isPanelPressed = false;
		helperPanel.GetComponent<Animator>().SetBool("Show", false);
		Invoke("HelperPanelOff", .5f);
		pressedTimer = 0f;
	}

	void HelperPanelOff()
	{
		helperPanel.SetActive(false);
	}
}
