using UnityEngine;
using UnityEngine.UI; // For UI Text
using TMPro; // For TextMeshPro support
using System.Collections.Generic;

public class BalanceSystem : MonoBehaviour
{
    public int sleep;
    public int social;
    public int school;

    public TMP_Text balanceText;
    public ProgressBar SchoolBar;
    public ProgressBar SleepBar;
    public ProgressBar SocialBar;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) { AddSleep(5); }
        if (Input.GetKeyDown(KeyCode.A)) { AddSocial(5); }
        if (Input.GetKeyDown(KeyCode.D)) { AddSchool(5); }
        if (Input.GetKeyDown(KeyCode.W)) { SubtractSleep(5); }
        if (Input.GetKeyDown(KeyCode.Q)) { SubtractSocial(5); }
        if (Input.GetKeyDown(KeyCode.E)) { SubtractSchool(5); }
    }

    void UpdateUI()
    {
        /*if (balanceText != null)
        {
            balanceText.text = $"Sleep: {sleep}\nSocial: {social}\nSchool: {school}";
        }*/
        SchoolBar.BarValue = school;
        SleepBar.BarValue = sleep;
        SocialBar.BarValue = social;
    }

    public void AddSleep(int value) { sleep += value; UpdateUI(); }
    public void SubtractSleep(int value) { sleep -= value; UpdateUI(); }

    public void AddSocial(int value) { social += value; UpdateUI(); }
    public void SubtractSocial(int value) { social -= value; UpdateUI(); }

    public void AddSchool(int value) { school += value; UpdateUI(); }
    public void SubtractSchool(int value) { school -= value; UpdateUI(); }

    public List<int> GetVals()
    {
        return new List<int> { school, sleep, social };
    }
}
