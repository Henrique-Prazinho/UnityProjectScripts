using UnityEngine;

public interface ICollisionInteractable
{
    void OnInteract(GameObject collider);
    void ExitInteraction(GameObject collider);
}
