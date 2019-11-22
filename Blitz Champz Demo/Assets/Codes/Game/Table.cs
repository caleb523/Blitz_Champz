using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Table : NetworkBehaviour
{
    [SyncVar]
    public Deck draw_deck;
    public List<GameObject> discard;
    public Offensive_Card last_card;
    public int player_count;
    public Player pov_player;
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public Text p1;
    public Text p2;
    public Text p3;
    public Text p4;
    public Player current_player;
    public GameObject gameOver;
    private bool reversed = false;
    private bool ready = true;
    LinkedListNode<Player> current;
    public LinkedList<Player> order = new LinkedList<Player>();
    void Start() {
        player_count = Manager.PlayerCount;
        Create_Deck();
        //Create_Players();
        StartCoroutine(Wait_For_Players());
    }
    public void AddPlayer(Player a) {
        if (!player1) {
            player1 = a;
            order.AddLast(player1);
        } else if (!player2) {
            player2 = a;
            order.AddLast(player1);
        } else if (!player3) {
            player3 = a;
            order.AddLast(player1);
        } else if (!player4) {
            player4 = a;
            order.AddLast(player1);
        }
    }
    public void Discard(GameObject card) {
        card.GetComponent<SpriteRenderer>().sortingOrder = discard.Count;
        discard.Add(card);
    }
    private void Create_Players() {
        GameObject player = Resources.Load("Prefabs/player") as GameObject;
        if (player_count > 1) {
            GameObject player1_object = Instantiate(player, new Vector3(-8f, -4.4f, 1f), transform.rotation);
            player1 = player1_object.GetComponent<Player>();
            player1.table = this;
            GameObject player2_object = Instantiate(player, new Vector3(-8f, 4.4f, 1f), transform.rotation);
            player2 = player2_object.GetComponent<Player>();
            player2.table = this;
            order.AddLast(player1);
            order.AddLast(player2);
        }
        if (player_count > 2) {
            GameObject player3_object = Instantiate(player, new Vector3(8f, 4.4f, 1f), transform.rotation);
            player3 = player3_object.GetComponent<Player>();
            player3.table = this;
            order.AddLast(player3);
            p3.gameObject.SetActive(true);
        }
        if (player_count == 4) {
            GameObject player4_object = Instantiate(player, new Vector3(8f, -4.4f, 1f), transform.rotation);
            player4 = player4_object.GetComponent<Player>();
            player4.table = this;
            order.AddLast(player4);
            p4.gameObject.SetActive(true);
        }
    }
    IEnumerator Wait_For_Deck() {
        yield return new WaitUntil(() => draw_deck.draw_deck.Count == 100);
        Debug.Log("Deck == 100 " + draw_deck.draw_deck.Count);
        initial_deal();
    }
    IEnumerator Wait_For_Players() {
        yield return new WaitUntil(() => order.Count == Manager.PlayerCount);
        current = order.First;
        current_player = player1;
        StartCoroutine(Wait_For_Deck());
    }
    private void Create_Deck() {
        Debug.Log("Deck start");
        GameObject deck = Resources.Load("Prefabs/deck") as GameObject;
        GameObject new_deck = Instantiate(deck, new Vector3(1.45f, 0f, 1f), transform.rotation);
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
        Debug.Log("Dealing done");
    }
    public void AdvanceTurn() {
        StartCoroutine(NextPlayer());
    }
    IEnumerator NextPlayer() {
        current_player.StackCards();
        if (reversed) {
            current = current.Previous ?? current.List.Last;
        } else {
            current = current.Next ?? current.List.First;
        }
        current_player = current.Value;
        current_player.Draw();
        yield return new WaitUntil(() => ready);
        Update_Scores();
    }
    public void Skip() {
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
    private void Update_Scores() {
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
    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/player"), new Vector3(-8f, -4.4f, 1f), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}

