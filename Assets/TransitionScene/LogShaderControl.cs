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
		mMat.SetFloat("_CanvasPositionY", mRectTrans.anchoredPosition.y);
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

	public void UpdateLogPos(float pos)
	{
		mRectTrans.anchoredPosition = new Vector2(mRectTrans.anchoredPosition.x, pos);
		mMat.SetFloat("_CanvasPositionY", mRectTrans.anchoredPosition.y);
	}

	public float GetLogPos()
	{
		return mMat.GetFloat("_CanvasPositionY");

	}
}
