using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position ,new Vector3(transform.position.x, player.position.y + 7f, transform.position.z), Time.deltaTime * 10);
    }
}
