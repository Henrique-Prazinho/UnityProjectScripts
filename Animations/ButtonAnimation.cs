using UnityEngine;

public class ButtonAnimation : MonoBehaviour, IAnimation
{
    private Animator buttonAnimator;
    
    void Start()
    {
        buttonAnimator = this.GetComponent<Animator>();
    }

    //Métodos concreto da interface
    public void OnCollisionEnterAnimation(GameObject collider)
    {
        if (collider.CompareTag("Objects"))
        {
            buttonAnimator.SetBool("buttonPressed", true);
        }
    }

    public void OnCollisionExitAnimation(GameObject collider)
    {
        if (collider.CompareTag("Objects"))
        {
            buttonAnimator.SetBool("buttonPressed", false);
        }
    }
}
