using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBall : MonoBehaviour
{
    Barrier barrier;
    float targetHeight;
    [SerializeField] float floatSpeed;
    [SerializeField] float minHeight, maxHeight;
    private void Start()
    {
        barrier = transform.parent.GetComponent<Barrier>();
        targetHeight = Random.Range(minHeight, maxHeight);

    }

    private void Update()
    {
        MoveZ();
        MoveY();
    }

    private void MoveY()
    {
        if (System.Math.Round(transform.localPosition.y, 3) != System.Math.Round(targetHeight, 3))
        {
            Debug.Log("Moving");
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, targetHeight, transform.localPosition.z), floatSpeed * Time.deltaTime);
        }
        else
        {
            if (targetHeight != minHeight)
            {
                targetHeight = minHeight;
            }
            else
            {
                targetHeight = Random.Range(minHeight, maxHeight);
            }
        }
    }

    private void MoveZ()
    {
        transform.localPosition += transform.forward * barrier.ballSpeed * Time.deltaTime;
        if (transform.localPosition.z > barrier.sphereAmount * 5)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
            barrier.ReplaceFirstListElement(transform);
        }
    }
}
