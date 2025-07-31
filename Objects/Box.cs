using Photon.Pun;
using UnityEngine;

public class Box : MonoBehaviourPun
{
    private const string PLAYER_TAG = "Player";

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(PLAYER_TAG)) return;
        var playerPhotonview = other.gameObject.GetComponent<PhotonView>();

        if (playerPhotonview == null)
        {
            Debug.Log("O jogador não possui um Photonview válido!");
            return;
        }

        photonView.TransferOwnership(playerPhotonview.Owner);
    }
}