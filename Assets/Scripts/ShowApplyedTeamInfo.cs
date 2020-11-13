using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowApplyedTeamInfo : MonoBehaviour
{
	public Text title;
	public Text position;
	public Text tool;

	private void OnEnable()
	{
		SetPanelInfo();
	}

	public void SetInfo()
	{
		UserManager.Instance.SetApplyedTeamInfo(TeamInfoDisplayer.Instance.teamInfo);
	}

	void SetPanelInfo()
	{
		if (!TeamInfoDisplayer.Instance.teamInfo) return;

		title.text = TeamInfoDisplayer.Instance.teamInfo.title;
		position.text = TeamInfoDisplayer.Instance.teamInfo.positions;
		tool.text = TeamInfoDisplayer.Instance.teamInfo.toolKR;
	}
}
