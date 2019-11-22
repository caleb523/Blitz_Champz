using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Lobby : NetworkLobbyManager
{
    //[SyncVar]
    public GameObject table;
    //[SyncVar]
    GameObject Table;
    LinkedListNode<Player> current;
    public LinkedList<Player> order = new LinkedList<Player>();
    public override void OnLobbyStartHost()
    {
        //table = Resources.Load("Prefabs/Table") as GameObject;
        //Table = Instantiate(table, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public override void OnLobbyServerPlayersReady()
    {
        Manager.PlayerCount = numPlayers;
        ServerChangeScene(playScene);
        
        //NetworkServer.Spawn(Table);
        
        Debug.Log("Should have created table");
    }
    //public override void OnLobbyServerSceneChanged(string sceneName) {

    //}
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer) {
        
        
        //table.GetComponent<Table>().AddPlayer(gamePlayer.GetComponent<Player>());
        Debug.Log("Player added");
        return true;
    }
    
    public void SpawnTable(){
        
        Debug.Log("Should have created table");
    }
}

