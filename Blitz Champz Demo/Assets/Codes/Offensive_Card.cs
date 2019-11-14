using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offensive_Card : Card {
	protected int value;
	public bool kick = false;
	public bool pass = false;
	public bool run = false;
	void Start() {
	}
	protected override void Play() {
		owner.field.Add(gameObject);
		owner.hand.Remove(gameObject);
		owner.table.last_card = this;
		for (int i = 0; i < owner.hand.Count; i++) {
			owner.hand[i].GetComponent<SpriteRenderer>().color = Color.white;
		}
		owner.update_score(value);
		Vector3 adjustment;
		if (owner.transform.position.x > 0) { 
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = owner.field.Count;
			adjustment = new Vector3(-1.75f + -1 * 0.25f * owner.field.Count, 0, 0.0f);
		} else {
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - owner.field.Count;
			adjustment = new Vector3(1.75f + .25f * owner.field.Count, 0, 0.0f);
		}

		gameObject.transform.position = owner.transform.position + adjustment + Vector3.Scale(transform.up, new Vector3(0, 2.5f, 0));

		gameObject.GetComponent<BoxCollider>().enabled = false;
		Show();
		AdvanceTurn();
	}
	public void Remove() { //remove card from the field and discard it thus removing points from that player
		owner.update_score((-1) * value);
		Discard();
	}
	private void OnMouseUpAsButton() {
		if (owner != null && owner.table.current_player == owner) {
			this.Play();
		}
	}
	void Update () {
		
	}
}
