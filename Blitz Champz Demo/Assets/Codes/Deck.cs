using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> draw_deck;
    public bool ready = false;
    void Start()
    {
        Vector3 deck_position = gameObject.GetComponent<Transform>().position;
        //Create and instantiate all of the cards in the deck
        for (int a = 0; a < 2; a++) {
            GameObject hailmary = Resources.Load("Prefabs/hail_mary") as GameObject;
            GameObject new_card = Instantiate(hailmary, deck_position, transform.rotation);
            draw_deck.Add(new_card);
        }
        for (int a = 0; a < 6; a++) {
            GameObject rushingtd = Resources.Load("Prefabs/rushing_td") as GameObject;
            GameObject new_rushingtd = Instantiate(rushingtd, deck_position, transform.rotation);
            draw_deck.Add(new_rushingtd);
            GameObject passingtd = Resources.Load("Prefabs/passing_td") as GameObject;
            GameObject new_passingtd = Instantiate(passingtd, deck_position, transform.rotation);
            draw_deck.Add(new_passingtd);
            GameObject conversion = Resources.Load("Prefabs/conversion") as GameObject;
            GameObject new_conversion = Instantiate(conversion, deck_position, transform.rotation);
            draw_deck.Add(new_conversion);
        }
        for (int a = 0; a < 7; a++) {
            GameObject fieldgoal = Resources.Load("Prefabs/field_goal") as GameObject;
            GameObject new_fieldgoal = Instantiate(fieldgoal, deck_position, transform.rotation);
            draw_deck.Add(new_fieldgoal);
        }
        for (int a = 0; a < 11; a++) {
            GameObject extra_point = Resources.Load("Prefabs/extra_point") as GameObject;
            GameObject new_extra_point = Instantiate(extra_point, deck_position, transform.rotation);
            draw_deck.Add(new_extra_point);
        }



        for (int a = 0; a < 3; a++) {
            GameObject interception = Resources.Load("Prefabs/interception") as GameObject;
            GameObject new_interception = Instantiate(interception, deck_position, transform.rotation);
            draw_deck.Add(new_interception);
        }
        for (int a = 0; a < 4; a++) {
            GameObject tackle = Resources.Load("Prefabs/tackle") as GameObject;
            GameObject new_tackle = Instantiate(tackle, deck_position, transform.rotation);
            draw_deck.Add(new_tackle);
        }
        for (int a = 0; a < 11; a++) {
            GameObject blocked_kick = Resources.Load("Prefabs/blocked_kick") as GameObject;
            GameObject new_blocked_kick = Instantiate(blocked_kick, deck_position, transform.rotation);
            draw_deck.Add(new_blocked_kick);
        }


        for (int a = 0; a < 4; a++) {
            GameObject fumble = Resources.Load("Prefabs/fumble") as GameObject;
            GameObject new_fumble = Instantiate(fumble, deck_position, transform.rotation);
            draw_deck.Add(new_fumble);
        }
        for (int a = 0; a < 8; a++) {
            GameObject first_down = Resources.Load("Prefabs/first_down") as GameObject;
            GameObject new_first_down = Instantiate(first_down, deck_position, transform.rotation);
            draw_deck.Add(new_first_down);
        }
        for (int a = 0; a < 8; a++) {
            GameObject pass_completion = Resources.Load("Prefabs/pass_completion") as GameObject;
            GameObject new_pass_completion = Instantiate(pass_completion, deck_position, transform.rotation);
            draw_deck.Add(new_pass_completion);
        }
        for (int a = 0; a < 8; a++) {
            GameObject five_yard_run = Resources.Load("Prefabs/5_yard_run") as GameObject;
            GameObject new_5_yard_run = Instantiate(five_yard_run, deck_position, transform.rotation);
            draw_deck.Add(new_5_yard_run);
        }
        for (int a = 0; a < 8; a++) {
            GameObject blitz = Resources.Load("Prefabs/blitz") as GameObject;
            GameObject new_blitz = Instantiate(blitz, deck_position, transform.rotation);
            draw_deck.Add(new_blitz);
        }
        for (int a = 0; a < 2; a++) {
            GameObject end_of_quarter1 = Resources.Load("Prefabs/end_of_quarter1") as GameObject;
            GameObject new_end_of_quarter1 = Instantiate(end_of_quarter1, deck_position, transform.rotation);
            draw_deck.Add(new_end_of_quarter1);
        }
        for (int a = 0; a < 2; a++) {
            GameObject end_of_quarter2 = Resources.Load("Prefabs/end_of_quarter2") as GameObject;
            GameObject new_end_of_quarter2 = Instantiate(end_of_quarter2, deck_position, transform.rotation);
            draw_deck.Add(new_end_of_quarter2);
        }
        for (int a = 0; a < 2; a++) {
            GameObject end_of_quarter3 = Resources.Load("Prefabs/end_of_quarter3") as GameObject;
            GameObject new_end_of_quarter3 = Instantiate(end_of_quarter3, deck_position, transform.rotation);
            draw_deck.Add(new_end_of_quarter3);
        }
        for (int a = 0; a < 2; a++) {
            GameObject end_of_quarter4 = Resources.Load("Prefabs/end_of_quarter4") as GameObject;
            GameObject new_end_of_quarter4 = Instantiate(end_of_quarter4, deck_position, transform.rotation);
            draw_deck.Add(new_end_of_quarter4);
        }
        for (int a = 0; a < draw_deck.Count; a++) {
            //draw_deck[a].GetComponent<Card>().Hide();
        }
        gameObject.GetComponent<Transform>().position = gameObject.transform.position + new Vector3(0f, 0f, -2);
        ready = true;
        Debug.Log("Deck made");
    }
    
    public GameObject Draw(Player a) {
        Debug.Log("Deck draw called");
        int random_num = Random.Range(0, draw_deck.Count);
        GameObject drawn_card = draw_deck[random_num];
        draw_deck.RemoveAt(random_num);
        return drawn_card;
    }
    private void OnMouseUpAsButton() {

	}
    void Update()
    {
        
    }
}
