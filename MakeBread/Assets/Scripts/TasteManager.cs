using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasteManager : MonoBehaviour
{
    private int _mainTaste = 0;
    private int[] _tasteArray = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _mainTaste = _tasteArray[3];
            //Debug.Log(_mainTaste);
            /*
            for(int i = 0; i < 4; i++)
            {
                Debug.Log(_tasteArray[i]);
            }
            */
        }
    }

    public void PutInTastArray(int inputNumber,int data_taste)
    {
        _tasteArray[inputNumber] = data_taste;
        
    }
}
