using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class LightManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject lightObject;
    public Light light;
    public string datas;

    public float USER_MIN = 500f;
    public float USER_MAX = -1f;
    private float threshold;
    SerialPort arduino = new SerialPort("/dev/cu.usbserial-1140", 115200);

    private List<float> data_list = new List<float>();
    private int[] layers;
    private int currentLayer;
    public bool isPressed = false;
  
    public float afterValue;
    public float mean = 0f;
    public float timer = 0, breathTimer = 0;

    void Start()
    {
        arduino.Open();
        InvokeRepeating("Serial_Data", 1f, 0.01f);
        
        light = lightObject.GetComponent<Light>();
        light.spotAngle = 0;
        currentLayer = 0;
        layers = new int[] { 20, 40, 60, 80, 100 };

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isPressed = true;
        }

        if (!isPressed)
        {
            return;
        }

        if (isPressed && timer < 5f)
        {
            timer += Time.deltaTime;
            data_list.Add(Serial_Data());            
            return;
        }

       

        mean = meanValue(data_list);
        threshold = mean * 0.1f;
        float value = Serial_Data();
       

        if (breathTimer>2f)
        {
            breathTimer += Time.deltaTime;
        }
        else
        {
            breathTimer = 0;
        }
        afterValue = Serial_Data(); 


        if (value < afterValue)
        {
            Debug.Log("a");
            light.spotAngle += 0.04f;

        }
  

        if (light.spotAngle >= layers[currentLayer])
        {
            light.spotAngle = layers[currentLayer++];
        }
    }


    private float Serial_Data()
    {
        datas = arduino.ReadLine();
        return float.Parse(datas);
    }


    private float meanValue(List<float> data)
    {
        int L = data.Count;
        float sum = 0.0f;

        for (int i = 0; i < L; ++i)
        {
            sum += data[i];
        }

        return sum / L; ;

    }
}
