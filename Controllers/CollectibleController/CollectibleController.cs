using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.GetComponent<ICollectible>() != null) //Se esse objeto implementa a classe concreta
        {
            var collectibleObj = this.gameObject.GetComponent<ICollectible>(); //Acessa o componente que implementa a interface
            collectibleObj.OnCollect(other.gameObject); //Chama o método do objeto em questão
        }
        else { Debug.Log($"O gameobject {this.gameObject.name} não implementa a interface ICollectible!"); }
    }
}