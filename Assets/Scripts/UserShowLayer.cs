using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserShowLayer : MonoBehaviour
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


		UserInfo userInfo = UserInfoDisplayer.Instance.userInfo;

		if (!userInfo) return;

		title.text = userInfo.title;
		position.text = userInfo.positions;
		tool.text = userInfo.toolKR;
		intro.text = userInfo.introduce;
	}
}
