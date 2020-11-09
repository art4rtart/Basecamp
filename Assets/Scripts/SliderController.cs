using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
	Slider slider;

	[Header("Slider Value Text")]
	public TextMeshProUGUI sliderValueText;

	private void Awake()
	{
		slider = GetComponent<Slider>();
	}

	public void OnValueChange()
	{
		sliderValueText.text = slider.value.ToString();
	}
}
