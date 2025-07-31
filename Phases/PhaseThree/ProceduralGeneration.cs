using System;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Photon.Pun;

public class ProceduralGeneration : MonoBehaviour
{
    [Header("Quantidade de objetos a serem instanciados")]
    [SerializeField] private int numberOfPipes; 
    [SerializeField] private int numberOfNikos;

    [Header("Objetos a serem instanciados")]
    [SerializeField] private GameObject cano;
    [SerializeField] private GameObject niko;
    [SerializeField] private GameObject finishLine;

    [Header("Posição do Spawnpoint da linha de chegada")]
    [SerializeField] private Transform finishLineSpawnpoint;

    [Header("Configuração dos objetos")]
    [SerializeField] private float heightRange;
    [SerializeField] private float gapBetweenObjects;

    [Header("Tempo para o evento de aumento de velocidade dos objetos")]
    [SerializeField] private float timeToIncreaseSpeed;

    [Header("Duração do nível para instanciação da linha de chegada")]
    [SerializeField] private float _levelTime; // Duração do nível para instanciação da linha de chegada

    private bool finishLineAlreadyGenerated = false; // Flag de controle

    private float _timeToRaiseEvent; // Intervalo de tempo em que o evento de aumento de velocidade é ativado

    public static event Action OnIncreaseSpeed;

    private void GeneratePipe()
    {
        Transform lastObjectCreated = this.gameObject.transform;

        for (int i = 0; i < numberOfPipes; i++)
        {
            var objectHandle = PhotonNetwork.Instantiate(cano.name, new Vector2(lastObjectCreated.transform.position.x + gapBetweenObjects, Random.Range(-heightRange, heightRange)), quaternion.identity);
            lastObjectCreated = objectHandle.transform;
        }
    }

    private async void GenerateNiko()
    {
        for (int i = 0; i < numberOfNikos; i++)
        {
            var objectHandle = PhotonNetwork.Instantiate(niko.name, new Vector2(transform.position.x + gapBetweenObjects, Random.Range(-heightRange, heightRange)), quaternion.identity);
            objectHandle.transform.localScale = new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), 0f);
            await Task.Delay(3000);
        }
    }

    private void GenerateFinishLine()
    {
        PhotonNetwork.Instantiate(nameof(finishLine), finishLineSpawnpoint.position, quaternion.identity);
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        _timeToRaiseEvent += Time.deltaTime;
        _levelTime -= Time.deltaTime;

        if (_timeToRaiseEvent >= timeToIncreaseSpeed)
        {
            OnIncreaseSpeed?.Invoke();
            _timeToRaiseEvent = 0;
        }

        if (_levelTime <= 0 && !finishLineAlreadyGenerated)
        {
            finishLineAlreadyGenerated = true;
            GenerateFinishLine();
        }
    }

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        GeneratePipe(); 
        GenerateNiko(); 
    }
}
