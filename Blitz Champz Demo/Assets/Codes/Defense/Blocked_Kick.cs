using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocked_Kick : Defensive_Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void Play() {
		if (owner.table.last_card.kick) {
            owner.table.last_card.Remove();
        }
        AdvanceTurn();
	}
    private void OnMouseUpAsButton() {
		if (owner != null && owner.table.current_player == owner && owner.table.last_card != null && owner.table.last_card.kick) {
			this.Play();
			this.Discard();
		}
	}
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/blocked_kick");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
