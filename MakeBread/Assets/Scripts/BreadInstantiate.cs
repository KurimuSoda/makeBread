using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _baseObj;
    private int _inputCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonBreadObj(string data_id)
    {
        GameObject obj = Instantiate(_baseObj, new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
        //GameObject textObj = obj.transform.GetChild(1).gameObject;
        var itemImage = obj.GetComponentInChildren<ItemImageController>();
        itemImage.num = _inputCount + 1;
        itemImage.itemname_ID = data_id;
        obj.name = data_id;
        _inputCount++;
    }
}
