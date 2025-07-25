using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameController : MonoBehaviourPunCallbacks
{
    public static RestartGameController Instance { get; private set; }
    private string cachedActualScene;
    private int timeExecutedRPC = 0;
    private int limitExecutionRPC = 1;

    private const string EMPTY_SCENE_NAME = "EmptyScene";
    private const string PLAYER_TAG = "Player";

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

    IEnumerator ResetRPCCount()
    {
        yield return new WaitForSeconds(2f);
        timeExecutedRPC = 0;
    }

    /// <summary>
    /// Método estático acessível globalmente via instância Singleton. Aciona o RPC no cliente Master para resetar a cena atual.
    /// </summary>
    public void ResetSceneMaster()
    {
        Instance.photonView.RPC(nameof(RPCResetSceneMaster), RpcTarget.MasterClient); //Envia para o master client
    }

    private async Task ReloadSceneAsync()
    {
        PhotonNetwork.LoadLevel(EMPTY_SCENE_NAME);
        
        cachedActualScene = SceneManager.GetActiveScene().name; //Guarda a fase atual assim que é carregada

        PhotonNetwork.LoadLevel(cachedActualScene); //Recarrega a cena atual

        Teleporter.Instance.photonView.RPC(nameof(Teleporter.RPCTeleportAllPlayersAsync), RpcTarget.All); //Teleporta os jogadores para o começo da fase

        //Não deixa executar novamente até a liberação pela corrotina
        timeExecutedRPC++;
        StartCoroutine(nameof(ResetRPCCount));

        await Task.Yield();
    }

    [PunRPC]
    private void RPCResetSceneMaster()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (timeExecutedRPC < limitExecutionRPC)
        {
            // Destrói todas as chaves que ainda estiverem atreladas aos players
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                var key = player.transform.Find("Key");

                if (key != null)
                {
                    PhotonNetwork.Destroy(key.GetComponent<PhotonView>());
                }
            }
            Teleporter.Instance.photonView.RPC(nameof(Teleporter.ResetExecutedRPCall), RpcTarget.All); //Reseta a chamada RPC quando a sala for reiniciada em todos clientes
            _ = ReloadSceneAsync(); // Não espera o resultado, já que não é necessário
        }
    }
}