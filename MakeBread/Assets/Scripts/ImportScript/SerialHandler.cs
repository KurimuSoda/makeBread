using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    //M5StickCPlus2のBluetooth接続したときに確認したポート
    public string portName = "/dev/cu.M5StickCPlus2BT-ESP32SPP";
    
    public int baudRate    = 115200;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_) {
            OnDataReceived(message_);
        }
    }

    //シングルトンを使わず画面遷移をする場合、アタッチしているオブジェクトが破棄されるので一度この処理が走ってしまう
    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.Open();

        isRunning_ = true;
        GameMG_new.isM5Serial = true;   //

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Close()
    {
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive) {
            thread_.Join();
            Debug.Log("Close step1");
        }

        if (serialPort_ != null && serialPort_.IsOpen) {
            serialPort_.Close();
            serialPort_.Dispose();
            Debug.Log("Close step2");
        }
        Debug.Log("Port Close void run");
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
            try {
                // if (serialPort_.BytesToRead > 0) {
                    message_ = serialPort_.ReadLine();
                    isNewMessageReceived_ = true;
                // }
            } catch (System.Exception e) {
                //Debug.LogWarning(e.Message);      //一旦コメントアウトした　繋がらなかった時用の処理をここに書くといいかも?
                Debug.Log(e.Message);
                GameMG_new.isM5Serial = false;
            }
        }
    }

    public void Write(string message)
    {
        try {
            serialPort_.Write(message);
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    /// <summary>
    /// 現在の通信を終了し、新しく受け取ったポート名に接続を試みる
    /// </summary>
    /// <param name="newPortName">新しいポート名</param>
    public void ReOpen(string newPortName)
    {
        if(GameMG_new.isM5Serial == false)
        {
            portName = newPortName;
            Debug.Log("M5 not Connected");
            Open();
        }
        else if(GameMG_new.isM5Serial == true)
        {
            Close();
            Debug.Log("Finish M5 Connect");
            portName = newPortName;
            Open();
        }
        
    }
}