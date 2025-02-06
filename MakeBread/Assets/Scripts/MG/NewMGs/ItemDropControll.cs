using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropControll : MonoBehaviour
{
    [Tooltip("ドロップアイテムの生成親obj"),SerializeField]
    private GameObject _dropItemsParent;

    [Tooltip("ドロップアイテムのベースobj"),SerializeField]
    private GameObject _dropItemObj;

    private string _itemname_ID;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //DropedItem();
        }
    }

    public void DropedItem(string itemname_ID, int itemnum)
    {
        GameObject obj = Instantiate(_dropItemObj, _dropItemsParent.transform);
        DropItemSpriteChange disCange = obj.GetComponentInChildren<DropItemSpriteChange>();
        disCange.itemID = itemname_ID;
        obj.tag = "Item" + itemnum;
        obj.name = itemname_ID;
        Debug.Log("Drop!!");
        //_dropItemObj.sprite = Resources.Load<Sprite>("Images/" + itemname_ID);
    }
}
