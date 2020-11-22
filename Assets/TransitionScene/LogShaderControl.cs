using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogShaderControl : MonoBehaviour
{

    Image mImage;
    RectTransform mRectTrans;
    Material mMat;
    // Start is called before the first frame update
    void Start()
    {
        mImage = GetComponent<Image>();
        mRectTrans = GetComponent<RectTransform>();
        mMat = Instantiate(mImage.material);
        mImage.material = mMat;
    }

    // Update is called once per frame
    void Update()
    {
        mMat.SetFloat("_CanvasPositionY", mRectTrans.anchoredPosition.y);
        
    }
}
