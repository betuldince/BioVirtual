using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using DG.Tweening;

public class GardenConnection : MonoBehaviour
{
    [SerializeField] AudioSource birdSound;
    [SerializeField] List<GameObject> plantsParents;
    [SerializeField] ProgressBar progressBar;

    public float y;

    //SerialPort arduino = new SerialPort("COM3", 115200); windows
    public String port = "/dev/cu.usbserial-140";
    SerialPort arduino = new SerialPort("/dev/cu.usbserial-140", 115200);

    public float calibTimer = 0;
    public float breathTimer = 0;
    public float CALIB_TIME = 1f;

    public bool isPressed = false;
  
    public int currentPlantLayer = 0;
    public float mapped;
    public float prevMapped;
    public int countSample = 0;
    public int NEXT_LAYER_TIME = 15;

    public bool isCalib = false;
    public float mean = 0f;
    public float standardDev = 0f;
    public float movingAverage = 0f;
    public float alpha = 0.003f;
    public float epsilon = 2f;

    public float scaleDuration = 1.5f;

    public List<float> higherScales = new List<float>();
    

    private List<float> dataList = new List<float>();
    


    void Start()
    {
        arduino.Open();
        InvokeRepeating("Serial_Data", 1f, 0.01f);
        LowerPlantScales();
        progressBar.slider.maxValue = NEXT_LAYER_TIME;
    }
  
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
        
        if (isPressed && calibTimer < CALIB_TIME) {
            calibTimer += Time.deltaTime;
            countSample++;

            float val = Serial_Data();
            dataList.Add(val);

            return;
        }

        
        mapped = Serial_Data();
        dataList.Add(mapped);
        prevMapped = dataList[0];
        dataList.RemoveAt(0);

        //if (mapped > prevMapped * 1.008)
        if (mapped > prevMapped + epsilon)
        {
            breathTimer += Time.deltaTime;
            progressBar.ProgressSlider(Time.deltaTime);
           
        }
         
 
        if (breathTimer > NEXT_LAYER_TIME)
        {
            GameObject parent = plantsParents[currentPlantLayer++];
            parent.SetActive(true);
            foreach (Transform child in parent.transform)
            {
                progressBar.ResetSlider();
                float scale = higherScales[0];
                higherScales.RemoveAt(0);
                child.gameObject.SetActive(true);
                child.DOScale(scale, scaleDuration);
                
            }
            breathTimer = 0;
        }
 

    }

    private void LowerPlantScales()
    {
        foreach(GameObject parent in plantsParents)
        { 
            foreach (Transform child in parent.transform)
            {
                higherScales.Add(child.localScale.x);
                child.DOScale(0.1f, 0.001f);
            }
        }

    }

    private void Calib()
    {
        if (!isCalib)
        {
            mean = meanValue(dataList);
            standardDev = calculateSD(dataList, mean);
            isCalib = true;
        }
    }
   
    private float Serial_Data()
    {
        y = float.Parse(arduino.ReadLine());
        return y;
    }

    private float calculateSD(List<float> data, float mean)
    {
        int L = data.Count;
        float SD = 0.0f;

        for (int i = 0; i < L; ++i)
        {
            SD += (data[i] - mean)*(data[i] - mean);
        }
        return (float) Math.Sqrt(SD / L);
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


    private void OnApplicationQuit()
    {
        arduino.Close();
    }

}
