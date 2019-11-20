using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
	public int score;
	public List<GameObject> hand;
	public List<GameObject> field;
	public Table table;
	public bool right = false;
	public bool up = false;
	public bool valid = true;
	void Start () {
		score = 0;
		if (this.transform.position.x > 0) {
			right = true;
		}
		if (this.transform.position.y > 0) {
			up = true;
			gameObject.transform.rotation = Quaternion.Euler(0,0,180f);
		}
	}
	public int UpdateScore() {
		score = 0;
		foreach (GameObject card in field) {
			if (card.GetComponent<Card>().owner != this) {
				field.Remove(card);
			} else {
				score += card.GetComponent<Offensive_Card>().GetValue();
			}
		}
		return score;
	}
	public void Draw() {
		Deck draw_deck = table.draw_deck;
		if (draw_deck.draw_deck.Count > 0 && table.current_player == this) {
			GameObject new_card = draw_deck.Draw(this);
			new_card.GetComponent<Card>().SetOwner(this);
			hand.Add(new_card);
		}
		OrderCards();
		CheckValid();
	}

	public void Remove(GameObject card) {
		field.Remove(card);
		hand.Remove(card);
	}
	public void StackCards() {
		for (int i = 0; i < hand.Count; i++) {
			hand[i].transform.position = gameObject.transform.position;
			hand[i].GetComponent<Card>().Hide();
			hand[i].GetComponent<BoxCollider>().enabled = false;
		}
		OrderField();
	}
	public void OrderField() {
		for (int i = 0; i < field.Count; i++) {
			field[i].transform.position = gameObject.transform.position;
			field[i].GetComponent<SpriteRenderer>().color = Color.white;
			if (right) {
				field[i].GetComponent<SpriteRenderer>().sortingOrder = i;
				Vector3 adjustment = new Vector3(-1.75f + -1 * 0.25f * i, 0, 2 * (field.Count - i));
				field[i].transform.position = transform.position + adjustment + Vector3.Scale(transform.up, new Vector3(0, 2.5f, 0));
				field[i].transform.rotation = Quaternion.Euler(0,0,-90f);
			} else {
				field[i].GetComponent<SpriteRenderer>().sortingOrder = i;
				Vector3 adjustment = new Vector3(1.75f + 0.25f * i, 0, 2 * (field.Count - i));
				field[i].transform.position = transform.position + adjustment + Vector3.Scale(transform.up, new Vector3(0, 2.5f, 0));
				field[i].transform.rotation = Quaternion.Euler(0,0,90f);
			}
		}
	}
	public void OrderCards() {
		if (CheckValid() == false) {
			Debug.Log("No valid cards. Discard please.");
		}
		if (right) {
			for (int i = 0; i < hand.Count; i++) {
				Vector3 adjustment = new Vector3(-1 * 0.5f * i, 0.0f, 0.0f);
				hand[i].GetComponent<SpriteRenderer>().sortingOrder = 2 * i;
				hand[i].GetComponent<Transform>().position = gameObject.transform.position + adjustment + new Vector3(0f, 0f, 2 * (hand.Count - i));
				hand[i].GetComponent<BoxCollider>().enabled = true;
				if (this == table.current_player) {
					hand[i].GetComponent<Card>().Show();
				}
			}
		}
		else {
			for (int i = 0; i < hand.Count; i++) {
				Vector3 adjustment = new Vector3(0.5f * i, 0.0f, 0.0f);
				hand[i].GetComponent<SpriteRenderer>().sortingOrder = 2 * (hand.Count - i);
				hand[i].GetComponent<Transform>().position = gameObject.transform.position + adjustment + new Vector3(0f, 0f, 2 * i);
				hand[i].GetComponent<BoxCollider>().enabled = true;
				if (this == table.current_player) {
					hand[i].GetComponent<Card>().Show();
				}
			}
		}
		OrderField();
	}
	protected bool CheckValid() {
		bool temp_valid = false;
		for (int i = 0; i < hand.Count; i++) {
			if (hand[i].GetComponent<Card>().CheckValid()) {
				temp_valid = true;
			}
		}
		valid = temp_valid;
		return temp_valid;
	}
	public bool StopWin() {
		bool canStop = false;
		foreach (GameObject a in hand) {
			if (a.GetComponent<Defensive_Card>() != null) {
				if (a.GetComponent<Defensive_Card>().CheckValid()) {
					canStop = true;
					a.GetComponent<Defensive_Card>().SetPlayed(true);
				} else {
					a.GetComponent<BoxCollider>().enabled = false;
					a.GetComponent<SpriteRenderer>().color = Color.gray;
				}
			} else if (a.GetComponent<Blitz>() != null) {
				canStop = true;
				a.GetComponent<Blitz>().SetPlayed(true);
			} else {
				a.GetComponent<BoxCollider>().enabled = false;
				a.GetComponent<SpriteRenderer>().color = Color.gray;
			}
		}
		return canStop;
	}
	public bool GetValid() {
		return CheckValid();
	}
	void Update () {
	}
}