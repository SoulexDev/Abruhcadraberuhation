using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public override void EnterState()
    {
        Debug.Log("Jumps");
    }
    public override void ExitState()
    {
        
    }
    public override void UpdateState()
    {
        ctx.playerController.Move(ctx.gravityVector);
        if (!ctx.grounded)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Air);
        }
    }
}