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
    void Awake()
    {
        mImage = GetComponent<Image>();
        mRectTrans = GetComponent<RectTransform>();
        mMat = Instantiate(mImage.material);
        mImage.material = mMat;
    }

	private void Start()
	{
		mMat.SetFloat("_CanvasPositionY", mRectTrans.anchoredPosition.y);
	}

	// Update is called once per frame
	void Update()
	{
		mMat.SetFloat("_CanvasPositionY", mRectTrans.anchoredPosition.y);
	}


	public void SetLogColor(float _color)
	{
		mMat.SetFloat("_CanvasPositionY", _color);
	}

	public float GetLogColor()
	{
		return mMat.GetFloat("_CanvasPositionY");
	}
}
