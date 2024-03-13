using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerContext context;
    public static PlayerStateMachine stateMachine;
    [SerializeField] private CameraController camController;
    [SerializeField] private CharacterController playerController;

    private PlayerWalkState walkState;
    private PlayerRunState runState;
    private PlayerIdleState idleState;
    private PlayerAirState airState;
    private PlayerJumpState jumpState;

    private PlayerState currentState;
    private bool switchingState;

    public delegate void StateSwitched();
    public static event StateSwitched OnStateSwitched;

    public static Dictionary<PlayerStateEnum, PlayerState> stateDictionary;

    private void Awake()
    {
        stateMachine = this;
        context = new PlayerContext()
        {
            walkSpeed = 5,
            runSpeed = 7.5f,
            airSpeed = 1,
            jumpHeight = 50,
            playerController = playerController,
            camController = camController,
            transform = transform
        };
        InitializeStates();

        SwitchState(idleState);
    }
    void InitializeStates()
    {
        walkState = new PlayerWalkState();
        runState = new PlayerRunState();
        idleState = new PlayerIdleState();
        airState = new PlayerAirState();
        jumpState = new PlayerJumpState();

        stateDictionary = new Dictionary<PlayerStateEnum, PlayerState>()
        {
            { PlayerStateEnum.Walk, walkState },
            { PlayerStateEnum.Run, runState },
            { PlayerStateEnum.Idle, idleState },
            { PlayerStateEnum.Air, airState },
            { PlayerStateEnum.Jump, jumpState }
        };
    }
    public void SwitchState(PlayerState state)
    {
        if(currentState == state && !state.canEnterSelf || switchingState)
        {
            return;
        }

        switchingState = true;

        if(currentState != null)
            currentState.ExitState();
        currentState = state;

        currentState.EnterState();

        OnStateSwitched?.Invoke();

        switchingState = false;
    }
    private void Update()
    {
        context.Update();

        if (switchingState || currentState == null)
            return;

        currentState.UpdateState();
        Debug.Log(currentState);
    }
}
public class PlayerContext
{
    private float x, z;
    public Vector3 moveVector;

    public CharacterController playerController;
    public CameraController camController;
    public Transform transform;

    public float walkSpeed;
    public float runSpeed;
    public float airSpeed;
    public float jumpHeight;

    public bool moving;
    public bool grounded;

    public Vector3 gravityVector;

    private PlayerStateMachine stateMachine => PlayerStateMachine.stateMachine;

    public void Update()
    {
        grounded = Grounded();

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        moveVector = transform.right * x + transform.forward * z;
        moveVector = moveVector.normalized * Time.deltaTime;

        moving = moveVector.magnitude > 0;

        Debug.Log(grounded);
    }
    public void RequestSwitchState(PlayerStateEnum state)
    {
        stateMachine.SwitchState(PlayerStateMachine.stateDictionary[state]);
    }
    private bool Grounded()
    {
        if(Physics.SphereCast(transform.position, 0.1f, Vector3.down, out RaycastHit hit, playerController.height/2 + 0.05f))
        {
            return true;
        }
        return false;
    }
}
public abstract class PlayerState
{
    protected PlayerContext ctx => PlayerStateMachine.context;
    public bool canEnterSelf = false;
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
public enum PlayerStateEnum { Walk, Run, Idle, Air, Jump }