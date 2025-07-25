using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private float _timeToShoot;

    private float _time;

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        _time += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        InstatiateBullets();
    }

    private void InstatiateBullets()
    {
        if (_time >= _timeToShoot)
        {
            PhotonNetwork.Instantiate("Bullet", ChooseRandomSpawnBullet(), quaternion.identity);
            _time = 0;
        }
    }

    /// <summary>
    /// Acessa um índice aleatório do array de spawnpoints, e retorna o position(Vector3) desse objeto.
    /// </summary>
    /// <returns></returns>
    private Vector3 ChooseRandomSpawnBullet()
    {
        var randomIndex = Random.Range(0, spawnpoints.Length);

        return spawnpoints[randomIndex].position;
    }
}
