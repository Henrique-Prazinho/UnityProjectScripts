using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
public class RoomController : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomInput;
    public TMP_InputField joinRoomInput;

    //CRIAR SALA
    public void CreateRoom()
    {
        if (createRoomInput.text.Length > 0) //Se o campo não estiver vazia
        {
            PhotonNetwork.CreateRoom(createRoomInput.text); //Informa o nome da sala a ser criado recebendo o valor do input como parâmetro
        }
    }

    //JUNTAR-SE A SALA
    public void JoinRoom()
    {
        if (joinRoomInput.text.Length > 0) //Se o campo não estiver vazia
        {
            PhotonNetwork.JoinRoom(joinRoomInput.text); //Informa o nome da sala a se juntar, recebendo o valor do input como parâmetro  
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true; //Faz a sincronização da cena de todos clientes com o master
        SceneManager.LoadScene("Phase1"); //Carrega a cena da fase 1
    }
}
