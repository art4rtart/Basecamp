using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactButtonController : MonoBehaviour
{
	Animator animator;
	Button button;

	public bool isClicked;

	float initTime;

	public Text applyCancelText;
	public Text timeLeftText;
	public Text alreadyApplyed;

	public Color defaultColor;

	private void OnEnable()
	{
		Debug.Log("OnEnable");
		if (UserManager.Instance.hasApplyed) {
			if(UserManager.Instance.IsSameTeam())
			{
				isClicked = true; ApplyText.enabled = false;
				applyCancelText.text = "지원 완료";
				applyCancelText.enabled = true;
				timeLeftText.enabled = true;
				UserManager.Instance.hasApplyed = true;
				if(UserManager.Instance.timeLeft < 0) button.interactable = false;
				alreadyApplyed.enabled = false;
			}

			else
			{
				alreadyApplyed.enabled = true;
				ApplyText.enabled = false;
				applyCancelText.enabled = false;
				timeLeftText.enabled = false;
			}
		}
	}

	private void Awake()
	{
		animator = GetComponent<Animator>();
		button = GetComponent<Button>();
		initTime = UserManager.Instance.timeLeft;
	}

	public Color normalColor;
	public void Update()
	{
		if (UserManager.Instance.hasApplyed)
		{
			if (UserManager.Instance.timeLeft <= 0)
			{
				applyCancelText.text = "지원 취소하기";
				button.interactable = true;

				var colors = button.colors;
				colors.normalColor = normalColor;
				button.colors = colors;
			}

			string minutes = Mathf.Floor(UserManager.Instance.timeLeft / 60).ToString("00");
			string seconds = (UserManager.Instance.timeLeft % 60).ToString("00");
			timeLeftText.text = string.Format("{0}:{1} 이후 취소 가능", minutes, seconds);
		}
	}

	public Text ApplyText;

	public void ApplyClicked()
	{
		isClicked = !isClicked;
		if (isClicked)
		{
			// 지원
			ApplyText.enabled = false;
			applyCancelText.text = "지원 완료";
			applyCancelText.enabled = true;
			timeLeftText.enabled = true;
			button.image.color = normalColor;
			UserManager.Instance.hasApplyed = true;
			button.interactable = false;

			UserManager.Instance.SetApplyedTeamInfo(TeamInfoDisplayer.Instance.teamInfo);
		}
		else {
			// 지원 취소

			button.interactable = false;

			var colors = button.colors;
			colors.normalColor = defaultColor;
			button.colors = colors;
			button.image.color = defaultColor;
			ApplyText.enabled = true;
			applyCancelText.enabled = false;
			timeLeftText.enabled = false;
			button.interactable = true;
			UserManager.Instance.hasApplyed = false;
			UserManager.Instance.timeLeft = initTime;
		}
	}

	public void CancelApplyClicked()
	{
		animator.SetTrigger("Cancel");
	}
}
