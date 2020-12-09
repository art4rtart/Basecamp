using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SaveReportRead : MonoBehaviour
{
    GameObject SaveReportPool;

    int ReportCnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SaveReportPool =  GameObject.Find("Save_Report_Pool");

        ReportCnt = SaveReportPool.transform.childCount;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
