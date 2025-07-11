using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

//ItemSelectSceneに設置する
public class ItemSelectMG : MonoBehaviour
{
    private GameMG_new _gameMG;
    [SerializeField] private IconColorChange _iconColorChange;
    [SerializeField] private static int _randomBaseItem = 0;

    [SerializeField] private GameObject _howToImage;    //操作説明表示用オブジェクト
    [SerializeField] private GameObject _popUpObj;      //ポップアップ用オブジェクト
    [SerializeField] private Image _popUpItemImg;

    /// <summary>
    /// アイテムの名前を表示するためのオブジェクト
    /// </summary>
    [Tooltip("アイテムの名前を表示するためのオブジェクト"),SerializeField]
    private TextMeshProUGUI _itemNameTx;

    /// <summary>
    /// アイテム説明文を表示するためのオブジェクト
    /// </summary>
    [Tooltip("アイテム説明文を表示するためのオブジェクト"),SerializeField]
    private TextMeshProUGUI _itemTextTx;

    /// <summary>
    /// アイテム選択で決定を押した時の音
    /// </summary>
    [Tooltip("アイテム決定音"), SerializeField]
    private AudioClip _itemSetSound;

    [Tooltip(""), SerializeField]
    private AudioSource _audioSource;

    [SerializeField] private RectTransform _animRectTrans;
    [SerializeField] private float _popAnimValue = 2.0f;
    private float _popAnimTime = 1.0f;

    public static bool IsShaked = false;
    public static bool IsButtonAPrs = false;
    public static int PopUpItemArrNum = -1;
    public static string PopUpItemID = "";

    /// <summary>
    /// HoutoImageが表示されている間true
    /// </summary>
    public bool isHouwtoOn = true;

    /// <summary>
    /// popUpItemの配列番号
    /// </summary>
    private int _popUpItemNum = 0;
    private string _popUpItemID = "";

    private KeyCode[] _numbersKeyMore  = new KeyCode[]
    {
        KeyCode.Alpha1,KeyCode.Alpha2,
        KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,
        KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,
        KeyCode.Alpha0
    };

    /*
    private int _countImput = 0;

    private int _inputUPLimit = 4;
    private int _inputLowerLimit = 0;
    */

    // Start is called before the first frame update
    void Start()
    {
        _gameMG = GameObject.Find("Manager").GetComponent<GameMG_new>();
        ItemSelectMGInit();

        _popUpObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isHouwtoOn == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || IsShaked == true)
            {
                _howToImage.SetActive(false);
                isHouwtoOn = false;
                return;

            }
            return;
        }

        
        if (Input.GetKeyDown(KeyCode.Return) || IsShaked == true)
        {
            IsShaked = false;
            if (_gameMG._countImput < 1) return;

            _gameMG.ItemSelectFinish();
            
        }
        if (Input.GetKeyDown(KeyCode.I) || IsButtonAPrs == true)
        {
            IsButtonAPrs = false;
            SetItemANDPopOff();
        }

        if (Input.anyKeyDown)
        {
            for (int i = 0; i < _numbersKeyMore.Length; i++)
            {
                if (Input.GetKeyDown(_numbersKeyMore[i]))
                {
                    //_countImput++;
                    //_countImput = Mathf.Clamp(_countImput, _inputLowerLimit, _inputUPLimit);
                    //_gameMG._countImput = _countImput;
                    //_gameMG.SetBread(i);
                    PopOnUseArrayNum(i);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _gameMG.RemoveBread();
        }
    }

    private void FixedUpdate()
    {
        if(PopUpItemArrNum != -1)
        {
            _popUpItemNum = PopUpItemArrNum;
            _popUpItemID = PopUpItemID;
            //PopOnUseArrayNum(_popUpItemNum);
            PopOnUseID(_popUpItemID);
            PopUpItemArrNum = -1;
            PopUpItemID = "";
        }
    }

    public void ItemSelectMGInit()
    {
        _randomBaseItem = 0;
        IsShaked = false;
        IsButtonAPrs = false;
        PopUpItemArrNum = -1;
        PopUpItemID = "";

        Random.InitState(System.DateTime.Now.Millisecond);
    }

    /// <summary>
    /// 入力した回数(選んだアイテム)の数を受け取って、その中からランダムに抽出した値をGMに渡す
    /// </summary>
    /// <param name="imputCount">入力した回数(選んだアイテム)の数</param>
    public static void RandomItemChose(int inputCount)
    {
        _randomBaseItem = 0;
        _randomBaseItem = Random.Range(0, inputCount + 1);
        GameMG_new.RandomItem = _randomBaseItem;
        Debug.Log("Random ItemNumber is ---> " + _randomBaseItem + ", inputCount is --> " + inputCount);
    }

    /// <summary>
    /// NFC
    /// </summary>
    /// <param name="itemname_ID"></param>
    public void PopOnUseID(string itemname_ID)
    {
        _popUpObj.SetActive(true);
        _popUpItemImg.sprite = Resources.Load<Sprite>("Images/" + itemname_ID);

        //味の情報をGM経由でデータから取り出す
        int taste = _gameMG.ArrayTOTaste(_popUpItemNum);
        IconColor(taste);

        //アイテムの名前をGM経由でデータから取り出す
        string itemName = _gameMG.ArrayTOName(_popUpItemNum);
        _itemNameTx.text = itemName;

        //アイテム説明文をGM経由でデータから取り出す
        string itemText = _gameMG.ArrayTOItemtext(_popUpItemNum);
        _itemTextTx.text = itemText;

        _popUpItemID = "";
    }

    /// <summary>
    /// KeyBord
    /// </summary>
    /// <param name="itemArrayNum"></param>
    public void PopOnUseArrayNum(int itemArrayNum)
    {
        _popUpObj.SetActive(true);
        //PopUpAnim();
        _popUpItemNum = itemArrayNum;
        _popUpItemID = _gameMG.ArrayNumTOItemID(itemArrayNum);
        _popUpItemImg.sprite = Resources.Load<Sprite>("Images/" + _popUpItemID);

        //味の情報をGM経由でデータから取り出す
        int taste = _gameMG.ArrayTOTaste(itemArrayNum);
        IconColor(taste);

        //アイテムの名前をGM経由でデータから取り出す
        string itemName = _gameMG.ArrayTOName(itemArrayNum);
        _itemNameTx.text = itemName;

        //アイテム説明文をGM経由でデータから取り出す
        string itemText = _gameMG.ArrayTOItemtext(_popUpItemNum);
        _itemTextTx.text = itemText;


        _popUpItemID = "";
    }

    public void SetItemANDPopOff()
    {
        _gameMG.SetBread(_popUpItemNum);
        _popUpItemNum = 0;
        _popUpObj.SetActive(false);
        _audioSource.PlayOneShot(_itemSetSound);
    }

    private void IconColor(int taste)
    {
        switch (taste)
        {
            case 1:
                _iconColorChange.SweetIconOnry();
                break;
            case 2:
                _iconColorChange.SpicyIconOnry();
                break;
            case 3:
                _iconColorChange.SourIconOnry();
                break;
            case 4:
                _iconColorChange.SaltIconOnry();
                break;
        }
    }

    private void PopUpAnim()
    {
        _animRectTrans.transform.DOScale(_popAnimValue, _popAnimTime)
            .SetRelative(true)
            .SetLoops(1, LoopType.Yoyo);
    }
    
}
