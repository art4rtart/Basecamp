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

public class ReportSave : MonoBehaviour
{
    [SerializeField]
    GameObject NameInput;

    [SerializeField]
    GameObject TeamInput;

    [SerializeField]
    GameObject WeekendInput;

    [SerializeField]
    GameObject DateInput;

    [SerializeField]
    GameObject URLInput;

    [SerializeField]
    GameObject ReportBoxPrefab;

    [SerializeField]
    GameObject ReportContent;

    [SerializeField]
    GameObject SaveReportLayerPrefab;

    [SerializeField]
    GameObject SaveReportPool;

    [SerializeField]
    GameObject[] WorkList;

    // Start is called before the first frame update
    void Start()
    {
        DateInput.transform.GetComponent<TextMeshProUGUI>().text = DateTime.Now.ToString(("yyyy-MM-dd"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClick()
    {
        ReportData data = new ReportData();

        data.name = NameInput.transform.GetComponent<TMP_InputField>().text;
        data.team = TeamInput.transform.GetComponent<TMP_InputField>().text;
        data.weekend = WeekendInput.transform.GetComponent<TMP_InputField>().text;
        data.url = URLInput.transform.GetComponent<TMP_InputField>().text;
        data.date = DateTime.Now.ToString(("yyyy-MM-dd"));

        var newReportBox = Instantiate(ReportBoxPrefab);

        newReportBox.transform.SetParent(ReportContent.transform, false);
        newReportBox.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

        newReportBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.weekend;
        newReportBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = data.url;
        newReportBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = data.date;

        var newSaveReportLayer = Instantiate(SaveReportLayerPrefab) as GameObject;

        newSaveReportLayer.transform.SetParent(SaveReportPool.transform, false);
        newSaveReportLayer.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

        newSaveReportLayer.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = data.name;
        newSaveReportLayer.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = data.team;
        newSaveReportLayer.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = data.weekend;
        newSaveReportLayer.transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>().text = data.date;
        newSaveReportLayer.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = data.url;

        int _i = ReportContent.transform.childCount-1;
        Debug.Log(_i);
        newReportBox.transform.GetComponent<Button>().onClick.AddListener(delegate () { OnClick(_i); });

        if (File.Exists(Application.dataPath + "/data.json") == false)
        {
            string jsonString = JsonConvert.SerializeObject(data);

            jsonString = "[" + jsonString + "]";
            File.WriteAllText(Application.dataPath + "/data.json", jsonString);
        }
        else {
            string file = File.ReadAllText(Application.dataPath + "/data.json");
            
            var fileToJson = JsonConvert.DeserializeObject<List<ReportData>>(file);
            fileToJson.Add(data);

            string jsonString = JsonConvert.SerializeObject(fileToJson);
            
            File.WriteAllText(Application.dataPath + "/data.json", jsonString);
        }

        NameInput.transform.GetComponent<TMP_InputField>().text = null;
        TeamInput.transform.GetComponent<TMP_InputField>().text = null;
        WeekendInput.transform.GetComponent<TMP_InputField>().text = null;
        URLInput.transform.GetComponent<TMP_InputField>().text = null;
        DateInput.transform.GetComponent<TextMeshProUGUI>().text = null;
    }

    public void OnClick(int num)
    {
        SaveReportPool.transform.GetChild(num).gameObject.SetActive(true);
        Debug.Log(num);
    }
}

public class ReportData
{
    public string name;
    public string team;
    public string weekend;
    public string url;
    public string date;
}
