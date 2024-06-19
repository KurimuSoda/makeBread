using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBLE_TestScript : MonoBehaviour
{
    //private UnityCoreBluetooth.CoreBluetoothManager blemanager;
    private bool flag = false;
    private byte[] value;

    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        var manager = UnityCoreBluetooth.CoreBluetoothManager.Shared;
        manager.OnUpdateState((string state) =>
        {
            Debug.Log("State: " + state);
            if (state != "poweredOn") return;
            manager.StartScan();
        });

        manager.OnDiscoverPeripheral((UnityCoreBluetooth.CoreBluetoothPeripheral peripheral) =>
        {
            if (peripheral.name != "")
                Debug.Log("discover peripheral name: " + peripheral.name);
            if (peripheral.name != "M5StickCPlus2") return;

            manager.StopScan();
            manager.ConnectToPeripheral(peripheral);
        });

        manager.OnConnectPeripheral((UnityCoreBluetooth.CoreBluetoothPeripheral peripheral) =>
        {
            Debug.Log("connected peripheral name: " + peripheral.name);
            peripheral.discoverServices();
        });

        manager.OnDiscoverService((UnityCoreBluetooth.CoreBluetoothService service) =>
        {
            Debug.Log("discover service uuid: " + service.uuid);
            if (service.uuid != "bbf12a89-8d76-4943-b50e-762867897c5d") return;
            service.discoverCharacteristics();
        });

        manager.OnDiscoverCharacteristic((UnityCoreBluetooth.CoreBluetoothCharacteristic characterisitic) =>
        {
            string uuid = characterisitic.Uuid;
            string usage = characterisitic.Propertis[0];
            Debug.Log("discover characteristic uuid: " + uuid + ", usage: " + usage);
            if (usage != "notify") return;
            characterisitic.SetNotifyValue(true);
        });

        manager.OnUpdateValue((UnityCoreBluetooth.CoreBluetoothCharacteristic characteristic, byte[] data) =>
        {
            this.value = data;
            this.flag = true;
        });

        manager.Start();



    }

    public void Write(UnityCoreBluetooth.CoreBluetoothCharacteristic characteristic)
    {
        characteristic.Write(System.Text.Encoding.UTF8.GetBytes($"{counter}"));
        counter++;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
