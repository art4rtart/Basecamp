using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

using UnityEngine.Networking;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TMPro;

public class JsonNET : MonoBehaviour
{
    public Image circlePrefab;
    public GameObject CommitBoxPrefab;
    
    public string[] names;
    public Color[] nameColors;              // 색상
    public int[] CommitCntByName;
    public int CommitCnt;
    public CommitMessage[] CommitMessage;

    GameObject Graph;
    GameObject Commit_Content;

    // Start is called before the first frame update    
    void Start()
    {
        Graph = GameObject.Find("Graph");
        Commit_Content = GameObject.Find("Commit_Content");


        StartCoroutine(Github_GET());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Github_GET()
    {
        string url = "https://api.github.com/repos/her0iin/basecamp/commits";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            string response = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            var myDeserializedClass = JsonConvert.DeserializeObject<List<MyArray>>(response);


            // 이름 개수만큼 배열 할당
            int[] nameCnt = new int[names.Length];
            CommitCntByName = new int[names.Length];

            // 커밋 개수만큼 배열 할당
            CommitMessage = new CommitMessage[myDeserializedClass.Count];
            CommitMessage.InitializeArray();

            // 이름 개수만큼 반복문
            for (int i = 0; i < names.Length; ++i) {
                for (int j = 0; j < myDeserializedClass.Count; ++j) {
                    // 이름별 커밋회수
                    if (myDeserializedClass[j].commit.author.name.Equals(names[i])) nameCnt[i]++;
                }
                CommitCntByName[i] = nameCnt[i];
            }

            //string temp = myDeserializedClass[0].commit.author.name;

            char separatorChar = '\n';
            for (int k = 0; k < myDeserializedClass.Count; ++k)
            {
                // Message Array
                CommitMessage[k].name = myDeserializedClass[k].commit.author.name;
            
                CommitMessage[k].message = myDeserializedClass[k].commit.message.Split(separatorChar)[0];
            
                CommitMessage[k].date = myDeserializedClass[k].commit.author.date;
            }

            nameColors = new Color[names.Length];
            nameColors[0] = Color.white;
            nameColors[1] = Color.red;
            nameColors[2] = Color.green;

            // 전체 커밋 회수
            CommitCnt = myDeserializedClass.Count;

            // 그래프 그리기
            MakeGraph();

            // Scroll View 채우기
            MakeCommitBox();
        }
        else
        {
            Debug.Log("error");
        }
    }

    void MakeGraph()
    {
        float zRotation = 0.0f;

        // 사람수 만큼
        for (int i = 0; i < names.Length; ++i)
        {
            // 이미지 생성
            Image newCircle = Instantiate(circlePrefab) as Image;

            newCircle.transform.SetParent(Graph.transform, false);
            newCircle.color = nameColors[i];

            // 현재 name의 커밋 회수 /  전체 커밋 회수 
            newCircle.fillAmount = (float)CommitCntByName[i] / (float)CommitCnt;
            newCircle.transform.GetChild(0).transform.GetComponent<Image>().fillAmount = newCircle.fillAmount;

            newCircle.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, zRotation));
            zRotation -= newCircle.fillAmount * 360.0f;
        }
    }

    void MakeCommitBox()
    {
        float zRotation = 0.0f;

        for (int i = 0; i < CommitMessage.Length; ++i) {
            var newCommitBox = Instantiate(CommitBoxPrefab) as GameObject;
        
            newCommitBox.transform.SetParent(Commit_Content.transform, false);
            newCommitBox.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, zRotation));

            newCommitBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = CommitMessage[i].name;

            newCommitBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = CommitMessage[i].message;
            
            string myConvertedDate = CommitMessage[i].date.ToString("yyyy/MM/dd hh:mm:ss");
            newCommitBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = myConvertedDate;
        }
    }
}

[Serializable]
public class Author
{
    public string name { get; set; }
    public string email { get; set; }
    public DateTime date { get; set; }
}
[Serializable]
public class Commit
{
    public Author author { get; set; }
    public string message { get; set; }
}
[Serializable]
public class MyArray
{
    public Commit commit { get; set; }
}

public class CommitMessage
{
    public string name;
    public string message;
    public DateTime date;
}

public static class Untility
{
    public static void InitializeArray<T>(this T[] array) where T : class, new()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new T();
        }
    }

    public static T[] InitializeArray<T>(int length) where T : class, new()
    {
        T[] array = new T[length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new T();
        }

        return array;
    }
}