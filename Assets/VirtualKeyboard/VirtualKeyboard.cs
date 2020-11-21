using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VirtualKeyboard : MonoBehaviour {
	public static VirtualKeyboard Instance
	{
		get
		{
			if (instance != null) return instance;
			instance = FindObjectOfType<VirtualKeyboard>();
			return instance;
		}
	}
	private static VirtualKeyboard instance;

	public VirtualTextInputBox TextInputBox = null;
	public string typedLetters;
	protected enum kLanguage { kKorean, kEnglish};
    protected bool mPressShift = false;
    protected kLanguage mLanguage = kLanguage.kKorean;
    protected Dictionary<char, char> CHARACTER_TABLE = new Dictionary<char, char>
    {
        {'1', '!'}, {'2', '@'}, {'3', '#'}, {'4', '$'}, {'5', '%'},{'6', '^'}, {'7', '&'}, {'8', '*'}, {'9', '('},{'0', ')'},
        { '`', '~'},   {'-', '_'}, {'=', '+'}, {'[', '{'}, {']', '}'}, {'\\', '|'}, {',', '<'}, {'.', '>'}, {'/', '?'}
    };

	public enum KeyboardType { None = -1, KR, ENG, Num }
	public KeyboardType KeyType = KeyboardType.KR;

	Animator animator;
	public InputField inputField;

	// kr, eng, num
	public GameObject[] keyboardTypes;
	int previousKeyboardIndex;

	void Awake()
    {
        VirtualKey._Keybord = this;
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		//if (inputField != null) { inputField.text = VirtualTextInputBox.Instance.TextField; }
	}

    public void Clear()
    {
        if(TextInputBox != null)
        {
            TextInputBox.Clear();
        }
    }

    public void KeyDown(VirtualKey _key)
    {
        if(TextInputBox != null)
        {
            switch(_key.KeyType)
            {
                case VirtualKey.kType.kShift:
                    {
                        mPressShift = true;
                    }
                    break;
                case VirtualKey.kType.kHangul:
                    {
						if (mLanguage == kLanguage.kKorean)
						{
							mLanguage = kLanguage.kEnglish;
							keyboardTypes[(int)KeyType].SetActive(false);
							KeyType = KeyboardType.ENG;
							keyboardTypes[(int)KeyType].SetActive(true);
						}
						else
						{
							mLanguage = kLanguage.kKorean;
							keyboardTypes[(int)KeyType].SetActive(false);
							KeyType = KeyboardType.KR;
							keyboardTypes[(int)KeyType].SetActive(true);
						}
                    }
                    break;
                case VirtualKey.kType.kSpace:
                case VirtualKey.kType.kBackspace:
                    {
                        TextInputBox.KeyDown(_key);
                    }
                    break;
                case VirtualKey.kType.kReturn:
                    {
						typedLetters = VirtualTextInputBox.Instance.TextField;
						VirtualTextInputBox.Instance.TextField = null;
						VirtualTextInputBox.Instance.Clear();
						//do somehing
					}
                    break;
                case VirtualKey.kType.kCharacter:
                    {
                        char keyCharacter = _key.KeyCharacter;
                        if (mPressShift)
                        {
                            keyCharacter = char.ToUpper(keyCharacter);
                            mPressShift = false;
                        }

                        if (mLanguage == kLanguage.kKorean)
                        {
                            TextInputBox.KeyDownHangul(keyCharacter);
                        }
                        else if (mLanguage == kLanguage.kEnglish)
                        {
                            TextInputBox.KeyDown(keyCharacter);
                        }
                    }
                    break;
                case VirtualKey.kType.kOther:
                    {
                        char keyCharacter = _key.KeyCharacter;
                        if (mPressShift)
                        {
                            keyCharacter = CHARACTER_TABLE[keyCharacter];
                            mPressShift = false;
                        }
                        TextInputBox.KeyDown(keyCharacter);
                    }
                    break;

				case VirtualKey.kType.KHide:

					// name

					Debug.Log(Backpack.Instance.askItemIndex);

					if(Backpack.Instance.askItemIndex == 0)
					{
						UserManager.Instance.name = inputField.text;
						Backpack.Instance.nameCardText.text = UserManager.Instance.name;
					}

					else if(Backpack.Instance.askItemIndex == 1)
					{
						UserManager.Instance.age = int.Parse(inputField.text);
						Backpack.Instance.ageCardText.text = UserManager.Instance.age.ToString();
					}

					else if(Backpack.Instance.askItemIndex == 2)
					{
						UserManager.Instance.studentNum = int.Parse(inputField.text);
						Backpack.Instance.studentNumCardText.text = UserManager.Instance.studentNum.ToString();
					}

					//if (!Backpack.Instance.studentNumChecked)
					//{
					//	UserManager.Instance.studentNum = int.Parse(inputField.text);
					//	Backpack.Instance.studentNumCardText.text = UserManager.Instance.studentNum.ToString();
					//}

					//// student num
					//UserManager.Instance.studentNum = int.Parse(inputField.text);
					//Backpack.Instance.studentNumCardText.text = UserManager.Instance.studentNum.ToString();

					KeyboardApper(false, (int)KeyType);
					VirtualTextInputBox.Instance.TextField = null;
					VirtualTextInputBox.Instance.Clear();
					break;

				case VirtualKey.kType.kNum:
					if(KeyType != KeyboardType.Num) previousKeyboardIndex = (int)KeyType;
					keyboardTypes[(int)KeyType].SetActive(false);
					if (KeyType == KeyboardType.Num) KeyType = (KeyboardType)previousKeyboardIndex;
					else KeyType = KeyboardType.Num;
					keyboardTypes[(int)KeyType].SetActive(true);
					break;
            }

			if (inputField != null) { inputField.text = VirtualTextInputBox.Instance.TextField; }
		}
    }

	public bool keyboardAppearStatus;
	public void KeyboardApper(bool _show, int _keyboardType)
	{
		animator.SetBool("Show", _show);
		keyboardAppearStatus = _show;

		for(int i = 0; i < keyboardTypes.Length; i++)
		{
			keyboardTypes[i].SetActive(false);
		}

		keyboardTypes[_keyboardType].SetActive(true);
	}
}
