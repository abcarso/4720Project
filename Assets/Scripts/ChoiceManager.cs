using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public GameObject choiceButtonPrefab; // Assign prefab in the Inspector
    public Transform choiceContainer; // Assign panel in the Inspector

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
        // Change stats
        FindObjectOfType<BalanceSystem>().AddSleep(option.sleepEffect);
        FindObjectOfType<BalanceSystem>().AddSocial(option.socialEffect);
        FindObjectOfType<BalanceSystem>().AddSchool(option.schoolEffect);
        
        // Check if there is a next card
        if (option.nextCard != null)
        {
            // Get the next card in line
            FindObjectOfType<CardManager>().ShowCard(option.nextCard);
        }
        else
        {
            // Get the next card from the deck
            FindObjectOfType<CardManager>().NextCard();
        }
    }
}