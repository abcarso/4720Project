using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject GameplayCanvas;
    public GameObject LoseCanvas;
    public TextMeshProUGUI LoseText;
    public BalanceSystem StatsManager;
    private string LossCause;
    private int GameState; // 0 is playing, 1 is win, -1 is lost
    
    // Start is called before the first frame update
    void Start()
    {
        GameplayCanvas.SetActive(true);
        LoseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState == 0)
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
}
