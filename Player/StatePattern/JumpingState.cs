using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class JumpingState : PlayerStates
{
    public override void Enter(PlayerStateManager player)
    {
        //Debug.Log("Entrei no Estado de pulando!");
    }

    public override void Update(PlayerStateManager player)
    {
        //Movimentação no ar
        float moveInput = Input.GetAxis("Horizontal");
        player.rb.linearVelocity = new Vector2(moveInput * player.Status.Speed, player.rb.linearVelocityY);

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.jumpBufferCounter = player.Status.JumpBuffer;
        }
        else
        {
            player.jumpBufferCounter -= Time.deltaTime;
        }

        if (player.coyoteTimeCounter > 0 && player.jumpBufferCounter > 0)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocityX, player.Status.JumpPower);

            player.jumpBufferCounter = 0;
        }

        //SNAP TAP - Força do pulo baseado no pressionamento de tecla
        if (Input.GetKeyUp(KeyCode.W) && player.rb.linearVelocityY > 0)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocityX, player.Status.JumpPower * 0.5f);

            player.coyoteTimeCounter = 0;
        }
    }
    public override void FixedUpdate(PlayerStateManager player)
    {
        player.ChangeToWalkingState();
        
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