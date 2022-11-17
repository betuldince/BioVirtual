using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRotation : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public bool bIsOnTheMove = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoving();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            
            if (bIsOnTheMove)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
 
 
           
    }
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 finalPos = transform.position;
        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
            bIsOnTheMove = true;
    }


}
