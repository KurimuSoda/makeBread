using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _baseObj;
    [SerializeField] private TasteManager _tasteManager;
    private int _inputCount = 0;
    private int _deleteNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonBreadObj(string data_id, int data_taste)
    {
        if(_inputCount < 4)
        {
            GameObject obj = Instantiate(_baseObj, new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
            //GameObject textObj = obj.transform.GetChild(1).gameObject;
            var itemImage = obj.GetComponentInChildren<ItemImageController>();
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
        _deleteNum = _inputCount;
        GameObject missSet = GameObject.FindWithTag("Item" + _deleteNum);
        Destroy(missSet);
        _inputCount--;
        //Debug.Log("miss:" + missSet.name);
    }
}
