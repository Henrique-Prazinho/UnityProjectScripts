using Photon.Pun;
using UnityEngine;

public class PlayerAnimation : MonoBehaviourPun
{
    private int cachedMyPhotonview;
    private SpriteRenderer cachedRendererSprite;    

    public Animator Animator { get; private set;}

    void Start()
    {
        if (photonView.IsMine)
        {
            cachedMyPhotonview = photonView.ViewID;
            cachedRendererSprite = this.gameObject.GetComponent<SpriteRenderer>();

            Animator = this.GetComponent<Animator>(); //Componente animator do player
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            OnKeyPressed();
        }
    }

    private void WalkingAnimation()
    {
        Animator.SetBool("IsWalking", true);
    }

    private void IdleAnimation()
    {
        Animator.SetBool("IsWalking", false);
    }

    private void OnKeyPressed()
    {
        if (Input.GetKey(KeyCode.D))
        {
            WalkingAnimation();
            if (cachedRendererSprite.flipX)
            {
                photonView.RPC(nameof(TurnSpriteSide), RpcTarget.All, false, cachedMyPhotonview);
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            WalkingAnimation();
            if (!cachedRendererSprite.flipX)
            {
                photonView.RPC(nameof(TurnSpriteSide), RpcTarget.All, true, cachedMyPhotonview);
            }
        }

        else
        {
            IdleAnimation();
        }
    }

    [PunRPC]
    public void TurnSpriteSide(bool value, int viewID)
    {
        var rendererSprite = PhotonView.Find(viewID).gameObject.GetComponent<SpriteRenderer>();
        rendererSprite.flipX = value;
    }
}
