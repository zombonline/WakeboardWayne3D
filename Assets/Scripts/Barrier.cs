using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;


//This script will generate and control a set amount of floating balls to simulate a barrier for the player.
public class Barrier : MonoBehaviour
{
    List<Transform> sphereArray = new List<Transform>(); //Reference to all active balls

    LineRenderer lineRenderer; //Line renderer connecting all balls

    [SerializeField] GameObject spherePrefab; //Ball prefab to generate
    [SerializeField] public float sphereAmount; //Amount to generate
    public float ballSpeed = 1f; //Speed at which balls will travel.
    [SerializeField] ObjectMovement objectMovement; //Reference to objectMomvement script

    private void Awake()
    {
        //for loop to initialise balls
        for (int i = 0; i < sphereAmount; i++)
        {
            var newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            newSphere.transform.parent = transform;

            newSphere.transform.localPosition = new Vector3(0, 0, i * 5);
            sphereArray.Add(newSphere.transform);
        }
        lineRenderer = GetComponent<LineRenderer>(); //Reference to line renderer
        lineRenderer.positionCount = sphereArray.Count; //Assign position counts to match amount of balls
    }

    private void Update()
    {
        if(objectMovement == null)
        {
            Debug.LogError("No Object Movement script found. Balls can not move.");
            return;
        }
        ballSpeed = objectMovement.speed; //Assign speed variable to match object movement speed

        //for loop to update line render positions to match location of balls.
        for (int i = 0; i < sphereArray.Count;i++) 
        {
            lineRenderer.SetPosition(i, sphereArray[i].position);
        }

    }

    //Method to use when a ball has reached the end of the line and has to loop back to start of barrier.
    //Updating the list so that this ball is now the first element will ensure the line renderer is not overlapping itself.
    public void ReplaceFirstListElement(Transform sphere)
    {
        sphereArray.Remove(sphere);
        sphereArray.Insert(0, sphere);
    }

 

}
