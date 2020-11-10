using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddReportScript : MonoBehaviour
{
    public GameObject Layer;
    public GameObject Pool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() 
    {
        GameObject WriteLayer = Instantiate(Layer, Pool.transform);

        WriteLayer.transform.parent = Pool.transform;
        
        WriteLayer.SetActive(true);
    }
}
