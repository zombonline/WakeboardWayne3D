using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    LineRenderer lineRenderer;
    [SerializeField] Transform screenPoint, playerPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }


    private void Update()
    {
        lineRenderer.SetPosition(0, playerPoint.position);
        lineRenderer.SetPosition(1, screenPoint.position);
    }

}
