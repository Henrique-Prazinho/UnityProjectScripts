using UnityEngine;
using Photon.Pun;

public class PlayerGravity : MonoBehaviourPun
{
    private PlayerCollision _collision;
    private Rigidbody2D _rb;
    [SerializeField] private PlayerStatus _stats;
    
    void Start()
    {
        if (photonView.IsMine)
        {
            _collision = GetComponent<PlayerCollision>();
            _rb = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            HandleGravity();
        }
    }
    private void HandleGravity()
    {
        if (_collision.IsGrounded() && _rb.linearVelocityY <= 0f)
        {
            //Mantém o jogador preso ao chão ajudando em possíveis declives
            _rb.linearVelocityY = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAceleration;
            _rb.linearVelocityY = Mathf.MoveTowards(
                _rb.linearVelocityY, //Valor atual
                -_stats.MaxFallSpeed, //Valor de queda máximo
                inAirGravity * Time.deltaTime //Valor incrementado por frame até atingir o valor de queda máximo
            );
        }
    }
}


