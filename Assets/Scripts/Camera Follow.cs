using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPos;
    public int height;
    public float offset = 10f;
    void Update()
    {
        transform.position = new Vector3(playerPos.position.x , height, playerPos.position.z - offset);
    }
}
