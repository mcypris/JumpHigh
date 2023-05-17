using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    [SerializeField] private Transform previousLevel;
    [SerializeField] private Transform nextLevel;
    [SerializeField] private CameraController cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            if(collider.transform.position.y > transform.position.y)
            {
                cam.MoveToNewLevel(nextLevel);
            }
            else
            {
                cam.MoveToNewLevel(previousLevel);
            }
        }
    }
}
