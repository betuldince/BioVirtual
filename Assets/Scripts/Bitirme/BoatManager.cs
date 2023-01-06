using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatManager : MonoBehaviour
{
    [SerializeField] List<GameObject> plantsParents;
    [SerializeField] ArduinoManager ArduinoManager;

    public bool isPressed = false;
    public float readValue;
    public float prevReadValue;
    private Queue<float> readValues = new Queue<float>();
    private const float GAP_TIME = 1f;
    private float _pastTime = 0;


    [SerializeField] List<Collision> layers = new List<Collision>();
    private const float STABLE_MOTION_EPSILON = 2f;
    public int currentPlantLayer = 0;
    private Queue<float> higherScales = new Queue<float>();
    private const int NEXT_LAYER_TIME = 5;
    private const float HIGH_SCALE_DURATION = 1.5f;

    void Start()
    {
        ProcessScaleForLayerObjects();
    }

    private void ProcessScaleForLayerObjects()
    {
        foreach (GameObject parent in plantsParents)
        {
            foreach (Transform child in parent.transform)
            {
                higherScales.Enqueue(child.localScale.x);
                child.DOScale(0.1f, 0.001f);
            }
        }

    }


    void Update()
    {
        if (Input.GetKeyDown("space")) { isPressed = true; }

        if (!isPressed) { return; }

        readValue = ArduinoManager.ReadValue;

        if (_pastTime < GAP_TIME)
        {
            _pastTime += Time.deltaTime;
            readValues.Enqueue(readValue);

            return;
        }

        readValues.Enqueue(readValue);
        prevReadValue = readValues.Dequeue();

        if (readValue > prevReadValue + STABLE_MOTION_EPSILON)
        {
            ProcessSuccessBreath();
        }

        //if (layers[currentPlantLayer].i)
        {
            ProcessNextLayer();
        }
    }

    private void ProcessSuccessBreath()
    {
     
    }

    private void ProcessNextLayer()
    {
        GameObject parent = plantsParents[currentPlantLayer];
        parent.SetActive(true);

        foreach (Transform child in parent.transform)
        {
            //UIManager.ChangeIconText((currentPlantLayer + 1).ToString());
            float scale = higherScales.Dequeue();
            child.gameObject.SetActive(true);
            child.DOScale(scale, HIGH_SCALE_DURATION);
        }
        currentPlantLayer++;

    }
}
