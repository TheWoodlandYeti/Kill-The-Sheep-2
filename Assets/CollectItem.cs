using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Gun.magSize = Gun.maxMagSize;
        Destroy(gameObject);
    }
}
