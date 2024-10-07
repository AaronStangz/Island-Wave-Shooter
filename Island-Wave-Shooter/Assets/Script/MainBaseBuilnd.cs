using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainBaseBuilnd : MonoBehaviour
{
    [SerializeField] private LayerMask Built;
    public List<GameObject> Ghost;
    public List<Collider> testSphere;

    public float BuildRange;

    private void OnCollisionEnter(Collision collision)
    {
        if (testSphere[1] != null && collision.collider == testSphere[1])
        {
            if (collision.gameObject.CompareTag("Built"))
            {
                Destroy(Ghost[1]);
            }
        }
        if (testSphere[2] != null && collision.collider == testSphere[2])
        {
            if (collision.gameObject.CompareTag("Built"))
            {
                Destroy(Ghost[2]);
            }
        }
        if (testSphere[3] != null && collision.collider == testSphere[3])
        {
            if (collision.gameObject.CompareTag("Built"))
            {
                Destroy(Ghost[3]);
            }
        }
        if (testSphere[4] != null && collision.collider == testSphere[4])
        {
            if (collision.gameObject.CompareTag("Built"))
            {
                Destroy(Ghost[4]);
            }
        }
    }
}
