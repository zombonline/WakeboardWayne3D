using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    LineRenderer lineRenderer;
    [SerializeField] Transform screenPoint, ropeHolder;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();    
        if(ropeHolder == null)
        {
            ropeHolder = GameObject.FindWithTag("Rope Holder").transform;
        }
    }


    private void Update()
    {
        lineRenderer.SetPosition(0, ropeHolder.position);
        lineRenderer.SetPosition(1, screenPoint.position);
    }

}
