using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTeamStatus : MonoBehaviour
{
	[Header("ApplyedStatus")]
	public GameObject applyed;
	public GameObject notApplyed;

	[Header("RecruitStatus")]
	public GameObject volunteer;
	public GameObject noVolunteer;

	// Start is called before the first frame update
	void OnEnable()
    {
		bool apply = UserManager.Instance.hasApplyed ? true : false;
		applyed.SetActive(apply);
		notApplyed.SetActive(!apply);

		bool recruited = UserManager.Instance.hasVolunteer ? true : false;
		volunteer.SetActive(recruited);
		noVolunteer.SetActive(!recruited);
	}
}
