using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] UnityEvent methodsToCall;

    GameObject objectToMove;
    public void InvokeMethods()
    {
        methodsToCall.Invoke();
    }

    public void SetObject(GameObject value)
    {
        objectToMove = value;
    }

    public void AssignObjectNewParent(GameObject value)
    {
        objectToMove.transform.parent = value.transform;
    }
}
