using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Photon.Pun;
using System.Threading.Tasks;

public class MovingPlataform : MonoBehaviourPunCallbacks
{
    [SerializeField] private float platformSpeed; //Velocidade da plataforma
    [SerializeField] private Transform pointA; 
    [SerializeField] private Transform pointB;
    private Vector3 nextPoint;

    [Header("Configurações caixa de colisão")]
    [SerializeField] private float boxSize;
    [SerializeField] private float offSetY;
    [SerializeField] private LayerMask playerLayer;
    private Collider2D hitInfo;

    void Start()
    {
        nextPoint = pointB.position;
    }

    public override void OnEnable()
    {
        Physics2D.queriesStartInColliders = false; // Evita de colidir com o próprio objeto
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, platformSpeed * Time.deltaTime);

        //Quando alcançar a próxima posição define um novo valor baseado na expressão ternária
        if (transform.position == nextPoint)
        {
            nextPoint = (nextPoint == pointA.position) ? pointB.position : pointA.position;
        }
    }

    void FixedUpdate()
    {
        hitInfo = Physics2D.OverlapBox
        (
            new Vector2(transform.position.x, transform.position.y - offSetY), // Posição do objeto
            transform.localScale * boxSize, // Tamanho da caixa de colisão do objeto
            0, // Ângulo do objeto
            playerLayer  // Filtro de layer
        );

        if (hitInfo != null)
        {
            MovePlayerWithPlatform(hitInfo.gameObject);
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - offSetY), transform.localScale * boxSize);
    // }

    private void MovePlayerWithPlatform(GameObject player)
    {
        var rb = player.gameObject.GetComponent<Rigidbody2D>();

        rb.position = Vector3.MoveTowards(rb.position, nextPoint, platformSpeed * Time.deltaTime);
    }
}