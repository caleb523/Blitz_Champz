using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fumble : Continuation_Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
	public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/fumble");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
