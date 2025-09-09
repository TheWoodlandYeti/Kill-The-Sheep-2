using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    // size of draw
    [SerializeField]
    protected float debugDrawRadius = 1f;

    public virtual void OnDrawGizmos()
    {
        // gizmo colour, shape, and transform
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
