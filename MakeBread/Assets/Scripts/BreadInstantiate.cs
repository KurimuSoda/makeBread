using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TasteMG;

public class BreadInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _itemsParent;
    [SerializeField] private GameObject _baseObj;
    private TasteManager _tasteManager = new TasteManager();

    /// <summary>
    /// 入力された回数をカウント。番目。
    /// </summary>
    private int _inputCount = 0;

    /// <summary>
    /// 削除するアイテムの番号。番目。
    /// </summary>
    private int _deleteNum = 0;

    /// <summary>
    /// 選択したアイテム4つを記録するための配列。IDが記録されている。
    /// </summary>
    private string[] _setBreadDataIDArray = new string[4];

    /// <summary>
    /// エンターキーが押されたかどうか。
    /// </summary>
    private bool _isReturnCheck = false;

    /// <summary>
    /// 選択したアイテムの画像付きオブジェクトを生成する関数。
    /// </summary>
    /// <param name="data_id"></param>
    /// <param name="data_taste"></param>
    public void SummonBreadObj(string data_id, int data_taste)
    {
        if(_inputCount < 4)
        {
            GameObject obj = Instantiate(_baseObj, _itemsParent.transform);
            //GameObject textObj = obj.transform.GetChild(1).gameObject;
            var itemImage = obj.GetComponentInChildren<ItemImageController>();
            _setBreadDataIDArray[_inputCount] = data_id;

            itemImage.num = _inputCount + 1;
            itemImage.itemname_ID = data_id;
            _tasteManager.PutInTastArray(_inputCount, data_taste);
            obj.tag = "Item" + itemImage.num;
            obj.name = data_id;
            _inputCount++;
        }
        else
        {

        }
        
    }

    public void DeleteBreadObj()
    {
        if (_isReturnCheck) { return; }
        _deleteNum = _inputCount;
        GameObject missSet = GameObject.FindWithTag("Item" + _deleteNum);
        Destroy(missSet);
        _inputCount--;
        //Debug.Log("miss:" + missSet.name);
    }

    private bool BiterCheck()
    {
        
        string biterCheck;

        foreach(string breadID in _setBreadDataIDArray)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                biterCheck = _setBreadDataIDArray[i];
                if(breadID == biterCheck) { count++; }
                if(count >= 2) { return true; }
            }
        }

        return false;
    }

    public void SelectionFixing()
    {
        _itemsParent.SetActive(false);
            _isReturnCheck = true;
            Debug.Log("確定!");
            //Debug.Log("BiterCheck is : " + BiterCheck());
            if (BiterCheck())
            {
                Debug.Log("Will Be Biter......");
                _tasteManager.isBiter = true;
            }
    }

    public string ReturnFirstItemID()
    {
        string firstItemID = _setBreadDataIDArray[0];
        return firstItemID;
    }
}
