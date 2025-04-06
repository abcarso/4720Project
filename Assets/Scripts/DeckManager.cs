using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Decks")]
    public CardDeck tutorialDeck;
    public CardDeck normalDeck;
    public CardDeck sleepDeprivedDeck;
    public CardDeck depressedDeck;
    public CardDeck anxiousDeck;

    [Header("Final Tutorial Card")]
    public Card finalTutorialCard;

    [Header("Thresholds")]
    public int sleepThreshold = 20;
    public int socialThreshold = 20;
    public int anxiousThreshold = 20; 

    [Header("References")]
    public BalanceSystem balanceSystem; 
    public CardManager cardManager;     

    private void Start()
    {
        // At the start, only the tutorial deck is active
        SetAllDecksInactive();
        tutorialDeck.isActive = true;
    }

    private void Update()
    {
        // 1) If the tutorial deck is active, check if it's finished
        if (tutorialDeck.isActive && (finalTutorialCard.encounterCount > 0))
        {
            tutorialDeck.isActive = false;
            normalDeck.isActive = true;
            
            // Force the CardManager to re-shuffle so that the Normal deck becomes available immediately
            cardManager.ShuffleDeck();
        }

        // 2) Sleep Deprived deck is only active while sleep < sleepThreshold
        if (balanceSystem.sleep <= sleepThreshold)
            sleepDeprivedDeck.isActive = true;
        else
            sleepDeprivedDeck.isActive = false;

        // 3) Depressed deck is only active while social < socialThreshold
        if (balanceSystem.social <= socialThreshold)
            depressedDeck.isActive = true;
        else
            depressedDeck.isActive = false;

        // 4) Anxious deck is only active if school >= some threshold (example)
        // Adjust condition to suit your design: you might want ">" or "<" or else.
        if (balanceSystem.school <= anxiousThreshold)
            anxiousDeck.isActive = true;
        else
            anxiousDeck.isActive = false;
    }

    private void SetAllDecksInactive()
    {
        tutorialDeck.isActive = false;
        normalDeck.isActive = false;
        sleepDeprivedDeck.isActive = false;
        depressedDeck.isActive = false;
        anxiousDeck.isActive = false;
    }
}
