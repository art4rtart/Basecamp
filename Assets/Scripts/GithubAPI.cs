using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GithubAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Github());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public string url;

	IEnumerator Github()
	{
		url = "string";
		WWW www = new WWW(url);
		yield return www;

		Debug.Log(www.ToString());
	}
}
