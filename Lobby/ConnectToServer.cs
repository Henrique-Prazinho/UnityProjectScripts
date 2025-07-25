using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Faz a conexão 
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); //Junta-se ao lobby com os outros jogadores
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby"); //Carrega a cena responsável por conter a UI de criação de sala e juntar-se
    }
}
