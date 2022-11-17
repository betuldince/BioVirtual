using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class arduinoConnection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ball;
    public string datas;
    public int intDatas;
    SerialPort arduino = new SerialPort("COM3", 115200);
    void Start()
    {
        arduino.Open();
        InvokeRepeating("Serial_Data", 0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        //datas = arduino.ReadLine();
        //Debug.Log(datas);

        ball.transform.position = new Vector3(ball.transform.position.x, Serial_Data(), ball.transform.position.z);
       
    }
    private void OnApplicationQuit()
    {
        arduino.Close();
    }

    float Serial_Data()
    {
        datas = arduino.ReadLine();
        return 3 + float.Parse(datas ) / 100;
        
    }
}
