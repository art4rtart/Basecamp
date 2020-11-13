using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamShowLayer : MonoBehaviour
{
	PanelController panelController;


	public Text title;
	public Text intro;
	public Text position;
	public Text tool;

	// Climber Info
	int membersNum;

    void OnEnable()
    {
		panelController = GetComponent<PanelController>();
		if (LayerManager.Instance.previousLayer != panelController.previousLayer) panelController.previousLayer = LayerManager.Instance.previousLayer;


		TeamInfo teamInfo =TeamInfoDisplayer.Instance.teamInfo;

		if (!teamInfo) return;

		title.text = teamInfo.title;
		position.text = teamInfo.positions;
		tool.text = teamInfo.toolKR;
		intro.text = teamInfo.introduce;
	}
}
