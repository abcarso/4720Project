using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    public List<CardDeck> allDecks; // Master list of all decks
    private List<Card> currentPool = new List<Card>(); // Cards in the active pool
    private Card currentCard; // Card displayed to the player
    public GameObject Card; // Physical card object
    //public TMP_Text cardText; // Text on the card
    
    public void Start()
    {
        foreach (var deck in allDecks)
        {
            foreach (var card in deck.cards)
            {
                card.encounterCount = 0;
            }
        }
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            NextCard();
        }
    }

    public void NextCard()
    {
        ShuffleDeck();
        currentCard = DrawCard();
        Debug.Log(currentCard.dialogText);
        Instantiate(Card);
        Card.GetComponent<TextMeshProUGUI>().text = currentCard.dialogText;
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
                        Debug.Log("Card Added from" + deck.deckID);
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
                Debug.Log(card.cardID);
                return card;
            }
            randomValue -= card.weight;
        }
        Debug.LogWarning("Oops.");
        return null; // Fallback
    }
}
