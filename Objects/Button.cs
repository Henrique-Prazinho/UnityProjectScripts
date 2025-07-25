using UnityEngine;
using Enums;

public class Button : Subject, ICollisionInteractable
{
    [SerializeField] LayerMask boxLayer;
    [SerializeField] private float radius;
    [SerializeField] private float castDistance;

    public void ExitInteraction(GameObject collider)
    {
        if (!IsBoxColliding())
        {
            NotifyObservers(ButtonState.Unpressed, null);
        };
    }

    public void OnInteract(GameObject collider)
    {
        if (IsBoxColliding())
        {
            NotifyObservers(ButtonState.Pressed, null);
        };
    }

    private bool IsBoxColliding()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - castDistance), radius, boxLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - castDistance), radius);
    }
}
