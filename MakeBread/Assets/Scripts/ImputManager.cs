using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputManager : MonoBehaviour
{
    [SerializeField] private BreadDate breadData;
    [SerializeField] private BreadInstantiate _breadinstantiate;
    private KeyCode[] _numbersKey = new KeyCode[]
    {
        KeyCode.Alpha1,KeyCode.Alpha2,
        KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,
        KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            for(int i = 0; i < _numbersKey.Length; i++)
            {
                if (Input.GetKeyDown(_numbersKey[i]))
                {
                    DebugLog(i);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        
    }

    private void DebugLog(int number)
    {
        Debug.Log(breadData.Bread_date[number].name);
        _breadinstantiate.SummonBreadObj(breadData.Bread_date[number].id);
    }
}
