using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    public List<CardDeck> allDecks; // Master list of all decks
    private List<Card> currentPool = new List<Card>(); // Cards in the active pool
    private Card currentCard; // Card displayed to the player
    public TMP_Text cardText; // Text on the card
    
    public void NextCard()
    {
        ShuffleDeck();
        currentCard = DrawCard();
        cardText.text = currentCard.dialogText;
    }

    //Reconstitute the deck of available cards
    public void ShuffleDeck()
    {
        Debug.Log("Shuffling...");
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
                        Debug.Log("Card Added");
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
                Debug.Log(card.cardID);
                return card;
            }
            randomValue -= card.weight;
        }
        Debug.LogWarning("Oops.");
        return null; // Fallback
    }
}
