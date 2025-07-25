using UnityEngine;

public interface IAnimation
{
    void OnCollisionEnterAnimation(GameObject collider);
    void OnCollisionExitAnimation(GameObject collider);
}
