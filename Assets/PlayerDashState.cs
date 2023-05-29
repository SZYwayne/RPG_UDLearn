using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(player.dashSpeed * player.facingDir, rb.velocity.y);
        stateTimer = player.dashDuration;
        player.dashTime = player.dashColddown;
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
