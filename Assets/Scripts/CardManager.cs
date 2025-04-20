using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public List<CardDeck> allDecks; // Master list of all decks
    private List<Card> currentPool = new List<Card>(); // Cards in the active pool
    private Card currentCard; // Card displayed to the player
    //public GameObject Card; // Physical card object
    public TMP_Text cardText; // Text on the card
    public GameObject currentCharacter; // Image on the card
    
    public void Start()
    {
        foreach (var deck in allDecks)
        {
            foreach (var card in deck.cards)
            {
                card.encounterCount = 0;
            }
        }
        NextCard();
    }

    public void NextCard()
    {
        ShuffleDeck();
        currentCard = DrawCard();
        //Instantiate(Card);

        // Display card text
        cardText.text = currentCard.dialogText;
        // Display card character if it exists
        if (currentCard.image != null)
        {
            currentCharacter.GetComponent<Image>().sprite = currentCard.image;
            currentCharacter.GetComponent<Image>().enabled = true;
        }
        else
        {
            currentCharacter.GetComponent<Image>().enabled = false;
        }

        FindObjectOfType<ChoiceManager>().GenerateChoices(currentCard.options);
    }

    //Reconstitute the deck of available cards
    public void ShuffleDeck()
    {
        currentPool.Clear();
        foreach (var deck in allDecks)
        {
            if (deck.isActive)
            {
                foreach (var card in deck.cards)
                {
                    if (IsCardAvailable(card))
                    {
                        currentPool.Add(card);
                    }
                }
            }
        }
    }

    //Check if a card has been seen before
    private bool IsCardAvailable(Card card)
    {
        // Check repeatable logic
        if (!card.isRepeatable && card.encounterCount > 0)
        {
            return false;
        }
        // Don't draw the same card twice in a row
        if (card == currentCard)
        {
            return false;
        }

        return true;
    }

    private bool IsDeckActiveByID(string deckID)
    {
        var deck = allDecks.Find(d => d.deckID == deckID);
        return deck != null && deck.isActive;
    }
    public Card DrawCard()
    {
        if (currentPool.Count == 0) return null;

        int totalWeight = 0;
        foreach (var card in currentPool)
        {
            totalWeight += card.weight;
        }

        // This is a method for drawing weighted random
        int randomValue = Random.Range(0, totalWeight);
        foreach (var card in currentPool)
        {
            if (randomValue < card.weight)
            {
                card.encounterCount++;
                return card;
            }
            randomValue -= card.weight;
        }
        Debug.LogWarning("Oops.");
        return null; // Fallback
    }
    
    public void ShowCard(Card card)
    {
        currentCard = card;
        currentCard.encounterCount++;

        cardText.text = currentCard.dialogText;

        Image img = currentCharacter.GetComponent<Image>();
        FindObjectOfType<ChoiceManager>().GenerateChoices(currentCard.options);
    }
}
