using Unity.Cinemachine;
using UnityEngine;

namespace PlayerManager
{
    public class CameraFollow : MonoBehaviour, ISetCameraFollow
    {
        public CinemachineCamera cameraView;
        public void SetCameraFollow(GameObject player)
        {
            DontDestroyOnLoad(cameraView); //Não permite que a câmera seja destruída
            cameraView.Follow = player.transform; //A câmera segue o jogador
        }
    }
}
