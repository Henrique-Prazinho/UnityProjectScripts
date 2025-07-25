using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (this.gameObject.GetComponent<IAnimation>() != null) //Se esse objeto implementa a classe concreta
        {
            var animator = this.gameObject.GetComponent<IAnimation>(); //Acessa o componente que implementa a interface
            animator.OnCollisionEnterAnimation(other.gameObject);
        }
        else { Debug.Log($"O gameobject {other.gameObject.name} não implementa a interface IAnimation!"); }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (this.gameObject.GetComponent<IAnimation>() != null) //Se esse objeto implementa a classe concreta
        {
            var animator = this.gameObject.GetComponent<IAnimation>(); //Acessa o componente que implementa a interface
            animator.OnCollisionExitAnimation(other.gameObject);
        }
        else { Debug.Log($"O gameobject {other.gameObject.name} não implementa a interface IAnimation!"); }
    }
}