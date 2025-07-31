using Photon.Pun;
using UnityEngine;

public class NetworkPlayerTransform : MonoBehaviourPun, IPunObservable
{
    private Vector2 networkPosition;
    private Vector2 currentVelocity;
    private Rigidbody2D rb;
    private float lerpSpeed = 10;
    
    public void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.AutomaticallySyncScene = true; //Vincular as cenas dos clientes com o master client
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.SendRate = 30;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            float distance = Vector2.Distance(rb.position, networkPosition);

            // Se a distância entre o valor atual ao valor remoto for muito grande, atualiza sem interpolar (AO RESETAR CENA É NECESSÁRIO ESSE CÓDIGO)
            if (distance > 5)
            {
                rb.position = networkPosition; // Teleporta
            }

            else
            {
                // Interpolação
                Vector2 smoothPos = Vector2.SmoothDamp(rb.position, networkPosition, ref currentVelocity, 1f / lerpSpeed, Mathf.Infinity, Time.fixedDeltaTime);
                rb.MovePosition(smoothPos);
            }
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

            // LAG COMPENSATION - EXTRAPOLAÇÃO
            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition = receivedPos + receivedVel * lag;
        }
    }
}
