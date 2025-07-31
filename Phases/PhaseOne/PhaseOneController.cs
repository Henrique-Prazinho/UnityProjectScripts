using System;
using Enums;
using Photon.Pun;
using UnityEngine;

public class PhaseOneController : BasePhaseController, IObserver
{
    [SerializeField] private Animator floor;
    [SerializeField] private Subject _buttonSubject;
    [SerializeField] private Subject _keySubject;

    private const string PHASE_NAME = "Phase3";

    //PISO
    [PunRPC]
    private void StartFloorAnimation()
    {
        if (floor != null)
        {
            floor.SetBool("buttonPressed", true);
        }
    }

    [PunRPC]
    private void FinishFloorAnimation()
    {
        if (floor != null)
        {
            floor.SetBool("buttonPressed", false);
        }
    }

    protected override void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; //Vincular as cenas dos clientes com o master client

        if (!PhotonNetwork.IsMasterClient) { return; }

        base.Awake(); //Utiliza o Awake da classe abstrata
    }

    //----------Métodos da classe abstrata---------
    protected override void LoadNextPhase()
    {
        PhotonNetwork.LoadLevel(PHASE_NAME); //Carrega a outra cena
    }

    //--------Métodos Observer Pattern---------
    public override void OnEnable()
    {
        //Adiciona a si mesmo na coleção de lista de observadores
        _keySubject.AddObserver(this);
        _buttonSubject.AddObserver(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //Remove a si mesmo na coleção de lista de observadores
        _buttonSubject.RemoveObserver(this);
    }

    public void OnNotify<T>(T data, int? viewID) where T : Enum
    {
        var enumType = typeof(T); // Captura o tipo real do enum genérico T (ex: ButtonState, PlayerState, etc.)
        var enumData = (T)Enum.ToObject(enumType, data); // Converte o valor numérico 'data' para o valor correspondente do enum T

        //--------------BUTTON-------------

        //Verifica se o enumData (Tipo genérico) é ButtonState, se for true retorna o valor do enum para a variável buttonState.
        if (enumData is ButtonState buttonState)
        {
            if (_buttonSubject != null)
            {
                if (buttonState == ButtonState.Pressed)
                {
                    photonView.RPC(nameof(StartFloorAnimation), RpcTarget.All);
                }
                else if (buttonState == ButtonState.Unpressed)
                {
                    photonView.RPC(nameof(FinishFloorAnimation), RpcTarget.All);
                }
            }
        }
    }
}
