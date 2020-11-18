using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonNET : MonoBehaviour
{
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
        //string url = "https://api.github.com/users/her0iin/repos";
        string url = "https://api.github.com/repos/her0iin/basecamp/commits";

        UnityWebRequest www = UnityWebRequest.Get(url);
        //www.SetRequestHeader("application ", "json");
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            //Debug.Log(www.downloadHandler.text);
            //var response = JObject.Parse(www.downloadHandler.text);

            //var jsonString = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

            //var jsonString = JsonUtility.FromJson<Root>(www.downloadHandler.text);

            string response = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            
            var myDeserializedClass = JsonConvert.DeserializeObject<List<MyArray>>(response);
            Debug.Log(myDeserializedClass.Count);

            for (int i = 0; i < myDeserializedClass.Count; ++i)
            {
                Debug.Log(myDeserializedClass[i].commit.author.name + " " + myDeserializedClass[i].commit.message);
            }
        }
        else
        {
            Debug.Log("error");
        }

    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
[Serializable]
public class Author
{
    public string name { get; set; }
    public string email { get; set; }
    public DateTime date { get; set; }
}

[Serializable]
public class Committer
{
    public string name { get; set; }
    public string email { get; set; }
    public DateTime date { get; set; }
}
[Serializable]
public class Tree
{
    public string sha { get; set; }
    public string url { get; set; }
}
[Serializable]
public class Verification
{
    public bool verified { get; set; }
    public string reason { get; set; }
    public string signature { get; set; }
    public string payload { get; set; }
}
[Serializable]
public class Commit
{
    public Author author { get; set; }
    public Committer committer { get; set; }
    public string message { get; set; }
    public Tree tree { get; set; }
    public string url { get; set; }
    public int comment_count { get; set; }
    public Verification verification { get; set; }
}
[Serializable]
public class Author2
{
    public string login { get; set; }
    public int id { get; set; }
    public string node_id { get; set; }
    public string avatar_url { get; set; }
    public string gravatar_id { get; set; }
    public string url { get; set; }
    public string html_url { get; set; }
    public string followers_url { get; set; }
    public string following_url { get; set; }
    public string gists_url { get; set; }
    public string starred_url { get; set; }
    public string subscriptions_url { get; set; }
    public string organizations_url { get; set; }
    public string repos_url { get; set; }
    public string events_url { get; set; }
    public string received_events_url { get; set; }
    public string type { get; set; }
    public bool site_admin { get; set; }
}
[Serializable]
public class Committer2
{
    public string login { get; set; }
    public int id { get; set; }
    public string node_id { get; set; }
    public string avatar_url { get; set; }
    public string gravatar_id { get; set; }
    public string url { get; set; }
    public string html_url { get; set; }
    public string followers_url { get; set; }
    public string following_url { get; set; }
    public string gists_url { get; set; }
    public string starred_url { get; set; }
    public string subscriptions_url { get; set; }
    public string organizations_url { get; set; }
    public string repos_url { get; set; }
    public string events_url { get; set; }
    public string received_events_url { get; set; }
    public string type { get; set; }
    public bool site_admin { get; set; }
}
[Serializable]
public class Parent
{
    public string sha { get; set; }
    public string url { get; set; }
    public string html_url { get; set; }
}
[Serializable]
public class MyArray
{
    public string sha { get; set; }
    public string node_id { get; set; }
    public Commit commit { get; set; }
    public string url { get; set; }
    public string html_url { get; set; }
    public string comments_url { get; set; }
    public Author2 author { get; set; }
    public Committer2 committer { get; set; }
    public List<Parent> parents { get; set; }
}