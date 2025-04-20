using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    public GameObject GameplayCanvas;
    public GameObject StartCanvas;
    public GameObject PauseCanvas;
    public GameObject LoseCanvas;
    public GameObject WinCanvas;
    public TextMeshProUGUI LoseText;
    public TextMeshProUGUI WinText;
    public BalanceSystem StatsManager;
    public CardManager aCardManager;
    private string LossCause;
    private int GameState; // 0 is playing, 1 is win, -1 is lost
    
    // Start is called before the first frame update
    void Start()
    {
        StartCanvas.SetActive(true);
        GameplayCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
        WinCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseCanvas.SetActive(true);
        }

        if (GameState == 0)
        {
            if (StatsManager.school <= 0)
            {
                LossCause = "school";
                GameState = -1;
            }
            else if (StatsManager.sleep <= 0)
            {
                LossCause = "sleep";
                GameState = -1;
            }
            else if (StatsManager.social <= 0)
            {
                LossCause = "social";
                GameState = -1;
            }
            else if(StatsManager.school >= 100 && StatsManager.sleep >= 100 && StatsManager.social >= 100)
            {
                GameState = 1;
            }
        }
        if(GameState == -1)
        {
            LoseText.GetComponent<TextMeshProUGUI>().text = "Your " + LossCause + " fell to 0";
            LoseCanvas.SetActive(true);
            GameplayCanvas.SetActive(false);
        }

        if(GameState == 1 || aCardManager.cardsDrawn == 20)
        {
            WinText.GetComponent<TextMeshProUGUI>().text = "You won in " + aCardManager.cardsDrawn + " days!";
            WinCanvas.SetActive(true);
            GameplayCanvas.SetActive(false);
        }
    }

    public void PlayButton()
    {
        GameplayCanvas.SetActive(true);
        StartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
    }

    public void QuitButton()
    {
        GameplayCanvas.SetActive(true);
        EditorApplication.ExitPlaymode();
    }
}
