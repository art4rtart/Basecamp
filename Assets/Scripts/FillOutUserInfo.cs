using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillOutUserInfo : MonoBehaviour
{
	public GameObject backpack;
	public CanvasGroup dragPanelCanvasGroup;
	public GameObject button;

	private void Awake()
	{
		backpack.transform.SetParent(this.gameObject.transform);
		StartCoroutine(FadeOut(2f));
		StartCoroutine(MoveDown(230f, 700f));
	}

	public void GoToTeamBuild(string _sceneName)
	{
		SceneManager.LoadScene(_sceneName);
	}

	IEnumerator FadeOut(float _fadeSpeed)
	{
		float alpha = 1f;

		while (alpha > 0)
		{
			alpha -= Time.deltaTime * _fadeSpeed;
			dragPanelCanvasGroup.alpha = alpha;
			yield return null;
		}

		dragPanelCanvasGroup.alpha = 0f;
	}

	IEnumerator MoveDown(float _targetPosY, float _moveSpeed)
	{
		yield return new WaitForSeconds(.5f);
		float posY = backpack.GetComponent<RectTransform>().localPosition.y;
		Debug.Log(posY);

		while (posY > _targetPosY)
		{
			posY -= Time.deltaTime * _moveSpeed;
			backpack.GetComponent<RectTransform>().localPosition = new Vector3(0f, posY, 0f);

			if(Mathf.Abs(posY-_targetPosY) < 300f) if(!button.activeSelf) button.SetActive(true);
			yield return null;
		}
		
		backpack.GetComponent<RectTransform>().localPosition = new Vector3(0f, _targetPosY, 0f);
	}
}
