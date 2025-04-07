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
    public TextMeshProUGUI LoseText;
    public BalanceSystem StatsManager;
    private string LossCause;
    private int GameState; // 0 is playing, 1 is win, -1 is lost
    
    // Start is called before the first frame update
    void Start()
    {
        StartCanvas.SetActive(true);
        GameplayCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
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
        }
        if(GameState == -1)
        {
            LoseText.GetComponent<TextMeshProUGUI>().text = "Your " + LossCause + " fell to 0";
            LoseCanvas.SetActive(true);
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
