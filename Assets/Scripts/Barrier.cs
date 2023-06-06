using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Barrier : MonoBehaviour
{
    [SerializeField] List<Transform> sphereArray;

    LineRenderer lineRenderer;

    [SerializeField] GameObject spherePrefab;
    [SerializeField] public float sphereAmount;

    [SerializeField] public float ballSpeed = 1f;

    [SerializeField] ObjectMovement objectMovement;

    private void Awake()
    {

        for (int i = 0; i < sphereAmount; i++)
        {
            var newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            newSphere.transform.parent = transform;

            newSphere.transform.localPosition = new Vector3(0, 0, i * 5);
            sphereArray.Add(newSphere.transform);
        }
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = sphereArray.Count;
    }

    private void Update()
    {
        if(objectMovement!= null)
        {
            ballSpeed = objectMovement.speed;
        }

        for(int i = 0; i < sphereArray.Count;i++)
        {
            lineRenderer.SetPosition(i, sphereArray[i].position);
        }

    }
    public void ReplaceFirstListElement(Transform sphere)
    {
        sphereArray.Remove(sphere);
        sphereArray.Insert(0, sphere);
    }

 

}
