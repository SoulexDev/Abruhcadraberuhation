using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void EnterState()
    {
        ctx.gravityVector.y = 0;
    }
    public override void ExitState()
    {
        
    }
    public override void UpdateState()
    {
        ctx.playerController.Move(ctx.gravityVector * Time.deltaTime);
        if (!ctx.grounded)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Air);
        }
        if(ctx.grounded && Input.GetKeyDown(KeyCode.Space))
        {
            ctx.gravityVector.y += Mathf.Sqrt(-2 * Physics.gravity.y * ctx.jumpHeight);
        }
        if(ctx.moving && !Input.GetKey(KeyCode.LeftShift))
        {
            ctx.RequestSwitchState(PlayerStateEnum.Walk);
        }
        else if (ctx.moving)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Run);
        }
    }
}