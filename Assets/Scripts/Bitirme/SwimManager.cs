using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject herd;
    [SerializeField] ArduinoManager ArduinoManager;
    [SerializeField] List<GameObject> fishes;
    [SerializeField] ParticleSystem bubbles;

    public float y;

    private float MAP_TO_UPPER = 32f; //30
    private float MAP_TO_LOWER = -24f; //-30

    public float USER_MIN = 500f;
    public float USER_MAX = -1f;

    private float THRESHOLD = 15f;

    public float _pastTime = 0;

    public bool isPressed = false;
    public float difmin;
    public float diffmax;

    public bool isMaxed = false;

    public int counter = 0;
    public int currentFish = 0;
    public float mapped;
    public int countSample = 0;
    public float sum = 0;
    public float mean = 0f;

    public bool isCalib = false;

    public float standardDev = 0f;
    public float minCoef = 2f;
    public float maxCoef = 2f;
    public float forceCoef = 1f;

    private List<float> data_list = new List<float>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) { isPressed = true; }

        if (!isPressed) { return; }

        if (isPressed && _pastTime < 10f)
        {
            _pastTime += Time.deltaTime;
            countSample++;
            float val = ArduinoManager.ReadValue;
            data_list.Add(val);

            return;
        }

        if (!isCalib)
        {
            mean = Utils.CalculateMean(data_list);
            standardDev = Utils.CalculateStandardDeviation(data_list, mean);
            USER_MIN = mean - 2 * standardDev;
            USER_MAX = mean + 2 * standardDev;
            isCalib = true;
        }
       
        mapped = Map(ArduinoManager.ReadValue);

        //if (!isMaxed && (mapped - MAP_TO_UPPER > THRESHOLD))
        if (!isMaxed && (mapped > Map(mean + standardDev)))
        {
            Debug.Log("isMaxed");

            counter++;
            isMaxed = true;
        }
        else if (mapped < Map(mean - standardDev))
        {
            Debug.Log("isMin");

            isMaxed = false;
        }
        else
        {
            bubbles.Stop();
        }

        if (counter > 2)
        {
            Debug.Log("Succeed");
            counter = 0;
            fishes[currentFish].SetActive(true);
            currentFish++;

            if (!bubbles.isPlaying)
            {
                bubbles.Play();
            }
        }

        herd.transform.position = new Vector3(herd.transform.position.x, mapped, herd.transform.position.z);
    }
 
    public float Map(float value)
    {
        //return (MAP_TO_UPPER - MAP_TO_LOWER) / (USER_MAX - USER_MIN) * (value - USER_MIN) + MAP_TO_LOWER;
        if (value > USER_MAX)
        {
            value = USER_MAX;
        }
        else if (value < USER_MIN)
        {
            value = USER_MIN;
        }

        return 2.5f * (MAP_TO_LOWER + (value - USER_MIN) * (MAP_TO_UPPER - MAP_TO_LOWER) / (1.75f * (USER_MAX - USER_MIN)));
    }
}
