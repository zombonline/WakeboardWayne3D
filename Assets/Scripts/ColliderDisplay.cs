using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisplay : MonoBehaviour
{
    Collider collider;
    [SerializeField] Color gizmoColor = new Color(0, 1, 0, 0.5f);
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        collider = GetComponent<Collider>();
        Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        
    }
}
