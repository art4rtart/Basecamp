using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowApplyedTeamInfo : MonoBehaviour
{
	public void SetInfo()
	{
		UserManager.Instance.SetApplyedTeamInfo(TeamInfoDisplayer.Instance.teamInfo);
	}
}
