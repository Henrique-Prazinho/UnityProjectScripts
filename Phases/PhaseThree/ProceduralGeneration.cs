using System;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGeneration : MonoBehaviour
{
    [Header("Quantidade de objetos a serem instanciados")]
    [SerializeField] private int numberOfPipes; 
    [SerializeField] private int numberOfNikos;

    [Header("Objetos a serem instanciados")]
    public GameObject cano;
    public GameObject Niko;

    [Header("Configuração dos objetos")]
    [SerializeField] private float heightRange;
    [SerializeField] private float gapBetweenObjects;

    [Header("Aumento de velocidade dos objetos")]
    [SerializeField] private float timeToIncreaseSpeed;

    private float _time;

    public static event Action OnIncreaseSpeed;

    private void GeneratePipe()
    {
        Transform lastObjectCreated = this.gameObject.transform;

        for (int i = 0; i < numberOfPipes; i++)
        {
            var objectHandle = Instantiate(cano, new Vector2(lastObjectCreated.transform.position.x + gapBetweenObjects, Random.Range(-heightRange, heightRange)), quaternion.identity);
            lastObjectCreated = objectHandle.transform;
        }
    }

    private async void GenerateNiko()
    {
        for (int i = 0; i < numberOfNikos; i++)
        {
            var objectHandle = Instantiate(Niko, new Vector2(transform.position.x + gapBetweenObjects, Random.Range(-heightRange, heightRange)), quaternion.identity);
            objectHandle.transform.localScale = new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), 0f);
            await Task.Delay(3000);
        }
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= timeToIncreaseSpeed)
        {
            OnIncreaseSpeed?.Invoke();
            _time = 0;
        }
    }

    void Start()
    {
        GeneratePipe();
        GenerateNiko();
    }
}
