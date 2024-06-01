using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasteManager : MonoBehaviour
{
    private int _mainTaste = 0;
    /// <summary>選択した材料の味を選択した順番に格納する</summary>
    private int[] _tasteArray = new int[4];
    private string[] _tasteStringArray = new string[5] {"甘い", "辛い", "酸っぱい", "しょっぱい", "苦い"};
    private int[] _tastCountReArray = new int[4] { 0, 0, 0, 0 };

    public bool isBiter = false;

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
            Debug.Log("main taste is ---> " + _mainTaste + " : " + _tasteStringArray[_mainTaste - 1]);
            _tastCountReArray = TasteCheck();
            

        }
    }

    public void PutInTastArray(int inputNumber,int data_taste)
    {
        _tasteArray[inputNumber] = data_taste;
        
    }

    private int[] TasteCheck()
    {
        int sweet = 0, spice = 1, sour = 2, salty = 3;
        int[] tasteCheckArray = new int[4] { 0, 0, 0, 0 };

        for(int i = 0; i < 4; i++)
        {
            if (_tasteArray[i] == 1)
            {
                tasteCheckArray[sweet]++;
            }
            else if (_tasteArray[i] == 2)
            {
                tasteCheckArray[spice]++;
            }
            else if (_tasteArray[i] == 3)
            {
                tasteCheckArray[sour]++;
            }
            else if (_tasteArray[i] == 4)
            {
                tasteCheckArray[salty]++;
            }
            else { }


        }

        return tasteCheckArray;
    }
}
