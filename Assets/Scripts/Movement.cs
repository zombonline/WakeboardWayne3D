using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Tooltip("Tracks that the player can move between.")]
    [SerializeField] Track[] tracks;
    int currentTrack = 1;
    Rigidbody rb;
    [Tooltip("Pixel value the player needs to move their finger by to perform a swipe.")]
    [SerializeField] float swipeValue = 100f;
    [Tooltip("Time value the player needs to perform a second tap by to perform a double tap.")]
    [SerializeField] float doubleTapTime;

    float doubleTapTimer;
    [Tooltip("How quickly the player moves between lanes after a swipe.")]
    [SerializeField] float speed;
    [SerializeField] public float jumpForce, fallMultiplier, jumpVelocityFallOff, downwardVelocityCap = 15f;
    public bool isGrounded, isGrinding;

    [Header("Raycast variables")]
    [SerializeField] Transform raycastStartPosition;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask raycastLayer;

    Vector2 swipeStart, direction;

    public bool airborneFromRamp = false;

    [SerializeField] Animator animator;

    float DELETELATER = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = tracks[currentTrack].transform.position;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        SwipeControls();
        GroundCheck();

        CheckForLandingAfterRamp();
        CheckForRailing();
    }
    private void FixedUpdate()
    {
        Move();

    }

    void CheckForRailing()
    {
        if(isGrounded && tracks[currentTrack].GetComponentInChildren<RailCheck>().railingActive)
        {
            isGrinding = true;
            animator.SetBool("isGrinding", true);
        }
        else
        {
            isGrinding = false;
            animator.SetBool("isGrinding", false);
        }
    }

    void CheckForLandingAfterRamp()
    {
        if (airborneFromRamp)
        {
            if (transform.position.y > DELETELATER)
            {
                DELETELATER = transform.position.y;
            }
        }

        if(isGrounded && airborneFromRamp)
        {
            Debug.Log(DELETELATER);
            DELETELATER = 0;
            FindObjectOfType<RampTrickComboManager>().EndCombo();
            airborneFromRamp = false;
            StartCoroutine(TransitionCamFov(60));
            FindObjectOfType<SlowMotion>().ActivateSlowMotion(1f, .5f);
            FindObjectOfType<ObjectMovement>().ignoreMultiplier = false;

        }
    }
    public void AirborneFromRamp()
    {
        airborneFromRamp = true;
        FindObjectOfType<RampTrickComboManager>().BeginCombo();
        StartCoroutine(TransitionCamFov(90));
    }

    IEnumerator TransitionCamFov(float targetFov)
    {
        var timeBetweenFOVUpdate = .01f / Mathf.Abs(targetFov - Camera.main.fieldOfView);
        while (Camera.main.fieldOfView > targetFov)
        {
            if (airborneFromRamp)
            {
                yield return null;
            }
            else
            {
                Camera.main.fieldOfView -= 2f;
                yield return new WaitForSeconds(timeBetweenFOVUpdate);
            }
        }
        while (Camera.main.fieldOfView < targetFov)
        {
            if (!airborneFromRamp)
            {
                yield return null;
            }
            else
            {
                Camera.main.fieldOfView += 2f;
                yield return new WaitForSeconds(timeBetweenFOVUpdate);
            }
        }        
    }


    private void GroundCheck()
    {
        var hit = Physics.Raycast(raycastStartPosition.position, Vector3.down, raycastLength, raycastLayer);
        Debug.DrawRay(raycastStartPosition.position, Vector2.down * raycastLength, Color.red);
        isGrounded = hit;

        animator.SetBool("isGrounded", isGrounded);
    }

   
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(tracks[currentTrack].transform.position.x, transform.position.y, 0), Time.unscaledDeltaTime * speed);

        //jump velocity fall off
        if (!isGrounded && rb.velocity.y < jumpVelocityFallOff)
        {
            rb.velocity += Vector3.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        //cap fall speed
        if (rb.velocity.y < downwardVelocityCap)
        {
            rb.velocity = new Vector2(rb.velocity.x, downwardVelocityCap);
        }
    }

    void RailingDismountHop()
    {
        rb.AddForce(Vector3.up * (jumpForce / 3), ForceMode.Impulse);
    }
    private void SwitchLane(int dir)
    {
        //Do not move if no track to desired position
        if (currentTrack + dir < 0 || currentTrack + dir > tracks.Length -1)
        {
            return;
        }
        //Do not move if track to desired position and player is not airborne or already on a railing
        else if (tracks[currentTrack + dir].GetComponentInChildren<RailCheck>().railingActive && isGrounded && !isGrinding)
        {
            return;
        }
        //Everything is ok to move, dismount hop if currently grinding
        else
        {
            if (isGrinding)
            {
                RailingDismountHop();
            }
            currentTrack = currentTrack + dir;
        }
    }
    private void SwipeControls()
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

                        if(Mathf.Abs(direction.x) > swipeValue || Mathf.Abs(direction.y) > swipeValue)
                        {
                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                //Swiped left - attempt to move to left lane
                                if (direction.x < -swipeValue) { SwitchLane(-1); }

                                //Swiped right - attempt to move to right lane
                                else if (direction.x > swipeValue) { SwitchLane(1); }
                            }
                            else
                            {
                                if (direction.y > swipeValue)
                                {
                                    //Jump if on ground and not airborne from ramp
                                    if (isGrounded && !airborneFromRamp) { Jump(); }
                                    //perform trick if airborne from ramp
                                    else if (!isGrounded && airborneFromRamp) { RampTrick("backFlip"); }
                                }
                                //Swiped Down
                                else if (direction.y < -swipeValue)
                                {
                                    //perform trick if airborne from ramp
                                    if (!isGrounded && airborneFromRamp) { RampTrick("frontFlip"); }
                                }
                            }
                        }
                        //No swipe made 
                        else
                        {
                            //two taps performed in quick succession && player is airborne
                            if (doubleTapTimer > 0f && airborneFromRamp)
                            {
                                //perform trick if right side of screen double tapped
                                if (touch.position.x > Screen.width / 2) { RampTrick("rightSpin"); }

                                //perofrm trick if left side of screen double tapped
                                else if (touch.position.x <= Screen.width / 2) { RampTrick("leftSpin"); }

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

    private void RampTrick(string trickAnimation)
    {
        if(isGrounded)
        {
            return;
        }
        animator.SetTrigger(trickAnimation);
        FindObjectOfType<Score>().AddScore(100f);
        FindObjectOfType<RampTrickComboManager>().TrickPeformed(trickAnimation);
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
}
