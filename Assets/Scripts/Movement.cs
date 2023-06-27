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
        GroundCheck();

        CheckForLandingAfterRamp();
        RailingCheck();
    }
    private void FixedUpdate()
    {
        Move();

    }

    void RailingCheck()
    {
        isGrinding = (isGrounded && tracks[currentTrack].GetComponentInChildren<RailCheck>().railingActive);
        animator.SetBool("isGrinding", isGrinding);

        if(isGrinding && transform.position.y < 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 2.4f, transform.position.z);
        }
    }

    void CheckForLandingAfterRamp()
    {
        if(isGrounded && airborneFromRamp)
        {
            FindObjectOfType<RampTrickComboManager>().EndCombo();
            airborneFromRamp = false;
            StartCoroutine(TransitionCamFov(60));
            FindObjectOfType<SlowMotion>().ActivateSlowMotion(1f, .5f);
            FindObjectOfType<ObjectMovement>().ignoreMultiplier = false;
            GetComponent<Health>().EnableInvincibility(.2f, false);
            GetComponent<RampTrickComboManager>().EndCombo();
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

        rb.AddForce(Vector3.up * (jumpForce / 3f), ForceMode.Impulse);
    }
    public void SwitchLane(int dir)
    {
        //Do not move to track if it does not exist.
        if (currentTrack + dir < 0 || currentTrack + dir > tracks.Length -1)
        {
            return;
        }
        //Everything is ok to move, mounting/dismount hop if coming from or going to a railing lane.
        else
        {
            if (isGrinding || tracks[currentTrack + dir].GetComponentInChildren<RailCheck>().railingActive)
            {
                RailingDismountHop();
            }
            currentTrack = currentTrack + dir;
        }
    }
    

    public void Jump()
    {
        if (isGrounded && !airborneFromRamp)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
