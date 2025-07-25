public abstract class PlayerStates
{
    public abstract void Enter(PlayerStateManager player);
    public abstract void Update(PlayerStateManager player);
    public abstract void FixedUpdate(PlayerStateManager player);
    public abstract void Exit(PlayerStateManager player);

}