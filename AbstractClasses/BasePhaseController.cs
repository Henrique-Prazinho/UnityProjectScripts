using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class BasePhaseController : MonoBehaviourPunCallbacks
{
    protected HashSet<GameObject> playersSet = new HashSet<GameObject>(); //Conjunto que guarda os players que entraram na porta

    protected abstract void LoadNextPhase(); //Força implementar a função para alterar de cena em todos controladores

    private bool NextLevelLoaded = false;

    protected virtual void Awake()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        Door.OnPlayerEnteredDoor += EnteredDoor;
    }

    //Método inscrito no delegate do script da porta que controla se todos jogadores entraram ou não
    private void EnteredDoor(GameObject player)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        var key = player.transform.Find("Key"); //Verifica se o player que entrou possuía a chave ou não

        if (key != null) //Se o player possuía a chave
        {
            PhotonNetwork.Destroy(key.GetComponent<PhotonView>()); //Destrói a chave
        }

        playersSet.Add(player); //Adiciona o player ao conjunto de players que acessaram a porta
        photonView.RPC(nameof(DeactivatePlayer), RpcTarget.All, player.GetComponent<PhotonView>().ViewID); //Desativa o player em todos clientes

        if (playersSet.Count == PhotonNetwork.CurrentRoom.PlayerCount && !NextLevelLoaded) //Se a quantidade de player que entrou na porta é o mesmo de players na sala
        {
            NextLevelLoaded = true;
            LoadNextPhase(); //Chama o método implementado nas classes derivadas
        }
    }

    [PunRPC]
    protected void DeactivatePlayer(int viewID) //Photon só suporta tipos primitivos: int, float, bool 
    {
        GameObject player = PhotonView.Find(viewID)?.gameObject;
        player.SetActive(false); //Desativa o player que entrou na porta
    }

    protected virtual void OnDestroy()
    {
        //Desinscreve ao reiniciar a cena, evitando que faça referência ao Action da cena antiga
        if (PhotonNetwork.IsMasterClient)
        {
            Door.OnPlayerEnteredDoor -= EnteredDoor;
        }
    }
}