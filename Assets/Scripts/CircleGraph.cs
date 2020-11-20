using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CircleGraph : MonoBehaviour
{
    public Image circlePrefab;

    // Github API를 통해 받아와야할 데이터
    public string[] names;                   // 이름
    public Color[] nameColors;              // 색상
    public int[] CommitCntByName;           // 이름별 커밋 회수
    public int CommitCnt;                   // 전체 커밋 회수

    void Start()
    {
        // Github Script
        var Data = GameObject.Find("Github_Manager").GetComponent<JsonNET>();
        names = Data.names;
        Debug.Log(names.Length);

        nameColors = new Color[names.Length];
        for (int i = 0; i < names.Length; ++i)
        {
            float rgb = Random.Range(0.0f, 1.0f);
            nameColors[i] = new Color(rgb, rgb, rgb);
        }

        CommitCntByName = new int[names.Length];
        CommitCntByName = Data.CommitCntByName;
        Debug.Log(CommitCntByName.Length);

        CommitCnt = Data.CommitCnt;

        MakeGraph();
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
            newCircle.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, zRotation));
            zRotation -= newCircle.fillAmount * 360.0f;
        }
    }
}
