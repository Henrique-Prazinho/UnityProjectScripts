using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace PlayerManager
{
    public class PlayerSetColor : MonoBehaviour, IPlayerSetColor
    {
        public List<SpriteLibraryAsset> spritesLibraries = new List<SpriteLibraryAsset>(); //Lista que guarda os SpritesLibrarys

        public void SetPlayerColor(int playerViewID, string color)
        {
            GameObject player = PhotonView.Find(playerViewID).gameObject;

            // Adicionar o jogador a lista de informações de players
            PlayersInfo.AddPlayerToList(color, player.GetComponent<PhotonView>(), player);

            //Setar o SpriteLibrary baseado na cor escolhida
            var setSpriteLibrary = spritesLibraries.First(library => library.name.Contains(color));
            player.GetComponent<SpriteLibrary>().spriteLibraryAsset = setSpriteLibrary;
        }
    }
}
