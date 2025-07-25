using Enums;
using Photon.Pun;
using UnityEngine;

public class Key : Subject, ICollectible
{
    public void OnCollect(GameObject collider)
    {
        //Ao jogador colidir com a chave
        if (collider.gameObject.CompareTag("Player"))
        {
            photonView.RPC(nameof(SetKeyToPlayer), RpcTarget.All, collider.GetComponent<PhotonView>().ViewID);
        }
    }
    
    /// <summary>
    /// Função que define a chave como filha do player que a coletou. Recebe como parâmetro o viewID do PhotonView e distribui para todos os jogadores a informação.
    /// </summary>
    /// <param name="viewID"></param>
    [PunRPC]
    private void SetKeyToPlayer(int viewID)
    {
        var player = PhotonView.Find(viewID);
        gameObject.transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, 1, 0);
    }
}