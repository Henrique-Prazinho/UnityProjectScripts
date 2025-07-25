using UnityEngine;

public class WalkingState : PlayerStates
{
    public override void Enter(PlayerStateManager player)
    {
        //Debug.Log("Entrei no Estado de andando!");
    }

    public override void Update(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.ChangeToJumpingState();
        }

        float moveInput = Input.GetAxis("Horizontal");
        player.rb.linearVelocity = new Vector2(moveInput * player.Status.Speed, 0);
    }
    public override void FixedUpdate(PlayerStateManager player)
    {
        //COYOTE TIME - Tempo para pular depois de sair do chão
        if (player.Collision.IsGrounded())
        {
            //Enquanto estiver no chão reseta o tempo do CoyoteTime
            player.coyoteTimeCounter = player.Status.CoyoteTime;
        }
        else
        {
            player.coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public override void Exit(PlayerStateManager player)
    {

    }

}