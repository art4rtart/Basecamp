﻿using UnityEngine;
using System.Collections;

public class VirtualTextInputBox : MonoBehaviour {
	public static VirtualTextInputBox Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<VirtualTextInputBox>();
			return instance;
		}
	}
	private static VirtualTextInputBox instance;

	AutomateKR		mAutomateKR = new AutomateKR();
    public UnityEngine.UI.InputField mTextField = null;
    public string TextField
    {
        set
        {
            if (mTextField != null)
            {
                mTextField.text = value;
            }
        }
        get
        {
            if (mTextField != null)
            {
                return mTextField.text;
            }
            return "";
        }
    }

    void Start () {

	}
	
	void Update () {

	}

    public void Clear()
    {
        mAutomateKR.Clear();

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }


    public void KeyDownHangul(char _key)
    {
        mAutomateKR.SetKeyCode(_key);

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }

    public void KeyDown(char _key)
    {
        mAutomateKR.SetKeyString(_key);

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }

    public void KeyDown(VirtualKey _key)
    {
        switch(_key.KeyType)
        {
            case VirtualKey.kType.kBackspace:
                {
                    mAutomateKR.SetKeyCode(AutomateKR.KEY_CODE_BACKSPACE);
                }
                break;
            case VirtualKey.kType.kSpace:
                {
                    mAutomateKR.SetKeyCode(AutomateKR.KEY_CODE_SPACE);
                }
                break;
        }

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }
}
