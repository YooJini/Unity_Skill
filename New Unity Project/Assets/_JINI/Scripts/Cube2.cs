using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{
    [SerializeField] GameObject goPrefab = null;
    [SerializeField] float force = 0f;
    [SerializeField] Vector3 offset = Vector3.zero;

    public void Explosion()
    {
        GameObject clone = Instantiate(goPrefab, transform.position, Quaternion.identity);
        Rigidbody[] rigids = clone.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].AddExplosionForce(force, transform.position + offset, 10f);
        }
        gameObject.SetActive(false);
    }
}
