using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadPlayerOptions;
    public void PlayGame ()
    {
        SceneManager.LoadScene("10-9-2019");
    }
    public void TwoPlayer()
    {
        Table.player_count = 2;
        PlayGame();
    }
    public void ThreePlayer()
    {
        Table.player_count = 3;
        PlayGame();
    }
    public void FourPlayer()
    {
        Table.player_count = 4;
        PlayGame();
    }
    public void OpenPlayerOptions()
    {
        loadPlayerOptions.SetActive(true);
    }

}
