using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Layer
{
	logo = 0,
	main,
	team,
	chat,
	report
}

public class LayerManager : MonoBehaviour
{
	public static LayerManager Instance {
		get {
			if (instance != null) return instance;
			instance = FindObjectOfType<LayerManager>();
			return instance;
		}
	} private static LayerManager instance;
	public event Action layerActiveEvent;

	public Animator fadeImageAnimator;

	public Layer layerName;
	public bool isTouched;

	public float logoShowTime;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) PlayFadeInOut();
	}

	IEnumerator Logo()
	{
		Debug.Log("Logo Layer is Played!");

		yield return new WaitForSeconds(logoShowTime);

		StartCoroutine(Main());
	}

	IEnumerator Main()
	{
		Debug.Log("Main Layer is Played!");

		while(!isTouched)
		{
			if (Input.GetMouseButtonDown(0)) isTouched = true;
			yield return null;
		}

		StartCoroutine(TeamPlay());
	}

	IEnumerator TeamPlay()
	{
		Debug.Log("TeamPlay Layer is Played!");

		yield return null;
	}

	public void PlayFadeInOut()
	{
		fadeImageAnimator.SetTrigger("FadeInOut");
		Invoke("ShowNextLayer", .25f);
	}

	int layerIndex;
	public GameObject[] layers;

	public void ShowNextLayer()
	{
		layers[layerIndex].SetActive(false);
		layerIndex = (layerIndex + 1) % layers.Length;
		layers[layerIndex].SetActive(true);
	}



	public GameObject currentLayer;
	public GameObject previousLayer;
	public GameObject nextLayer;

	public void SetLayer(GameObject _current, GameObject _previous, GameObject _next)
	{
		currentLayer = _current;
		previousLayer = _previous;
		nextLayer = _next;
	}

	public void ChangeLayer(GameObject _current, GameObject _next)
	{
		previousLayer = _current;
		currentLayer = _next;
		Debug.Log(currentLayer.name);

		previousLayer.SetActive(false);
		currentLayer.SetActive(true);
	}
}
