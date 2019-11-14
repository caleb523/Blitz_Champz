using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public Deck draw_deck;
    public List<GameObject> discard;
    public void Discard(GameObject card) {
        card.GetComponent<SpriteRenderer>().sortingOrder = discard.Count;
        discard.Add(card);
    }
    public Offensive_Card last_card;
    static public int player_count;
    public Player pov_player;
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public Text p1;
    public Text p2;
    public Player current_player;
    LinkedListNode<Player> current;
    public LinkedList<Player> order = new LinkedList<Player>();
    void Start() {
        Create_Deck();
        Create_Players();

        
        current = order.First;
        current_player = player1;
        StartCoroutine(Wait_For_Deck());
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
        }
        if (player_count == 4) {
            GameObject player4_object = Instantiate(player, new Vector3(8f, -4.4f, 1f), transform.rotation);
            player4 = player4_object.GetComponent<Player>();
            player4.table = this;
            order.AddLast(player4);
        }
    }
    IEnumerator Wait_For_Deck() {
        yield return new WaitUntil(() => draw_deck.draw_deck.Count == 100);
        Debug.Log("Deck == 100 " + draw_deck.draw_deck.Count);
        initial_deal();
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
        //Update_Scores();
        current_player.stack_cards();
        current = current.Next ?? current.List.First;
        current_player = current.Value;
        current_player.draw();
    }
    public void Update_Scores() {
        p1.text = (player1.score).ToString();
        p2.text = (player2.score).ToString();
    }
    void Update() {
    }
}