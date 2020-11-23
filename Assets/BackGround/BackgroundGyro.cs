using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.CoreModule;

public class BackgroundGyro : MonoBehaviour
{

    bool mEnableGyro;
    Gyroscope mGyro;
    RectTransform mRect;
    Vector2 OriginPos;
    [SerializeField]
    float MaxDistance;
    [SerializeField]
    float DistanceValue;

    private void Awake() {
        mRect = GetComponent<RectTransform>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
        mEnableGyro = EnableGyro();
        OriginPos = mRect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(mEnableGyro)
        {
            mRect.anchoredPosition = OriginPos + new Vector2(mGyro.attitude.x, mGyro.attitude.y ) * MaxDistance * DistanceValue;
        }

        Debug.Log(mGyro.attitude.x);
    }

    private bool EnableGyro()
    {
       // /if(SystemInfo.supportsGyroscope)
        //{
            mGyro = Input.gyro;
            mGyro.enabled = true;
            return true;
        //}

       // return false;
    }

}
