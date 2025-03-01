using UnityEngine;
using UnityEngine.UI; // For UI Text
using TMPro; // For TextMeshPro support

public class BalanceSystem : MonoBehaviour
{
    public int sleep = 5;
    public int social = 5;
    public int school = 5;

    public TMP_Text balanceText; 

    void Start()
    {
        Debug.Log("Balance System Initialized: Sleep = " + sleep + ", Social = " + social + ", School = " + school);
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) { AddSleep(1); }
        if (Input.GetKeyDown(KeyCode.A)) { AddSocial(1); }
        if (Input.GetKeyDown(KeyCode.D)) { AddSchool(1); }
        if (Input.GetKeyDown(KeyCode.W)) { SubtractSleep(1); }
        if (Input.GetKeyDown(KeyCode.Q)) { SubtractSocial(1); }
        if (Input.GetKeyDown(KeyCode.E)) { SubtractSchool(1); }
    }

    void UpdateUI()
    {
        if (balanceText != null)
        {
            balanceText.text = $"Sleep: {sleep}\nSocial: {social}\nSchool: {school}";
        }
    }

    public void AddSleep(int value) { sleep += value; Debug.Log("Sleep: " + sleep); UpdateUI(); }
    public void SubtractSleep(int value) { sleep -= value; Debug.Log("Sleep: " + sleep); UpdateUI(); }

    public void AddSocial(int value) { social += value; Debug.Log("Social: " + social); UpdateUI(); }
    public void SubtractSocial(int value) { social -= value; Debug.Log("Social: " + social); UpdateUI(); }

    public void AddSchool(int value) { school += value; Debug.Log("School: " + school); UpdateUI(); }
    public void SubtractSchool(int value) { school -= value; Debug.Log("School: " + school); UpdateUI(); }
}
