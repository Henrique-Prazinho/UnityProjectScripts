using System.Threading.Tasks;
using UnityEngine;

public class FinishLine : MonoBehaviour, ITriggerInteractable
{
    public void OnInteract(GameObject collider)
    {
        _ = OnInteractAsync(collider);
    }

    public async Task OnInteractAsync(GameObject collider)
    {
        await MoveLerpedToPosition(collider);
    }

    private async Task MoveLerpedToPosition(GameObject collider)
    {
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();

        var actualPosition = rb.transform.position;
        var endPosition = new Vector3(actualPosition.x + 5, actualPosition.y, 0);

        while (rb.position != (Vector2)endPosition)
        {
            actualPosition = rb.position; // Atualiza com o novo valor
            rb.position = Vector3.Lerp(actualPosition, endPosition, Time.deltaTime * 0.8f); 
            await Task.Yield(); // Retoma o controle para unity, evitando crashar
        }
        await Task.Yield();
    }
}