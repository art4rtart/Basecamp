using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactButtonController : MonoBehaviour
{
	Animator animator;
	Button button;

	bool clicked;

	bool applyed;

	public float timeLeft;
	float initTime;

	public Text applyCancelText;
	public Text timeLeftText;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		button = GetComponent<Button>();
		initTime = timeLeft;
	}

	public void Update()
	{
		if (applyed)
		{
			timeLeft -= Time.deltaTime;

			if (timeLeft < 0)
			{
				animator.SetTrigger("ShowCancel");
				applyed = false;
				timeLeft = initTime;
			}

			else
			{
				string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
				string seconds = (timeLeft % 60).ToString("00");

				timeLeftText.text = string.Format("{0}:{1} 이후 취소 가능", minutes, seconds);
			}
		}
	}

	public void ApplyClicked()
	{
		clicked = !clicked;

		UserManager.Instance.hasApplyed = true;
		if (clicked) {
			animator.SetTrigger("Apply");
			applyCancelText.text = "지원 완료";
		}
		else animator.SetTrigger("ClickCancel");
	}

	public void ApplyCancelTrigger()
	{
		applyCancelText.text = "지원 취소하기";
	}

	public void TimerTrigger()
	{
		applyed = true;
	}

	public void CancelApplyClicked()
	{
		animator.SetTrigger("Cancel");
	}
}
