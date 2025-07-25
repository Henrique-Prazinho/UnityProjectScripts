using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

public class Teleporter : MonoBehaviourPunCallbacks
{
    public static Teleporter Instance { get; private set; }
    public int timeExecutedRPC = 0;
    public int limitExecutionRPC = 1;

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

    private async Task AllPlayersReady(GameObject[] players)
    {
        if (players.Length == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            await Task.Yield();
        }
    }

    [PunRPC]
    public void ResetExecutedRPCall()
    {
        timeExecutedRPC = 0;
    }

    /// <summary>
    /// Método acessível globamente via instância estática. Chama o RPC que cria um vetor com todos objetos com a tag "Player", itera sobre cada um teleportando para uma posição aleatória.
    /// </summary>
    [PunRPC]
    public async void RPCTeleportAllPlayersAsync()
    {
        await ActivatePlayer.Instance.ActivateAllPlayersAsync(); // Espera os jogadores estarem ativos 

        //Permite executar apenas uma vez por cliente
        if (timeExecutedRPC < limitExecutionRPC)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // Array com todos jogadores
            List<PhotonView> playersViews = new List<PhotonView>(); // PhotonView de cada jogador 

            await AllPlayersReady(players); //Espera a lista de jogadores condizer com a quantidade dentro da sala

            foreach (GameObject player in players)
            {
                //Pega todos photons views de todos jogadores e adiciona na lista
                var view = player.GetComponent<PhotonView>();
                playersViews.Add(view);
            }

            //Itera sobre cada photonview existente na lista
            for (int i = 0; i < playersViews.Count; i++)
            {
                //Caso o photonview iterado seja do player ele é teleportado
                if (playersViews[i].IsMine)
                {
                    float vectX = Random.Range(-25, -21);
                    float vectY = Random.Range(-6, -3);
                    players[i].transform.position = new Vector3(vectX, vectY, 0);
                }
            }
        }
        timeExecutedRPC++;
    }
}
