using System;
using Photon.Pun;
using UnityEngine;

public class Door : MonoBehaviourPunCallbacks, ITriggerInteractable
{
    //Campos de instância
    [SerializeField] private Sprite[] doorSprites; //Vetor para guardar os possíveis sprites da porta

    //Delegates 
    public static Action<GameObject> OnPlayerEnteredDoor; //Action que recebe um Gameobject que vai ser usado no controlador de cada fase
    
    //Propriedades
    public bool DoorIsOpened { get; private set; } = false; //Variável de controle para verificação da abertura da porta

    [PunRPC]
    public void OpenDoor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = doorSprites[0]; //Altera pro primeiro sprite do vetor
        DoorIsOpened = true; //Define a porta como aberta
    }

    [PunRPC]
    public void RequestEnterDoor(int viewID)
    {
        var player = PhotonView.Find(viewID).gameObject;
        OnPlayerEnteredDoor?.Invoke(player); 
    }

    //Métodos que implementam a interface
    public void OnInteract(GameObject collider)
    {
        if (collider.transform.Find("Key")) //Se o objeto que colidiu possuir a chave 
        {
            photonView.RPC(nameof(OpenDoor), RpcTarget.All); //Altera o sprite pra porta aberta e altera o valor da variável para aberta
        }

        if (collider.CompareTag("Player") && DoorIsOpened) //Se o player colidiu e a porta está aberta
        {
            var viewID = collider.GetComponent<PhotonView>().ViewID;
            photonView.RPC(nameof(RequestEnterDoor), RpcTarget.MasterClient, viewID); //Envia um RPC para o master computar que entrou na porta
        }
    }
}
