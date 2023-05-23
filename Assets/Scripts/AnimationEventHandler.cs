using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] UnityEvent methodsToCall;

    public void InvokeMethods()
    {
        methodsToCall.Invoke();
    }
}
