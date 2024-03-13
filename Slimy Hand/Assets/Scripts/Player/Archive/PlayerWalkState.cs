using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        ctx.playerController.Move(ctx.moveVector * ctx.walkSpeed + ctx.gravityVector * Time.deltaTime);
        if (!ctx.grounded)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Air);
        }
        if (ctx.grounded && Input.GetKeyDown(KeyCode.Space))
        {
            ctx.gravityVector.y += Mathf.Sqrt(-2 * Physics.gravity.y * ctx.jumpHeight);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ctx.RequestSwitchState(PlayerStateEnum.Run);
        }
        if (!ctx.moving)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Idle);
        }
    }
}