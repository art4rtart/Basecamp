using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
	Text text;
	Color textColor;

	private void Awake()
	{
		text = GetComponent<Text>();
		textColor = text.color;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.V)) StartFadeInOut();
	}
	public void StartFadeInOut()
	{
		Debug.Log("HI");
		StartCoroutine(FadeInOut());
	}

	IEnumerator FadeInOut()
	{
		float alpha = textColor.a;

		while (true)
		{
			text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
			alpha = Mathf.PingPong(Time.time * .25f, .5f);
			Debug.Log(alpha);
			yield return null;
		}
	}
}
