using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensive_Card : Card {
	protected bool kick = false;
	protected bool pass = false;
	protected bool run = false;

	void Start () {
		
	}
	public void SetPlayed(bool a) {
		win_played = a;
	}
	public bool GetKick() {
		return kick;
	}
	public bool GetPass() {
		return pass;
	}
	public bool GetRun() {
		return run;
	}
	protected override void Play() {
		owner.table.last_card.Remove();
		AdvanceTurn();
	}
	private void OnMouseUpAsButton() {
		if (owner != null && owner.table.current_player == owner) {
            if (CheckValid()) {
				this.Play();
                this.Discard();
            } else {
				if (owner.GetValid()) {
                	Debug.Log("Not a valid move");
				} else {
					AdvanceTurn();
					this.Discard();
				}
            }
		}
	}
	public override bool CheckValid() {
		win_played = false;
		if(owner.table.last_card){
			if (owner.table.last_card.GetPass() == true && pass == true) {
				valid = true;
				return true;
			} else if (owner.table.last_card.GetRun() == true && run == true) {
				valid = true;
				return true;
			} else if (owner.table.last_card.GetKick() == true && kick == true) {
				valid = true;
				return true;
			} else {
				valid = false;
				return false;
			}
		} else {
			return false;
		}
	}
	void Update () {
		
	}
}
