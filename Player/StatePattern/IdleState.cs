using UnityEngine;

public class IdleState : PlayerStates
{
    public override void Enter(PlayerStateManager player)
    {
        Debug.Log("Entrei no Estado de parado!");
    }

    public override void Update(PlayerStateManager player)
    {
        //TRANSIÇÃO PARA O ESTADO WALKING
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            player.ChangeState(player.WalkingState);
        }
        //TRANSIÇÃO PARA O ESTADO DE JUMPING
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.ChangeState(player.JumpingState);
        }
    }
    public override void FixedUpdate(PlayerStateManager player)
    {
        
    }

    public override void Exit(PlayerStateManager player)
    {

    }
}