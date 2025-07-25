using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float pipeSpeed;
    [SerializeField] private float pipeIncreaseSpeed;
    private Rigidbody2D _rb;

    void Start()
    {
        ProceduralGeneration.OnIncreaseSpeed += IncreasePipeSpeed;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        // Define pela primeira vez a velocidade
        MovePipe();
    }
    
    void OnDestroy()
    {
        ProceduralGeneration.OnIncreaseSpeed -= IncreasePipeSpeed;
    }

    private void MovePipe()
    {
        _rb.linearVelocityX = -pipeSpeed;
    }

    private void IncreasePipeSpeed()
    {
        pipeSpeed += pipeIncreaseSpeed;
        MovePipe(); // Chama a função com o novo valor 
    }
}
