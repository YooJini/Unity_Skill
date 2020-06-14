using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform gunBody = null;
    [SerializeField] float range = 0f;
    [SerializeField] LayerMask layerMask = 0;
    [SerializeField] float spinSpeed = 0f;
    [SerializeField] float fireRate = 0f;
    float currentFireRate;

    Transform tfTarget = null;

    void SearchEnemy()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, range, layerMask);
        Transform shortestTarget = null;

        if(cols.Length>0)
        {
            float shortestDistance = Mathf.Infinity;
            foreach(Collider colTarget in cols)
            {
                float distance = Vector3.SqrMagnitude(transform.position - colTarget.transform.position);
                if(shortestDistance>distance)
                {
                    shortestDistance = distance;
                    shortestTarget = colTarget.transform;
                }
            }
        }

        tfTarget = shortestTarget;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentFireRate = fireRate;
        InvokeRepeating("SearchEnemy", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tfTarget == null)
            gunBody.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(tfTarget.position);
            Vector3 euler = Quaternion.RotateTowards(gunBody.rotation, lookRotation, spinSpeed * Time.deltaTime).eulerAngles;

            gunBody.rotation = Quaternion.Euler(0, euler.y, 0);

            Quaternion fireRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
            if(Quaternion.Angle(gunBody.rotation,fireRotation)<5f)
            {
                currentFireRate -= Time.deltaTime;
                if(currentFireRate<=0)
                {
                    currentFireRate = fireRate;
                    Debug.Log("발사!!!");
                }
            }

        }
    }
}
