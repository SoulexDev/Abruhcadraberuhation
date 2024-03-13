using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float sprintMult = 1.5f;
    private float sensitivity => PlayerPrefs.GetFloat("Sensitivity", 2.5f);
    [SerializeField] private float _jumpHeight = 5;
    private float jumpHeight => _jumpHeight;
    public float maxSpeed = 10;
    [SerializeField] private float airMoveAmount = 40;
    [SerializeField] private float gravity = 15;
    [SerializeField] private AnimationCurve crouchYCurve;
    [SerializeField] private AnimationCurve crouchZCurve;
    [SerializeField] private AnimationCurve landCurve;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float crouchHeight = 0.4f;

    [SerializeField] private Transform camPos;
    [SerializeField] private Transform camHolder;
    [SerializeField] private AudioSource walkSource;
    //[SerializeField] private Collider col;

    private Transform cam;
    private Vector2 movementInput;
    public Vector2 camInput;

    Vector3 lastDir;
    Vector3 moveDir;
    Vector3 controllerMoveDir;
    Vector3 yVel;
    Vector3 groundNormal;

    private bool grounded => Grounded();
    private bool groundedPrevFrame;
    private float groundDist;

    public bool isMovingAndGrounded => grounded && moving;

    private bool moving;
    private bool jumping;
    private bool canJump = true;
    private bool crouching;
    private bool sprinting;

    private bool ceilChecked;

    private float startColHeight;
    private float startCamHeight;

    private float lerpPosY = 0;
    private float lerpPosZ = 0;
    private float lerpRotX = 0;
    private float landLerpZ = 0;

    LayerMask ignoreMask;

    public enum MoveState { GroundMove, AirMove }
    public MoveState moveState;

    void Awake()
    {
        cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startColHeight = controller.height;
        startCamHeight = camPos.localPosition.y;

        ignoreMask = ~LayerMask.GetMask("Player", "Ignore Player", "Ignore Raycast", "Pickup");

        if (moveSpeed > maxSpeed)
            maxSpeed = moveSpeed;
    }
    void Update()
    {
        SetMoveState();

        if (Player.Instance.canMove && !Player.Instance.paused)
            PlayerInput();
        else
            movementInput = Vector2.zero;

        Movement();
        CheckLanded();
    }
    private void LateUpdate()
    {
        CamMovement();
        CalculateCrouch();
    }
    void SetMoveState()
    {
        moveState = grounded ? MoveState.GroundMove : MoveState.AirMove;
    }
    void PlayerInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        if (Player.Instance.canLook)
        {
            camInput.x += Input.GetAxisRaw("Mouse X") * sensitivity;
            camInput.y += Input.GetAxisRaw("Mouse Y") * sensitivity;

            camInput.y = Mathf.Clamp(camInput.y, -90, 90);
        }

        if (grounded && Input.GetButtonDown("Jump") && !jumping && canJump)
        {
            jumping = true;
            canJump = false;
            yVel.y = jumpHeight;

            lastDir += controller.velocity;

            Invoke(nameof(ResetJump), 0.2f);
        }

        bool canUnCrouch = true;
        if (Physics.SphereCast(transform.position, controller.radius, Vector3.up, out RaycastHit hit, 2, ignoreMask))
        {
            canUnCrouch = hit.distance > 1;
        }
        crouching = canUnCrouch ? Input.GetKey(KeyCode.LeftControl) : true;

        sprinting = Input.GetKey(KeyCode.LeftShift) && !crouching;
    }

    void CheckLanded()
    {
        if (grounded && !groundedPrevFrame)
        {
            Invoke(nameof(SetCanJump), 0.2f);
            StartCoroutine(DoLandCurve());
            //AudioManager.Instance.PlayAudioOnGlobalSource(AudioManager.Instance.playerLand);
        }
        groundedPrevFrame = grounded;
    }
    void SetCanJump()
    {
        canJump = true;
    }
    void Movement()
    {
        SlopeCheck();

        moveDir = (Vector3.Cross(transform.right, groundNormal) * movementInput.y - Vector3.Cross(transform.forward, groundNormal) * movementInput.x).normalized;

        moveDir *= moveSpeed * (sprinting ? sprintMult : 1) * (crouching ? 0.5f : 1);

        moving = new Vector3(moveDir.x, 0, moveDir.z).magnitude > 0;

        if (grounded)
            maxSpeed = moveSpeed * (sprinting ? sprintMult : 1);

        //if(!walkSource.isPlaying && isMovingAndGrounded)
        //{
        //    walkSource.Play();
        //}
        //else if(walkSource.isPlaying && !isMovingAndGrounded)
        //{
        //    walkSource.Stop();
        //}

        switch (moveState)
        {
            case MoveState.GroundMove:
                MoveGround();
                break;
            case MoveState.AirMove:
                MoveAir();
                break;
            default:
                break;
        }

        if (!controller.enabled)
            return;
        controllerMoveDir += yVel;
        controller.Move(controllerMoveDir * Time.deltaTime);
    }
    void MoveGround()
    {
        if (!jumping)
            yVel.y = Mathf.MoveTowards(yVel.y, -1, Time.deltaTime * 5);

        if (!moving && !jumping)
        {
            lastDir = Vector3.Lerp(lastDir, Vector3.zero, Time.deltaTime * 10);
            controller.Move(lastDir * Time.deltaTime);
        }
        if (moving)
            lastDir = new Vector3(moveDir.x, 0, moveDir.z);

        controllerMoveDir = moveDir;
    }
    void MoveAir()
    {
        if (!controller.enabled)
            return;

        moveDir.y = 0;
        if (moving)
        {
            lastDir += moveDir * Time.deltaTime * airMoveAmount;

            if (lastDir.magnitude > maxSpeed)
                lastDir *= maxSpeed / lastDir.magnitude;
        }
        else if (!jumping)
        {
            lastDir.x = Mathf.Abs(controller.velocity.x) < Mathf.Abs(lastDir.x) ? controller.velocity.x : lastDir.x;
            lastDir.z = Mathf.Abs(controller.velocity.z) < Mathf.Abs(lastDir.z) ? controller.velocity.z : lastDir.z;
        }
        CheckCeiling();

        yVel.y -= gravity * Time.deltaTime;
        controllerMoveDir = lastDir;
    }
    void CamMovement()
    {
        lerpRotX = Mathf.Lerp(lerpRotX, !grounded ? jumpCurve.Evaluate(groundDist / 1.5f) : 0, Time.deltaTime * 5);

        transform.rotation = Quaternion.Euler(0, camInput.x, 0);

        camPos.rotation = Quaternion.Euler(-camInput.y + lerpRotX, camInput.x, 0);

        cam.SetPositionAndRotation(camHolder.position, camHolder.rotation);
    }
    void CalculateCrouch()
    {
        controller.height = crouching ? startColHeight / 2 : startColHeight;
        controller.center = Vector3.up * (crouching ? -(startColHeight / 4) : 0);

        float target = crouching ? 1 : 0;

        Vector3 camDesiredPosY = Vector3.up * (crouchHeight - 1);

        Vector3 camDesiredPosZ = Vector3.forward * 0.2f;

        lerpPosY = Mathf.MoveTowards(lerpPosY, target, Time.deltaTime * crouchSpeed);
        lerpPosZ = Mathf.MoveTowards(lerpPosZ, target, Time.deltaTime * crouchSpeed);

        Vector3 desiredPos = Vector3.Lerp(Vector3.up * startCamHeight, camDesiredPosY, crouchYCurve.Evaluate(lerpPosY)) +
            Vector3.Lerp(Vector3.zero, camDesiredPosZ, crouchZCurve.Evaluate(lerpPosZ));

        camPos.localPosition = desiredPos + Vector3.up * landLerpZ;
    }
    void ResetJump()
    {
        jumping = false;
    }
    public void AddForce(Vector3 force)
    {
        jumping = true;

        controllerMoveDir += new Vector3(force.x, 0, force.z);
        yVel.y += force.y;

        Invoke(nameof(ResetJump), 0.5f);
    }
    public bool Grounded()
    {
        return Physics.SphereCast(transform.position, 0.35f, Vector3.down, out RaycastHit hit, 0.85f, ignoreMask);
    }
    void CheckCeiling()
    {
        if (controller.collisionFlags == CollisionFlags.Above)
        {
            if (!ceilChecked)
                yVel.y = -0.1f;
            ceilChecked = true;
        }
        else
            ceilChecked = false;
        //if (Physics.SphereCast(transform.position, 0.2f, Vector3.up, out RaycastHit hit, 1.2f, ~LayerMask.GetMask("Ignore Raycast", "Ignore Player", "Player")))
        //{
        //    if (!ceilChecked)
        //        yVel.y = -0.1f;
        //    ceilChecked = true;
        //}
        //else
        //    ceilChecked = false;
    }
    public void SlopeCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, ~LayerMask.GetMask("Player", "Ignore Player", "Ignore Raycast")))
        {
            groundNormal = hit.normal;
            groundDist = hit.distance;
        }
        else
        {
            groundNormal = Vector3.up;
            groundDist = 1.5f;
        }
    }
    IEnumerator DoLandCurve()
    {
        float timer = 0;
        while (timer < 1)
        {
            landLerpZ = landCurve.Evaluate(timer);
            timer += Time.deltaTime;
            yield return null;
        }
        landLerpZ = 0;
    }
}