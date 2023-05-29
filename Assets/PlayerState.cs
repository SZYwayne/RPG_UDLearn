using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected float xInput;
    public Rigidbody2D rb;

    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        player.dashTime -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }


}
