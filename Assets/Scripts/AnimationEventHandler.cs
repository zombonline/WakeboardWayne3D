using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//This script allows a public Unity Event to be called by animations attached the the same object.
//This will allow the animation to invoke other methods not attached to the same object.
public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] UnityEvent methodsToCall;
    public void InvokeMethods()
    {
        methodsToCall.Invoke();
    }
}
