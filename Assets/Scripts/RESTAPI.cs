using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System;
using System.IO;

public class RESTAPI : MonoBehaviour
{
    [SerializeField]
    public class CommitInfo
    {
        public string name;
        public string date;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Github_GET());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Github_GET()
    {
        string url = "https://api.github.com/repos/her0iin/basecamp/commits";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            //Debug.Log(www.downloadHandler.text);

        }
        else
        {
            Debug.Log("error");
        }

    }
}

