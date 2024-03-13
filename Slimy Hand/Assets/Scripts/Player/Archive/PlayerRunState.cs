using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        ctx.playerController.Move(ctx.moveVector * ctx.runSpeed);
        if (!ctx.grounded)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Air);
        }
        if (ctx.grounded && Input.GetKeyDown(KeyCode.Space))
        {
            ctx.gravityVector.y += Mathf.Sqrt(-2 * Physics.gravity.y * ctx.jumpHeight);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ctx.RequestSwitchState(PlayerStateEnum.Walk);
        }
        if (!ctx.moving)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Idle);
        }
    }
}