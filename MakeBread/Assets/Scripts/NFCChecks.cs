using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// NFC読み取りの可否状態を列挙。
/// </summary>
public enum ReadStatus
{
    /// <summary>
    /// NFCを読み込まないで欲しい時
    /// </summary>
    StopRead,

    /// <summary>
    /// NFCを読み込んでも良い時
    /// </summary>
    ReadOK,

    /// <summary>
    /// 読み込みを終わる時
    /// </summary>
    Off

}

public class NFCChecks: MonoBehaviour
{
    /// <summary>
    /// 新しく読み取ったUID
    /// </summary>
    private string _currentUid;

    [SerializeField] private BreadDate breadData;
    private int _itemLength = 0;

    public ReadStatus readStatus = ReadStatus.ReadOK;


    // Sceneが開始された時に一度だけ呼び出される関数。オブジェクトのアクティブ状態に関わらず行われる。
    private void Awake()
    {
        ItemDataLengthCount();

    }

    /// <summary>
    /// UIDを受け取って対応する配列番号(int型)を返す関数
    /// </summary>
    /// <param name="uid">NFCタグから読み取ったUIDを渡す</param>
    /// <returns>対応する配列番号(int型)</returns>    
    public int UIDtoArrayNo(string uid)
    {
        int hitNumber = 0;
        int arrayNo;

        for (int i = 0; i < _itemLength; i++)
        {
            if (uid == breadData.Bread_date[i].uid)
            {
                hitNumber = i;
                break;
            }
        }
        arrayNo = hitNumber;

        return arrayNo;
    }

    /// <summary>
    /// UIDを受け取って対応するID(string型)を返す関数
    /// </summary>
    /// <param name="uid">NFCタグから読み取ったUIDを渡す</param>
    /// <returns>対応するID(string型)</returns>    
    public string UIDtoItemID(string uid)
    {
        int hitNumber = 0;
        string itemID;

        for (int i = 0; i < _itemLength; i++)
        {
            if (uid == breadData.Bread_date[i].uid)
            {
                hitNumber = i;
                break;
                //Debug.Log("hitNumber = " + hitNumber);
            }
        }
        itemID = breadData.Bread_date[hitNumber].id;

        return itemID;
    }

    /// <summary>
    /// Bread_dateに入っている素材の数を調べる
    /// </summary>
    private void ItemDataLengthCount()
    {
        foreach (BreadDataEntity datacount in breadData.Bread_date)
        {
            _itemLength++;
        }
        Debug.Log("ItemLength :" + _itemLength);
    }

}

