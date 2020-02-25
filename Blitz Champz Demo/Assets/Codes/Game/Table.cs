using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

/*
This is where most of the game is handled. A lot of actions are called here from card/player objects
*/

public class Table : MonoBehaviourPunCallbacks
{
    
    public Deck draw_deck;
    public List<GameObject> discard; //A list of discarded cards. This is never actually used but was added originally in anticipation 
    //that there may be a scenario where discarded cards need to be re-added to the draw deck. This is an impossible situation to reach however
    public Offensive_Card last_card; //This keeps track of the last offensive card that was played so that a defensive card can check to see if it can be played
    //and know what card to remove
    public int player_count;
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public Text p1; //the score display for player 1
    public Text p2; //the score display for player 2
    public Text p3; //the score display for player 3
    public Text p4; //the score display for player 4
    public Player current_player; //The player whose turn it is
    public GameObject gameOver; //This is the gameover screen, it is always present in the scene but is not enabled until the game ends
    private bool reversed = false; //Which direction play is rotating. False is clockwise and the game starts out this way
    public bool ready = true;
    public LinkedListNode<Player> current; //Node for the linked list below
    public LinkedList<Player> order = new LinkedList<Player>(); //A linked list of players that is used for determining the next player
    public LinkedList<Photon.Realtime.Player> temp_order = new LinkedList<Photon.Realtime.Player>();
    public LinkedListNode<Photon.Realtime.Player> temp_current;
    void Start() {
        if (PlayerManager.LocalPlayerInstance == null && PhotonNetwork.IsMasterClient) {
            foreach(Photon.Realtime.Player p in PhotonNetwork.PlayerList) {
                temp_order.AddLast(p);
            }
            temp_current = temp_order.First;
            for (int a = 0; a < temp_order.Count; a++) {
                if (temp_current.Value == PhotonNetwork.LocalPlayer) {
                    Debug.Log("This player equals this player");
                    GameObject player1_object = PhotonNetwork.Instantiate("player", new Vector3(-8f, -4.4f, 1f), transform.rotation);
                    player1 = player1_object.GetComponent<Player>();
                    order.AddLast(player1);
                    player1.table = this;
                    temp_current = temp_current.Next ?? temp_current.List.First;
                    for (int b = 0; b < temp_order.Count - 1; b++) {
                        if (b == 0) {
                            GameObject player2_object = PhotonNetwork.Instantiate("player", new Vector3(-8f, 4.4f, 1f), Quaternion.Euler(0,0,180f));
                            player2 = player2_object.GetComponent<Player>();
                            order.AddLast(player2);
                            player2.table = this;
                            player2_object.GetComponent<PhotonView>().TransferOwnership(temp_current.Value);
                            temp_current = temp_current.Next ?? temp_current.List.First;
                        } else if (b == 1) {
                            GameObject player3_object = PhotonNetwork.Instantiate("player", new Vector3(8f, 4.4f, 1f), Quaternion.Euler(0,0,180f));
                            player3 = player3_object.GetComponent<Player>();
                            order.AddLast(player3);
                            player3.table = this;
                            player3_object.GetComponent<PhotonView>().TransferOwnership(temp_current.Value);
                            temp_current = temp_current.Next ?? temp_current.List.First;
                        } else if(b == 2) {
                            GameObject player4_object = PhotonNetwork.Instantiate("player", new Vector3(8f, -4.4f, 1f), transform.rotation);
                            player4 = player4_object.GetComponent<Player>();
                            order.AddLast(player4);
                            player4.table = this;
                            player4_object.GetComponent<PhotonView>().TransferOwnership(temp_current.Value);
                            temp_current = temp_current.Next ?? temp_current.List.First;
                        }
                    } break;
                } else {
                    temp_current = temp_current.Next ?? temp_current.List.First;
                }
            }
            if (order.Count == 2){
                photonView.RPC("UpdateTable", RpcTarget.Others, player1.gameObject.GetComponent<PhotonView>().ViewID, player2.gameObject.GetComponent<PhotonView>().ViewID);
                Debug.Log("Table updated");
            } else if (order.Count == 3) {
                photonView.RPC("UpdateTable", RpcTarget.Others, player1.gameObject.GetComponent<PhotonView>().ViewID, player2.gameObject.GetComponent<PhotonView>().ViewID, player3.gameObject.GetComponent<PhotonView>().ViewID);
            } else if (order.Count == 4) {
                photonView.RPC("UpdateTable", RpcTarget.Others, player1.gameObject.GetComponent<PhotonView>().ViewID, player2.gameObject.GetComponent<PhotonView>().ViewID, player3.gameObject.GetComponent<PhotonView>().ViewID, player4.gameObject.GetComponent<PhotonView>().ViewID);

            }
            player_count = order.Count;
            Create_Deck();
            current = order.Last;
            current_player = current.Value;
            StartCoroutine(Wait_For_Deck());
        }
        
    }
    //RPCs are "Remote Procedure Calls" and are the easiest way to synchronize the game across players when using Photon
    //These may show as having "0 references" in your code editor but they are being called
    [PunRPC] 
    void UpdateTable(int a, int b) { //Overloaded function for different amounts of players
        player1 = PhotonView.Find(a).gameObject.GetComponent<Player>(); //Photon assigns an ID to every object when it is created.
        //We use these IDs to find the player gameobjects then access the Player() module
        player2 = PhotonView.Find(b).gameObject.GetComponent<Player>();
        player1.table = this; player2.table = this;
        order.AddLast(player1); order.AddLast(player2);
        current = order.First;
        current_player = player1;
        player_count = 2;
    }
    [PunRPC]
    void UpdateTable(int a, int b, int c) {
        player1 = PhotonView.Find(a).gameObject.GetComponent<Player>(); 
        player2 = PhotonView.Find(b).gameObject.GetComponent<Player>();
        player3 = PhotonView.Find(c).gameObject.GetComponent<Player>(); 
        player1.table = this; player2.table = this; player3.table = this;
        order.AddLast(player1); order.AddLast(player2); order.AddLast(player3);
        current = order.First;
        current_player = player1;
        player_count = 3;
    }
    [PunRPC]
    void UpdateTable(int a, int b, int c, int d) {
        player1 = PhotonView.Find(a).gameObject.GetComponent<Player>(); 
        player2 = PhotonView.Find(b).gameObject.GetComponent<Player>();
        player3 = PhotonView.Find(c).gameObject.GetComponent<Player>(); 
        player4 = PhotonView.Find(d).gameObject.GetComponent<Player>();
        player1.table = this; player2.table = this; player3.table = this; player4.table = this;
        order.AddLast(player1); order.AddLast(player2); order.AddLast(player3); order.AddLast(player4);
        current = order.Last;
        current_player = current.Value;
        player_count = 4;
    }
    [PunRPC]
    void SyncTable(int a, bool b) {
        current = order.Find(PhotonView.Find(a).gameObject.GetComponent<Player>());
        reversed = b;
    }
    public void Discard(GameObject card) {
        card.GetComponent<SpriteRenderer>().sortingOrder = discard.Count;
        discard.Add(card);
    }
    IEnumerator Wait_For_Deck() { //this is a coroutine that waits for the deck to be completely created before it starts dealing cards
        yield return new WaitUntil(() => draw_deck.draw_deck.Count == 100);
        Debug.Log("Deck == 100 " + draw_deck.draw_deck.Count);
        initial_deal();
    }
    private void Create_Deck() {
        Debug.Log("Deck start");
        GameObject deck = Resources.Load("Prefabs/deck") as GameObject;
        GameObject new_deck = PhotonNetwork.InstantiateSceneObject("deck", new Vector3(1.45f, 0f, 1f), transform.rotation);
        draw_deck = new_deck.GetComponent<Deck>();
        Debug.Log("Deck done");
    }
    public void initial_deal() {
        Debug.Log("Dealing start");
        Debug.Log(draw_deck.draw_deck.Count);
        current = current.List.Last;
        for (int i = 0; i < 5 * order.Count; i++) {
            AdvanceTurn();
        }
        AdvanceTurn();
        photonView.RPC("SyncTable", RpcTarget.All, current.Value.gameObject.GetComponent<PhotonView>().ViewID, reversed);
        Debug.Log("Dealing done");
    }
    [PunRPC]
    void Advance() {
        StartCoroutine(NextPlayer());
    }
    public void AdvanceTurn() {
        Debug.Log("Turn advanced");
        photonView.RPC("SyncTable", RpcTarget.All, current.Value.gameObject.GetComponent<PhotonView>().ViewID, reversed);
        photonView.RPC("Advance", RpcTarget.All);
        current_player.Draw();
    }
    //IEnumerators are coroutines which means that they can be designed to wait for specific conditions to be met before they continue
    IEnumerator NextPlayer() {
        current_player.StackCards();
        if (reversed) {
            current = current.Previous ?? current.List.Last;
        } else {
            current = current.Next ?? current.List.First;
        }
        current_player = current.Value;
        if (PhotonNetwork.IsMasterClient) {
            
        }
        yield return new WaitUntil(() => ready);
        Update_Scores();
        current_player.OrderCards();
    }
    public void Skip() { //called by the skip player card, basically just advances the current player before the next player coroutine is run
        current_player.StackCards();
        if (reversed) {
            current = current.Previous ?? current.List.Last;
        } else {
            current = current.Next ?? current.List.First;
        }
    }
    public void Reverse() {
        reversed = !reversed;
    }
    private void Update_Scores() { //checks all of the scores and displays the game over screen if a player is over the score limit and the next player can't remove enough points
        p1.text = player1.UpdateScore().ToString();
        p2.text = player2.UpdateScore().ToString();
        if (player3) {
            p3.text = player3.UpdateScore().ToString();
        }
        if (player4) {
            p4.text = player4.UpdateScore().ToString();
        }
        if (player1.score >= 21 && !current_player.StopWin()) {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 1 wins!";
        }
        else if (player2.score >= 21 && !current_player.StopWin()) {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 2 wins!";
        }
        else if (player3 && player3.score >= 21 && !current_player.StopWin()) {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 3 wins!";
        }
        else if (player4 && player4.score >= 21 && !current_player.StopWin()) {
            gameOver.SetActive(true);
            gameOver.GetComponentInChildren<TextMeshProUGUI>().text = "Player 4 wins!";
        }
    }
    public void SetReady(bool a) {
        ready = a;
    }
    void Update() {
    }
}