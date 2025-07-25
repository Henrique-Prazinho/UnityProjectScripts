using UnityEngine;

public class TriggerInteractableController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.GetComponent<ITriggerInteractable>() != null) //Se esse objeto implementa a classe concreta
        {
            var interactableObj = this.gameObject.GetComponent<ITriggerInteractable>(); //Acessa o componente que implementa a interface
            interactableObj.OnInteract(other.gameObject); //Chama o método do objeto em questão
        }
        else { Debug.Log($"O gameobject {this.gameObject.name} não implementa a interface ITriggerInteractable!"); }
    }
}