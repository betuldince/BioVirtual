using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    private float _time = 0;
    private float _randomShift;
    private Vector3 _startPosition;
    void Start()
    {
        _randomShift = Random.Range(0, 10);
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _startPosition + 5 * Vector3.up * Mathf.Sin(_randomShift + Time.time);
        _time = Time.deltaTime;
    }
}
