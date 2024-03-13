using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private Vector3 gravityVector;
    private Vector3 enterVelocity;
    private Vector3 moveVelocity;
    public override void EnterState()
    {
        //enterVelocity = new Vector3(ctx.playerController.velocity.x, 0, ctx.playerController.velocity.z);
        //moveVelocity = enterVelocity;
    }
    public override void ExitState()
    {
        gravityVector.y = 0;
    }
    public override void UpdateState()
    {
        gravityVector.y += Time.deltaTime * Time.deltaTime * Physics.gravity.y;

        //moveVelocity -= moveVelocity * 0.1f * Time.deltaTime;

        ctx.playerController.Move(/*ctx.moveVector * ctx.airSpeed + moveVelocity + */gravityVector);

        if (ctx.grounded)
        {
            ctx.RequestSwitchState(PlayerStateEnum.Idle);
        }
    }
}