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

	public void PlayTransition()
	{
		fadeImageAnimator.SetTrigger("FadeInOut");
		Invoke("ShowNextLayer", .25f);
	}

	int layerIndex;
	public GameObject[] layers;

	public void ShowNextLayer()
	{
		previousLayer.SetActive(false);
		layerIndex = (layerIndex + 1) % layers.Length;
		if (currentLayer.activeSelf != true) currentLayer.SetActive(true);
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
		PlayTransition();
	}
}
