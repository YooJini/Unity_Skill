using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField] float force = 0f;
    [SerializeField] Vector3 offset = Vector3.zero;

    Quaternion originRot;

    // Start is called before the first frame update
    void Start()
    {
        originRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(ShakeCoroutine());
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            StartCoroutine(Reset());
        }
    }
    IEnumerator ShakeCoroutine()
    {
        Vector3 originEuler = transform.eulerAngles;
        while(true)
        {
            float rotX = Random.Range(-offset.x, offset.x);
            float rotY= Random.Range(-offset.y, offset.y);
            float rotZ= Random.Range(-offset.z, offset.z);

            Vector3 randomRot = originEuler + new Vector3(rotX, rotY, rotZ);
            Quaternion rot = Quaternion.Euler(randomRot);

            while(Quaternion.Angle(transform.rotation,rot)>0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, force * Time.deltaTime);
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator Reset()
    {
        while (Quaternion.Angle(transform.rotation,originRot)>0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originRot, force * Time.deltaTime);
            yield return null;
        }
    }
}
