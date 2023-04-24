using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] public float scrollSpeed = .1f;
    public float scrollY;

    private void Awake()
    {
        scrollY = scrollSpeed;
    }

    private void Update()
    {
        var offsetY = scrollY * Time.deltaTime;
        GetComponent<Renderer>().material.mainTextureOffset += new Vector2(0, offsetY);
    }

}
