using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PanelController : MonoBehaviour
{
	[Header("Previous Layer (Must Be Set!)")]
	public GameObject previousLayer;
	[Header("Current Layer")]
	public GameObject currentLayer;
	[Header("Next Layer")]
	public GameObject[] nextLayer;
	int nextLayerIndex;

	private void Awake()
	{
		if(previousLayer == null) Debug.Log(this.gameObject.name + " previous layer is not added! You Must Set previous Layer !!!");
	}

	public void Start()
	{
		currentLayer = this.gameObject;
		if(nextLayer.Length != 0) LayerManager.Instance.SetLayer(this.gameObject, LayerManager.Instance.previousLayer, nextLayer[0]);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N) && nextLayer != null) GoNextLayer(nextLayerIndex);
		if (Input.GetKeyDown(KeyCode.X) && previousLayer != null) GoBackLayer();
	}

	public void GoNextLayer(int _index)
	{
		LayerManager.Instance.ChangeLayer(currentLayer, nextLayer[Mathf.Clamp(_index, 0, nextLayer.Length)]);
	}

	public void GoBackLayer()
	{
		LayerManager.Instance.ChangeLayer(currentLayer, previousLayer);
	}

}
