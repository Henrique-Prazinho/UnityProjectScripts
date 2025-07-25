using UnityEngine;

public class CollisionInteractableController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (this.gameObject.GetComponent<ICollisionInteractable>() != null) //Se esse objeto implementa a classe concreta
        {
            var interactableObj = this.gameObject.GetComponent<ICollisionInteractable>(); //Acessa o componente que implementa a interface
            interactableObj.OnInteract(other.gameObject); //Chama o método do objeto em questão
        }
        else { Debug.Log($"O gameobject {this.gameObject.name} não implementa a interface ICollisionInteractable!"); }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (this.gameObject.GetComponent<ICollisionInteractable>() != null) //Se esse objeto implementa a classe concreta
        {
            var interactableObj = this.gameObject.GetComponent<ICollisionInteractable>(); //Acessa o componente que implementa a interface
            interactableObj.ExitInteraction(other.gameObject); //Chama o método do objeto em questão
        }
        else { Debug.Log($"O gameobject {this.gameObject.name} não implementa a interface ICollisionInteractable!"); }
    }
}