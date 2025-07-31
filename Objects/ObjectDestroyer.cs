using Photon.Pun;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour, ITriggerInteractable
{
    public void OnInteract(GameObject collider)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.Destroy(collider); // Se não tiver filho destrói o objeto
        
        if (collider.transform.parent != null)
        {
            PhotonNetwork.Destroy(collider.transform.parent.gameObject); // Se colidir com um objeto filho, destrói o objeto pai
        }
    }
}
