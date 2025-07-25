using UnityEngine;

public class FellOffTheMap : MonoBehaviour, ITriggerInteractable
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void OnInteract(GameObject collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            RestartGameController.Instance.ResetSceneMaster();
        }
    }
}
