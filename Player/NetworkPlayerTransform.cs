using Photon.Pun;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;

public class NetworkPlayerTransform : MonoBehaviourPunCallbacks, IPunObservable
{
    private Vector2 networkPosition; //Posição remota recebida
    private float lerpSpeed = 10; //Velocidade da interpolação
    private Rigidbody2D rb; // Rigidbody do player 

    public override void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.AutomaticallySyncScene = true; //Vincular as cenas dos clientes com o master client
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PhotonNetwork.SerializationRate = 10;
    }

    void Update()
    {
        // Cada player possui esse script anexado, caso não seja o próprio "Owner", todos outros players vão realizar a movimentação de forma interpolarizada. 
        // Resultando na alteração de todos transform no cliente que executa.

        if (!photonView.IsMine)
        {
           rb.position = Vector3.Lerp(rb.position, networkPosition, Time.deltaTime * lerpSpeed);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.linearVelocity);
            //Debug.Log($"Eu sou o player local! {this.GetComponent<PhotonView>().ViewID}");
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext(); //Recebe o Rigidbody Position remoto
            if (rb != null)
            {
                rb.linearVelocity = (Vector2)stream.ReceiveNext(); // Recebe a velocidade do RigidBody remoto

                //LAG COMPENSATION
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                networkPosition += this.rb.linearVelocity * lag;
            }

            //Debug.Log($"Eu sou o player remoto! {this.GetComponent<PhotonView>().ViewID}");
        }
    }
}