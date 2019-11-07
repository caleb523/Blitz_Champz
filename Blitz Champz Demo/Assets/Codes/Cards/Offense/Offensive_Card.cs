﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offensive_Card : Card {
	public int value;
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
		gameObject.GetComponent<BoxCollider>().enabled = false;
		Show();
		AdvanceTurn();
	}
	public int GetValue() {
		return value;
	}
	public void Remove() { //remove card from the field and discard it thus removing points from that player
		owner.UpdateScore();
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