using UnityEngine;

public class Bullet : MonoBehaviour, ITriggerInteractable
{
    private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocityX = -_bulletSpeed;
    }
    
    public void OnInteract(GameObject collider)
    {
        if (collider.CompareTag("Player"))
        {
            RestartGameController.Instance.ResetSceneMaster();
        }
    }

}
