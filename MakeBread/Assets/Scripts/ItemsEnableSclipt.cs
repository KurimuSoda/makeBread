using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEnableSclipt : MonoBehaviour
{
    private void OnEnable()
    {
        GameMG.isItemsObjExit = true;
    }
}
