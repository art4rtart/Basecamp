using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
	public static UserManager Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<UserManager>();
			return instance;
		}
	}
	private static UserManager instance;

	[Header("Default")]
	public string name;
	public int age;
	public string position;
	public string department;

	public bool hasTeam;

	public bool hasApplyed;
	public bool hasVolunteer;

	public float timeLeft;

	private void Update()
	{
		if (hasApplyed)
		{
			if(timeLeft > 0) timeLeft = Mathf.Clamp(timeLeft -= Time.deltaTime, 0, 1000);
		}
	}

	public TeamInfo teamInfo;

	public void SetApplyedTeamInfo(TeamInfo _teamInfo)
	{
		teamInfo = _teamInfo;
	}

	public bool IsSameTeam()
	{
		if (teamInfo == TeamInfoDisplayer.Instance.teamInfo) { Debug.Log("this is applyed team");  return true; }
		else return false;
	}
}
