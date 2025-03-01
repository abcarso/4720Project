using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeck", menuName = "Card Deck")]
public class CardDeck : ScriptableObject
{
    public string deckID; // Unique identifier for the deck
    public List<Card> cards; // Cards in this deck
    public bool isActive; // Whether the deck is currently active
}
