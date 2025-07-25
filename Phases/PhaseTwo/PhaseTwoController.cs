using Photon.Pun;

public class PhaseTwoController : BasePhaseController
{
    private const string PHASE_NAME = "Phase3";

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
    }
}

