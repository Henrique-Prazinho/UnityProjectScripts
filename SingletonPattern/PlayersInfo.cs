using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public static class PlayersInfo
{
    public static List<PlayerInformation> playerInfoList { get; private set; } = new List<PlayerInformation>();
    
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