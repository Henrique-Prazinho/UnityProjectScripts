using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class NetworkTransform : MonoBehaviourPun, IPunObservable
{
    private Vector2 networkPosition;
    private Vector2 currentVelocity;
    private Rigidbody2D rb;
    private float lerpSpeed = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        networkPosition = rb.position;
    }

    void FixedUpdate()
    {
        if (!this.photonView.IsMine)
        {
            Vector2 smoothPos = Vector2.SmoothDamp(rb.position, networkPosition, ref currentVelocity, 1f / lerpSpeed, Mathf.Infinity, Time.fixedDeltaTime);
            rb.MovePosition(smoothPos);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.linearVelocity);
        }
        else
        {
            Vector2 receivedPos = (Vector2)stream.ReceiveNext();
            Vector2 receivedVel = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition = receivedPos + receivedVel * lag;
        }
    }
}
