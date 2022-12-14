using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] GameObject herd;

     
    public Transform endPosition;

    public bool isCreated;
    public bool isArrived;

    public float positionX = 42.3f;
    public float positionZ = 150f;
    public float MAP_TO_UPPER = 35f;
    public float MAP_TO_LOWER = -25f;


    private void OnEnable()
    {
        isCreated = true;
    }
 
    void Update()
    {

        if (isCreated && !isArrived)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition.position, Time.deltaTime * 0.7f);

            return;
            if (Vector3.Distance(transform.position, endPosition.position) < 0.05f)
            {
                isArrived = true;
                //transform.SetParent(herd.transform);

            }

            
        }

    }


   




}
