using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    private void Start()
    {
        offset.x = 0;
        offset.y = 0;
        offset.z = transform.position.z - player.position.z;
    }

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
