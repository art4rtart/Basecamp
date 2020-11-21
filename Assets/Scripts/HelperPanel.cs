using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperPanel : MonoBehaviour
{
	public Vector2 mousePosition;


	[Header("Content 1")]
	Vector2 p1;
	public Vector2[] p2;
	public Vector2[] p3;
	int length;

	public Image fillImage;
	public RectTransform fillRect;

	private void Awake()
	{
		p1 = new Vector2(540f, 1235f);
	}
	private void Check()
	{
		length = p2.Length;
		//p2 = new Vector2(1080, 695f);
		//p3 = new Vector2(1080f, 1775f);
		float[] alpha = new float[length];
		float[] beta = new float[length];
		float[] gamma = new float[length];

		for (int i = 0; i < length; i++)
		{
			alpha[i] = ((p2[i].y - p3[i].y) * (mousePosition.x - p3[i].x) + (p3[i].x - p2[i].x) * (mousePosition.y - p3[i].y)) /
					((p2[i].y - p3[i].y) * (p1.x - p3[i].x) + (p3[i].x - p2[i].x) * (p1.y - p3[i].y));
			beta[i] = ((p3[i].y - p1.y) * (mousePosition.x - p3[i].x) + (p1.x - p3[i].x) * (mousePosition.y - p3[i].y)) /
							((p2[i].y - p3[i].y) * (p1.x - p3[i].x) + (p3[i].x - p2[i].x) * (p1.y - p3[i].y));
			gamma[i] = 1.0f - alpha[i] - beta[i];
		}

		for(int i = 0; i < length; i++)
		{
			if (alpha[i] > 0 && beta[i] > 0 && gamma[i] > 0)
			{
				fillRect.eulerAngles = new Vector3(0f, 0f, i * -90f + 45f);
				Debug.Log("Current Index is " + i);
			}
		}
	}

	public Text posTex;
	private void Update()
	{
		mousePosition =  Input.GetTouch(0).position;

		Check();
	}
}
