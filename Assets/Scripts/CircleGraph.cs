using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CircleGraph : MonoBehaviour
{
    public Image circlePrefab;
    public Text MessagePrefab;

    // Github API를 통해 받아와야할 데이터
    public string[] names;                   // 이름
    public Color[] nameColors;              // 색상
    public int[] CommitCntByName;           // 이름별 커밋 회수
    public int CommitCnt;                   // 전체 커밋 회수

    void Start()
    {
        //StartCoroutine(Test());
    }
    private void Update()
    {
    }

    void MakeGraph()
    {
        float zRotation = 0.0f;

        // 사람수 만큼
        for (int i = 0; i < names.Length; ++i)
        {
            // 이미지 생성
            Image newCircle = Instantiate(circlePrefab) as Image;
            
            newCircle.transform.SetParent(transform, false);
            newCircle.color = nameColors[i];
            
            // 현재 name의 커밋 회수 /  전체 커밋 회수 
            newCircle.fillAmount = (float)CommitCntByName[i] / (float)CommitCnt;
            newCircle.transform.GetChild(0).transform.GetComponent<Image>().fillAmount = newCircle.fillAmount;

            newCircle.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, zRotation));
            zRotation -= newCircle.fillAmount * 360.0f;
        }
    }

    IEnumerator Test()
    {
        var Data = GameObject.Find("Github_Manager").GetComponent<JsonNET>();
        yield return Data.StartCoroutine(Data.Github_GET());

        names = Data.names;

        nameColors = new Color[names.Length];
        nameColors[0] = Color.white;
        nameColors[1] = Color.red;

        CommitCntByName = Data.CommitCntByName;

        CommitCnt = Data.CommitCnt;

        MakeGraph();
    }
}
