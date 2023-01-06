using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoManager : MonoBehaviour
{
    [SerializeField] public string PortPath;

    public float ReadValue { get; set; }

    //SerialPort arduino = new SerialPort("COM3", 115200); windows
    // /dev/cu.usbserial-140; mac
    private SerialPort _arduino;

    void Start()
    {
        _arduino = new SerialPort(PortPath, 115200);
        _arduino.Open();
        InvokeRepeating("ReadData", 1f, 0.01f);
    }
    
    private void ReadData()
    {
        ReadValue = float.Parse(_arduino.ReadLine());
    }

    private void OnApplicationQuit()
    {
        _arduino.Close();
    }
}