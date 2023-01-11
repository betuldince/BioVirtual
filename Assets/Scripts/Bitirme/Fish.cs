using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{     
    public Transform endPosition;
    public float speed;
    public bool isCreated;

       
    private void OnEnable()
    {
        isCreated = true;
    }
 
    void Update()
    {
        if (isCreated)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition.position, Time.deltaTime * speed);
        }  
    }
}
