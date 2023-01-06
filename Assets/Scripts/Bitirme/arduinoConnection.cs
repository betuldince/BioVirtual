using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class arduinoConnection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject herd;
    [SerializeField] AudioSource whaleSound;
    [SerializeField] List<GameObject> fishes;
    [SerializeField] Rigidbody ball;
    public string datas;
    public int intDatas;
    public float y;
    
    //SerialPort arduino = new SerialPort("COM3", 115200);
    SerialPort arduino = new SerialPort("COM4", 115200);

    private float MAP_TO_UPPER = 32f; //30
    private float MAP_TO_LOWER = -24f; //-30

    public float USER_MIN = 500f;
    public float USER_MAX = -1f;

    private float THRESHOLD = 15f;

    public float timer = 0;

    public bool isPressed = false;
    public float difmin;
    public float diffmax;

    public bool isMaxed=false;

    public int counter = 0;
    public int currentFish = 0;
    public float mapped;
    public int countSample = 0;
    public float sum = 0;
    public float mean = 0f;
    

    public float standardDev = 0f;
    public float minCoef = 2f;
    public float maxCoef = 2f;
    public float forceCoef = 1f;

    private List<float> data_list = new List<float>();

    void Start()
    {
        arduino.Open();
        InvokeRepeating("Serial_Data", 1f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        //datas = arduino.ReadLine();
        //Debug.Log(datas);

        if (Input.GetKeyDown("space"))
        {
            isPressed = true;
        }

        if (!isPressed)
        {
            return;
        }
        
        if (isPressed && timer < 10f) {
            timer += Time.deltaTime;
            countSample++;

            

            float val = Serial_Data();
            data_list.Add(val);

            /*
            sum += val;
            if (val > USER_MAX)
            {
                USER_MAX = val;

            }
            if (val < USER_MIN)
            {
                USER_MIN = val;
            }
            return;*/
            return;
        }
        mean = meanValue(data_list);
        standardDev = calculateSD(data_list, mean);
        USER_MIN = mean -  2 * standardDev;
        USER_MAX = mean + 2 * standardDev;


        mapped = Map(Serial_Data());

        difmin = mapped + THRESHOLD;


        //if (!isMaxed && (mapped - MAP_TO_UPPER > THRESHOLD))
        if (!isMaxed && (mapped > Map(mean + standardDev)))
        {
            Debug.Log("isMaxed");

            counter++;
            isMaxed = true;

        }//else if (difmin < MAP_TO_LOWER)
        else if (mapped < Map(mean - standardDev))
        {
            Debug.Log("isMin");

            isMaxed = false;
        }
        if (counter > 2)
        {
            Debug.Log("Succeed");
            whaleSound.Play();
            counter = 0;
            fishes[currentFish++].SetActive(true);
        }
        
        herd.transform.position = new Vector3(herd.transform.position.x, mapped, herd.transform.position.z);

         
    }

   

    private void OnApplicationQuit()
    {
        arduino.Close();
    }

    private float Serial_Data()
    {
        datas = arduino.ReadLine();
        y = float.Parse(datas);
        return y;
    }

    public float Map(float value)
    {

        //return (MAP_TO_UPPER - MAP_TO_LOWER) / (USER_MAX - USER_MIN) * (value - USER_MIN) + MAP_TO_LOWER;
        if (value > USER_MAX)
        {
            value = USER_MAX;

        }else if (value < USER_MIN)
        {
            value = USER_MIN;
        }

        return 2.5f*(MAP_TO_LOWER + (value - USER_MIN) * (MAP_TO_UPPER - MAP_TO_LOWER) /(1.75f* (USER_MAX - USER_MIN)));
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
}
