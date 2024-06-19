using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class BTSerialTest : MonoBehaviour
{
    public SerialHandler serialHandler;

    //受信用変数
    public float data;              //受信データのfloat型版変数
    string receive_data;            //受信した生データを入れる変数


    // Start is called before the first frame update
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string message)
    {
        /*
        receive_data = (message);           //受信データをreceive_dataに入れる
        data = float.Parse(receive_data);   //float型に変換してdataに入れる
        Debug.Log("受信データ: " + data);
        */

        var data = message.Split(
            new string[] { "\n" }, System.StringSplitOptions.None); //受信する
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            serialHandler.Write("Hello" + "\n");
        }
    }
}
