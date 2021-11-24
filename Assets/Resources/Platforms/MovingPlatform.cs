using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;

    float currentTime = 0;
    bool isMovingTo; // true for start to end, false for end to start

    private Rigidbody player;
    private Vector3 currentPos;


    private void FixedUpdate()
    {
        currentTime += Time.deltaTime / travelTime;
        if (isMovingTo) this.transform.position = Vector3.Lerp(startPoint.transform.position, endPoint.transform.position, currentTime);
        else this.transform.position = Vector3.Lerp(endPoint.transform.position, startPoint.transform.position, currentTime);


        if (this.transform.position == endPoint.transform.position)
        {
            isMovingTo = false;
            currentTime = 0;
        }
        if (this.transform.position == startPoint.transform.position)
        {
            isMovingTo = true;
            currentTime = 0;
        }

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.tag == "Player")
        {
            player = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.parent = null;
        }
    }
    */
}
