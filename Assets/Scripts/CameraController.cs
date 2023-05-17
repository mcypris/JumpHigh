using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, currentPosY, transform.position.z);
    }

    public void MoveToNewLevel(Transform _newLevel)
    {
        currentPosY = _newLevel.position.y;
    }    
}
