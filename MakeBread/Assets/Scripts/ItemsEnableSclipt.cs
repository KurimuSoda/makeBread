using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEnableSclipt : MonoBehaviour
{
    //アイテム選択画面の時にImageの親オブジェクトを取得するフラグを立てる専用
    private void OnEnable()
    {
        GameMG_new.isItemsObjExit = true;
        
    }
}
