﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass_Completion : Continuation_Card
{
    //Get the AudioSource for each Offensive card
	private AudioSource source;
    //animation
    public float speed = 2.5f;
    private Vector3 target;
    private Vector3 position;
    void Start() {
        //animation
        target = new Vector3(-1.45f, 0f, 0f);
        position = gameObject.transform.position;
        
    }
    protected override void Play() {
        
        StartCoroutine(MoveTo());
        owner.Draw();
        
        //When the card is played, play the sound attached to it
		source = GetComponent<AudioSource>();
		source.Play();
        AdvanceTurn();
    }
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/pass_completion");
    }
    void Update() {
        
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