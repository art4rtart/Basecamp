using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TMPro;

public class ReportLayer : MonoBehaviour
{
    [SerializeField]
    GameObject ReportBoxPrefab;

    [SerializeField]
    GameObject ReportContent;

    [SerializeField]
    GameObject SaveReportLayerPrefab;

    [SerializeField]
    GameObject SaveReportPool;

    // Start is called before the first frame update
    void Start()
    {
        SaveReportPool = GameObject.Find("Save_Report_Pool");

        MakeReportBox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MakeReportBox()
    {
        string file = File.ReadAllText(Application.dataPath + "/data.json");
        var fileToJson = JsonConvert.DeserializeObject<List<ReportData>>(file);

        for (int i = 0; i < fileToJson.Count; ++i)
        {
            // Report Box List
            var newReportBox = Instantiate(ReportBoxPrefab) as GameObject;

            newReportBox.transform.SetParent(ReportContent.transform, false);
            newReportBox.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

            newReportBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fileToJson[i].weekend;
            newReportBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = fileToJson[i].url;
            newReportBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = fileToJson[i].date;

            // Save Report Layer
            var newSaveReportLayer = Instantiate(SaveReportLayerPrefab) as GameObject;

            newSaveReportLayer.transform.SetParent(SaveReportPool.transform, false);
            newSaveReportLayer.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

            newSaveReportLayer.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = fileToJson[i].name;
            newSaveReportLayer.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = fileToJson[i].team;
            newSaveReportLayer.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = fileToJson[i].weekend;
            newSaveReportLayer.transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>().text = fileToJson[i].date;
            newSaveReportLayer.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = fileToJson[i].url;

            int _i = i;
            newReportBox.transform.GetComponent<Button>().onClick.AddListener(delegate() { OnClick(_i); });
        }
    }
    public void OnClick(int num)
    {
        SaveReportPool.transform.GetChild(num).gameObject.SetActive(true);
        Debug.Log(num);
    }
}

