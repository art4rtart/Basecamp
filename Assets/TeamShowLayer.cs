using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamShowLayer : MonoBehaviour
{
	PanelController panelController;

    void OnEnable()
    {
		panelController = GetComponent<PanelController>();
		if (LayerManager.Instance.previousLayer != panelController.previousLayer) panelController.previousLayer = LayerManager.Instance.previousLayer;
	}
}
