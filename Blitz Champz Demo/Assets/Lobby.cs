using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Lobby : NetworkLobbyManager
{
        public GameObject table;
        public override void OnLobbyServerPlayersReady()
        {
            Manager.PlayerCount = numPlayers;
            ServerChangeScene(playScene);
            
            
            Debug.Log("Should have created table");
        }
        //public override void OnLobbyServerSceneChanged(string sceneName) {

        //}
        public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer) {
            var Table = Instantiate(table, new Vector3(0, 0, 0), Quaternion.identity);
            NetworkServer.Spawn(Table);
            Table.GetComponent<Table>().AddPlayer(gamePlayer.GetComponent<Player>());
            Debug.Log("Player added");
            return true;
        }
        
        public void SpawnTable(){
            
            Debug.Log("Should have created table");
        }
    }

