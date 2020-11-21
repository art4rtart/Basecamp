using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VirtualKey : MonoBehaviour {

    static public VirtualKeyboard _Keybord = null;
    public enum kType { kCharacter, kOther, kReturn, kSpace, kBackspace, kShift, kTab, kCapsLock, kHangul, KHide, kNum}
    public char KeyCharacter;
    public kType KeyType = kType.kCharacter;

	Text keyText;

    private bool mKeepPresed;
    public bool KeepPressed
    {
        set { mKeepPresed = value; }
        get { return mKeepPresed; }
    }

	private void Awake()
	{
		keyText = this.gameObject.transform.GetChild(0).GetComponent<Text>();
		if(!keyText) originText = keyText.text;
	}

	// Use this for initialization
	void Start () {
        UnityEngine.UI.Button _button = gameObject.GetComponent<UnityEngine.UI.Button>();
        if(_button != null)
        {
            _button.onClick.AddListener(onKeyClick);
        }
    }

    void onKeyClick()
    {
        //VirtualKeyboard _keybord = GameObject.FindObjectOfType< VirtualKeyboard>();
        if(_Keybord != null)
        {
            _Keybord.KeyDown(this);
        }
    }

    // Update is called once per frame
    void Update () {

	    if(KeepPressed)
        {
            //do something
        }
	}

	public string targetText;
	public string originText;

	public void ChangeText(string _text)
	{
		if (!keyText) return;
		keyText.text = _text;
	}

	bool isPressed;
	public string[] targetTexts;
	public void ChangeText()
	{
		targetText = targetTexts[(int)VirtualKeyboard.Instance.KeyType];
		if (!keyText) return;
		isPressed = !isPressed;
		if (isPressed) keyText.text = targetText;
		else keyText.text = originText;
	}
}
