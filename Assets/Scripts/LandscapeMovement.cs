using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
    }
}
