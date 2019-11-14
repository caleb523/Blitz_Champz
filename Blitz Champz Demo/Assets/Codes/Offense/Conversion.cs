using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversion : Offensive_Card
{
    void Start()
    {
        value = 2;
		kick = false;
		pass = true;
		run = true;
    }
    public override void Show() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cards/conversion");
    }
    void Update()
    {
        
    }
}
