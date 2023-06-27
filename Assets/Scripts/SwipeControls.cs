using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeControls : MonoBehaviour
{
    [Tooltip("Pixel value the player needs to move their finger by to perform a swipe.")]
    [SerializeField] float swipeValue = 100f;
    [Tooltip("Time value the player needs to perform a second tap by to perform a double tap.")]
    [SerializeField] float doubleTapTime;
    float doubleTapTimer;

    Vector2 swipeStart, direction;

    [SerializeField] UnityEvent onSwipeLeft, onSwipeRight, onSwipeUp, onSwipeDown, onDoubleTap;

    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        doubleTapTimer -= Time.deltaTime;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        swipeStart = touch.position;
                        break;

                    case TouchPhase.Moved:

                        break;

                    case TouchPhase.Ended:
                        direction = touch.position - swipeStart;
                        //if player swipes both left and up or right and down etc. a comparison between the distance is made, if they swiped more up then up is used etc.

                        //Swiped left or right

                        if (Mathf.Abs(direction.x) > swipeValue || Mathf.Abs(direction.y) > swipeValue)
                        {
                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                //Swiped left - attempt to move to left lane
                                if (direction.x < -swipeValue) { onSwipeLeft.Invoke(); }

                                //Swiped right - attempt to move to right lane
                                else if (direction.x > swipeValue) { onSwipeRight.Invoke(); }
                            }
                            else
                            {
                                if (direction.y > swipeValue)
                                {
                                    onSwipeUp.Invoke();
                                }
                                //Swiped Down
                                else if (direction.y < -swipeValue)
                                {
                                    onSwipeDown.Invoke();
                                }
                            }
                        }
                        //No swipe made 
                        else
                        {
                            //two taps performed in quick succession && player is airborne
                            if (doubleTapTimer > 0f)
                            {
                                onDoubleTap.Invoke();
                                //reset doubletap
                                doubleTapTimer = 0f;
                            }

                            //if tap this frame was not preceeded by a tap within the time frame, begin the timer to check for double tap on next tap.
                            else if (doubleTapTimer <= 0f)
                            {
                                doubleTapTimer = doubleTapTime;
                            }
                        }
                        break;
                }
            }
        }
    }
}
