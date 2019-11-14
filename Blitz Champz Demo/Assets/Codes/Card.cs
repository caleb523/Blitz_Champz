using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Card : MonoBehaviour {
	public Player owner;
	public void SetOwner(Player own) {
		this.owner = own;
		gameObject.GetComponentInChildren<TextMeshPro>().SetText(owner.name + "\nHand");
		if (owner.up) {
			gameObject.transform.rotation = Quaternion.Euler(0,0,180f);
		}
	}
	void Start () {
	}

	public void Discard () {
		if (this.owner != null) {
			for (int i = 0; i < owner.hand.Count; i++) {
				owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
			}
			owner.table.last_card = null;
			gameObject.GetComponent<Transform>().position = new Vector3(-1f, 0f, 0f);
			gameObject.GetComponentInChildren<TextMeshPro>().SetText("None" + "\nDiscarded");
			owner.table.Discard(gameObject);
			owner.remove(gameObject);
			owner.order_cards();
			this.owner = null;
			Destroy(GetComponent<BoxCollider>());
			Show();
		}
	}
	public void AdvanceTurn() {
		owner.table.AdvanceTurn();
	}
	private void OnMouseUpAsButton() {
		if (owner != null && owner.table.current_player == owner) {
			this.Play();
			this.Discard();
		}
	}
	public void Hide() {
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/back");
	}
	public virtual void Show() {
	}
	void OnMouseEnter() {
		if (owner != null && owner == owner.table.current_player) {
			gameObject.transform.position += Vector3.Scale(transform.up, new Vector3(0f, 0.5f, 0f));
			gameObject.GetComponent<SpriteRenderer>().sortingOrder +=20;
			for (int i = 0; i < owner.hand.Count; i++) {
				if (owner.hand[i] != gameObject) {
					owner.hand[i].GetComponent<SpriteRenderer>().color = Color.gray;
				}
			}
		}
	}
	void OnMouseExit() {
		if (owner != null && owner == owner.table.current_player) {
			gameObject.transform.position -= Vector3.Scale(transform.up, new Vector3(0f, 0.5f, 0f));
			gameObject.GetComponent<SpriteRenderer>().sortingOrder -=20;
			for (int i = 0; i < owner.hand.Count; i++) {
				owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}
	protected virtual void Play () {
	}
	void Update () {
		
	}
}