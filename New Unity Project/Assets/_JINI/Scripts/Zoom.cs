using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float max = 0f;
    [SerializeField] float min = 0f;

    void CameraZoom()
    {
        float zoomDirection = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y <= max && zoomDirection > 0)
            return;
        if (transform.position.y >= min && zoomDirection < 0)
            return;

        transform.position += transform.forward * zoomDirection * speed;
    }
    void camMove()
    {
        if(Input.GetMouseButton(2))
        {
            float posX = Input.GetAxis("Mouse X");
            float posZ = Input.GetAxis("Mouse Y");
            transform.position += new Vector3(posX, 0, posZ);

        }    
    }
    private void Update()
    {
        CameraZoom();
        camMove();


    }
}
