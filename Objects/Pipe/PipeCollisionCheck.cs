using UnityEngine;

public class PipeCollisionCheck : MonoBehaviour, ITriggerInteractable
{
    private const string PLAYER_TAG = "Player";

    public void OnInteract(GameObject collider)
    {
        if (collider.CompareTag(PLAYER_TAG))
        {
            RestartGameController.Instance.ResetSceneMaster();
        }
    }
}
