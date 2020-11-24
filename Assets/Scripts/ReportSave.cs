using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

public class ReportData
{
    public string name;
    public string team;
    public string weekend;
    public string url;
    public string date;
}
