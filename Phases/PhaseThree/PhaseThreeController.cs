using Photon.Pun;

public class PhaseThreeController : BasePhaseController
{
    private const string PHASE_NAME = "Phase4";

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LoadNextPhase()
    {
        PhotonNetwork.LoadLevel(PHASE_NAME); //Carrega a próxima cena ao concluir a sala
    }

    private void Start()
    {
        Teleporter.Instance.RPCTeleportAllPlayersAsync();
        foreach (var player in PlayersInfo.playerInfoList)
        {
            if (!player.PhotonView.IsMine) return;

            // Desativação dos scripts
            var gravityScript = player.Gameobj.GetComponent<PlayerGravity>().enabled = false;
            var stateManagerScript = player.Gameobj.GetComponent<PlayerStateManager>().enabled = false;

            // Ativação dos scripts
            var freeMovementScript = player.Gameobj.GetComponent<PlayerFreeMovement>().enabled = true;

            // Troca de sprite resolver
            player.Gameobj.GetComponent<PlayerAnimation>().Animator.SetBool("Plane", true);                  
        }
    }
}

