using System.Linq;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

public class ActivatePlayer : MonoBehaviourPunCallbacks
{
    public static ActivatePlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) //Caso já exista uma instância Singleton e não seja o próprio objeto, destrói o gameobject
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public async Task ActivateAllPlayersAsync()
    {
        GameObject[] players = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None)
        .Where(player => player.gameObject.CompareTag("Player"))
        .ToArray();

        foreach (GameObject player in players)
        {
            player.SetActive(true);
        }

        //Finaliza o método Assíncrono
        await Task.Delay(300); // Aguarda 0.5 Seg 
    }
}