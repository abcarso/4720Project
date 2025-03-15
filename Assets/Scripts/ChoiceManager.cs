using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public GameObject choiceButtonPrefab; // Assign prefab in the Inspector
    public Transform choiceContainer; // Assign the panel in the Inspector

    private List<GameObject> activeChoices = new List<GameObject>();

    public void GenerateChoices(List<DialogOption> dialogOptions)
    {
        ClearChoices(); // Remove old choices before adding new ones

        foreach (DialogOption option in dialogOptions)
        {
            GameObject newButton = Instantiate(choiceButtonPrefab, choiceContainer);
            newButton.SetActive(true); // Ensure the button is visible

            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = option.text; // Set the button's text
            }

            Button button = newButton.GetComponent<Button>();
            button.onClick.AddListener(() => OnChoiceSelected(option)); // Pass the full option

            activeChoices.Add(newButton); // Keep track of buttons for cleanup
        }
    }



    public void ClearChoices()
    {
        foreach (GameObject button in activeChoices)
        {
            Destroy(button);
        }
        activeChoices.Clear();
    }

    private void OnChoiceSelected(DialogOption option)
    {
        Debug.Log($"Player selected: {option.text}");
        Debug.Log($"Effects: School={option.schoolEffect}, Sleep={option.sleepEffect}, Social={option.socialEffect}");
        FindObjectOfType<BalanceSystem>().AddSleep(option.sleepEffect);
        FindObjectOfType<BalanceSystem>().AddSocial(option.socialEffect);
        FindObjectOfType<BalanceSystem>().AddSchool(option.schoolEffect);
        // Apply effects or handle selection logic here
        ClearChoices(); // Remove choices after selection (optional)
    }
}