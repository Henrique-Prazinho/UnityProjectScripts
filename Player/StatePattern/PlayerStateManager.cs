using Photon.Pun;
using UnityEngine;
public class PlayerStateManager : MonoBehaviourPun
{
    public Rigidbody2D rb { get; private set; } //Componente Rigidbody2D do player
    public PlayerCollision Collision { get; private set; } //Script de Colisão do player
    public PlayerStatus Status; //Scriptable Object que contém todos atributos do player
    public float jumpBufferCounter; //Tempo para pulo antes de encostar no chão
    public float coyoteTimeCounter; //Tempo para pulo depois de sair de colisão com o chão

    public IdleState IdleState { get; private set; } = new IdleState();
    public WalkingState WalkingState { get; private set; } = new WalkingState();
    public JumpingState JumpingState { get; private set; } = new JumpingState(); 
    public PlayerStates currentState;

    void Start()
    {
        if (photonView.IsMine)
        {
            rb = this.gameObject.GetComponent<Rigidbody2D>();
            Collision = this.gameObject.GetComponent<PlayerCollision>();

            currentState = IdleState;
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            currentState.Update(this);
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            currentState.FixedUpdate(this);
        }
    }

    /// <summary>
    /// Sai do estado atual e altera para o próximo estado informado no parâmetro da função
    /// </summary>
    /// <param name="newState">Instância de estado</param>
    public void ChangeState(PlayerStates newState)
    {
        currentState.Exit(this);

        currentState = newState;
        currentState.Enter(this);
    }

    /// <summary>
    /// Executa sempre que o código é recompilado ou feito alguma alteração via inspector. Utilizado para verificar se o script foi anexado corretamente.
    /// </summary>
    private void OnValidate()
    {
        if (Status == null)
        {
            Debug.Log("O Script PlayerStatus não foi anexado via inspector!");
        }
    }

    //---------------------------------------------TRANSIÇÕES DE ESTADOS--------------------------------------------------

    /// <summary>
    /// Função responsável de alterar o estado para "Walking". Verifica se o player está no chão, Input de tecla "A" e "D", e verificação do Vetor Y
    /// O qual verifica se o objeto está ainda pulando.
    /// </summary>
    public void ChangeToWalkingState()
    {
        if (Input.GetKey(KeyCode.A) && Collision.IsGrounded() && rb.linearVelocityY == 0 || Input.GetKey(KeyCode.D) && Collision.IsGrounded() && rb.linearVelocityY == 0)
        {
            ChangeState(WalkingState);
        }
    }
    /// <summary>
    /// Função responsável de alterar o estado para "Jumping". Verifica se o player pressionou a tecla "W", alterando seu estado e inicializando o BufferCounter
    /// evitando cair no IF que define o valor do buffer pela primeira vez.
    /// </summary>
    public void ChangeToJumpingState()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ChangeState(JumpingState);
            jumpBufferCounter = Status.JumpBuffer;
        }
    }
}