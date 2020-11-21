using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClimber : MonoBehaviour
{
	public Animator ClimberAnimator;

	private void Start()
	{

	}

	public void OpenAddPanel()
	{
		ClimberAnimator.SetBool("Open", true);
	}

	public void CloseAddPanel()
	{
		ClimberAnimator.SetBool("Open", false);
	}
}
