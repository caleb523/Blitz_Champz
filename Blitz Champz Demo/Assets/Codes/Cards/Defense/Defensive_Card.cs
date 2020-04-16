﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensive_Card : Card {
	protected bool kick = false;
	protected bool pass = false;
	protected bool run = false;
	//Get the AudioSource for each Defensive card
	private AudioSource source;
	//Animation parameters, set to public here so the lower classes can edit the values
	public float speed = .25f;
    public Vector3 target;
    public Vector3 position;

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
		//When the card is played, play the sound attached to it
		source = GetComponent<AudioSource>();
		source.Play();
		StartCoroutine(MoveTo());
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
				valid = true;
				return false;
			}
		} else {
			return false;
		}
	}
	void Update () {
		
	}

	 //MoveTo Coroutine
     IEnumerator MoveTo()
    {
       

        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while (transform.position != target)
        {
            Debug.Log("Got to 4 loop");
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed);
            // Wait a frame and move again.
            yield return null;
        }
    }
}
