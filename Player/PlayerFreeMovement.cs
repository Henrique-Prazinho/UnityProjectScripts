using Photon.Pun;
using UnityEngine;

public class PlayerFreeMovement : MonoBehaviourPun
{
    private Rigidbody2D _rb;
    [SerializeField] private PlayerStatus status;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float moveInputX = Input.GetAxis("Horizontal");
            float moveInputY = Input.GetAxis("Vertical");

            _rb.linearVelocity = new Vector2(moveInputX * status.Speed, moveInputY * status.Speed);
        }
    }
}