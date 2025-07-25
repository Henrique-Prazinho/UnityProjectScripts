using Photon.Pun;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour, ITriggerInteractable
{
    public void OnInteract(GameObject collider)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.Destroy(collider);
    }
}
