using System.Collections;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

public class PhaseThreeController : BasePhaseController
{
    private PlayerGravity _gravityScriptHandle;
    private PlayerStateManager _stateManagerScriptHandle;
    private PlayerAnimation _animationScriptHandle;
    private PlayerFreeMovement _freeMovementScriptHandle;

    private const string PHASE_NAME = "Phase4";

    protected override void Awake()
    {
        base.Awake();
        ScriptsHandler();
    }

    protected override void LoadNextPhase()
    {
        PhotonNetwork.LoadLevel(PHASE_NAME); //Carrega a próxima cena ao concluir a sala
    }

    private async void Start()
    {
        await Task.Delay(600);
        RestartGameController.Instance.ResetSceneMaster();
    }

    private void ScriptsHandler()
    {
        var myPlayer = new GameObject();

        foreach (var player in PlayersInfo.playerInfoList)
        {
            if (!player.PhotonView.IsMine) continue;

            // Atribuição dos scripts
            _gravityScriptHandle = player.Gameobj.GetComponent<PlayerGravity>();
            _stateManagerScriptHandle = player.Gameobj.GetComponent<PlayerStateManager>();
            _animationScriptHandle = player.Gameobj.GetComponent<PlayerAnimation>();
            _freeMovementScriptHandle = player.Gameobj.GetComponent<PlayerFreeMovement>();

            // Atribuição do player para a flag de controle (DEIXAR EM ESCOPO GLOBAL CASO NECESSÁRIO)
            myPlayer = player.Gameobj;
        }
        if (myPlayer == null) return;

        // Alteração de sprite library
        _animationScriptHandle.Animator.SetBool("Plane", true);

        // Desativação dos scripts
        _gravityScriptHandle.enabled = false;
        _stateManagerScriptHandle.enabled = false;
        _animationScriptHandle.enabled = false; // Desativa a animação para o avião não rotacionar de lado

        // Ativação de scripts
        _freeMovementScriptHandle.enabled = true; // Ativa o script de movimento livre, necessário na fase atual
    }
}

