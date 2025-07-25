using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [Header("MOVEMENT")]
    [Tooltip("Velocidade máxima de movimento horizontal do jogador")]
    public float Speed = 10;

    [Header("JUMP")]
    [Tooltip("Velocidade vertical aplicada imediatamente ao pular")]
    public float JumpPower = 10;

    [Tooltip("Tempo máximo após sair da plataforma em que o pulo ainda pode ser executado (coyote time)")]
    public float CoyoteTime = .15f;

    [Tooltip("Tempo durante o qual um pulo pressionado antes de tocar no chão ainda pode ser executado (jump buffer)")]
    public float JumpBuffer = .2f;

    [Header("GRAVITY")]
    [Tooltip("Força aplicada quando o objeto estiver no chão para manter fixo no chão em declives")]
    public float GroundingForce;

    [Tooltip("Capacidade do player de ganhar velocidade estando no ar (In Air Gravity)")]
    public float FallAceleration;

    [Tooltip("Velocidade máxima de queda")]
    public float MaxFallSpeed;
}


