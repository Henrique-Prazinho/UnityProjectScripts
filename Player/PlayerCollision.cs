using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
public class PlayerCollision : MonoBehaviourPun
{
    [Header("Configuração de caixa de colisão")]
    public Vector2 boxSize; //Tamanho da caixa de verificação de colisão
    public float castDistance; //Distância entre player e objeto que verificará a colisão
    
    [SerializeField] private LayerMask groundLayer; //Layer em que os objetos serão verificados
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private LayerMask objectsLayer;

    private int allMasks;

    void Start()
    {
        if (!photonView.IsMine) { return; }
        Physics2D.queriesStartInColliders = false; // Não permite colidir consigo mesmo
        allMasks = groundLayer | playerLayer | objectsLayer | boxLayer; //Se colidir com as duas ou uma das duas retorna true
    }

    //Métodos para verificação de colisão
    public bool IsGrounded()
    {
        if (photonView.IsMine)
        {
            return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, allMasks);
        }
        return false;
    }

    //-------------VERIFICAR CAIXA DE COLISÃO------------- 

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    // }
}
