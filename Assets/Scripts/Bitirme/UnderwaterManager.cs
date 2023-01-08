using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnderwaterManager : MonoBehaviour
{
    [SerializeField] List<GameObject> fishes;
    [SerializeField] ArduinoManager ArduinoManager;
    [SerializeField] ParticleSystem bubbles;

    public bool isPressed = false;
    public float readValue;
    public float prevReadValue;
    private Queue<float> readValues = new Queue<float>();
    public float GAP_TIME = 1f;
    private float _pastTime = 0;

    public int currentPlantLayer = 0;
    public float _breathTimer = 0;
    public int NEXT_LAYER_TIME = 5;
    public float STABLE_MOTION_EPSILON;


    // Update is called once per frame
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
        else
        {
            bubbles.Stop();
        }

        if (_breathTimer > NEXT_LAYER_TIME)
        {
            ProcessNextLayer();
        }
    }

    private void ProcessSuccessBreath()
    {
        _breathTimer += Time.deltaTime;

        if (!bubbles.isPlaying)
        {
            bubbles.Play();
        }
    }

    private void ProcessNextLayer()
    {
        GameObject fish = fishes[currentPlantLayer];
        fish.SetActive(true);
        currentPlantLayer++;
        _breathTimer = 0;
    }
}
