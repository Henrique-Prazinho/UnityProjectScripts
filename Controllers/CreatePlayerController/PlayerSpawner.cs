using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerManager
{
    public class PlayerSpawner : MonoBehaviourPunCallbacks
    {
        private ISetCameraFollow _cameraFollow;
        private IPlayerSetColor _playerSetColor;
        
        private List<string> playerColors = new List<string>()
        {
            "Green",
            "Gray",
            "White",
            "Pink",
            "Blue"
        };

        void Awake()
        {
            //Acesso aos scripts que implementam as interfaces
            _playerSetColor = GetComponent<IPlayerSetColor>();
            _cameraFollow = GetComponent<ISetCameraFollow>();
            DontDestroyOnLoad(this.gameObject);
        }

        public void SpawnPlayer()
        {   
            //Garante só um RestartGame no jogo inteiro, instanciado pelo Master
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("RestartGame", new Vector3(0, -15, 0), quaternion.identity);
            }

            //Instanciação do player
            GameObject player = PhotonNetwork.Instantiate("PlayerPrefab", new Vector3(-21, -5, 0), quaternion.identity);

            if (_cameraFollow != null)
            {
                _cameraFollow.SetCameraFollow(player);
            }

            //Chama o método de cor para todos clientes que entraram e que vão entrar
            photonView.RPC("RpcCallFunc", RpcTarget.AllBufferedViaServer, player.GetComponent<PhotonView>().ViewID, playerColors[Random.Range(0, playerColors.Count)]);
        }

        [PunRPC]
        public void RpcCallFunc(int viewID, string color)
        {
            if (_playerSetColor != null)
            {
                _playerSetColor.SetPlayerColor(viewID, color);
                playerColors.Remove(color);
            }
        }

        public override void OnJoinedRoom()
        {
            SpawnPlayer();
        }
    }
}