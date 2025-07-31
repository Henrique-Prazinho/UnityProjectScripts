using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public static class PlayersInfo
{
    /// <summary>
    /// Lista contendo as informações de cada player: Cor, PhotonView, Gameobject.
    /// </summary>
    public static List<PlayerInformation> playerInfoList { get; private set; } = new List<PlayerInformation>();
    
    /// <summary>
    /// Método para adicionar o player a uma lista pública de informações.
    /// </summary>
    public static void AddPlayerToList(string color, PhotonView photonView, GameObject playerGameobject)
    {
        playerInfoList.Add(new PlayerInformation
        {
            Color = color,
            PhotonView = photonView,
            Gameobj = playerGameobject
        });
    }
}

public struct PlayerInformation
{
    public string Color;
    public PhotonView PhotonView;
    public GameObject Gameobj;
}