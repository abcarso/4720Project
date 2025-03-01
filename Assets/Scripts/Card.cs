 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{

    [Header("Event Details")]
    public string eventName; // Name of the event
    public Sprite image; // If we want we can put sprites here
    
    [TextArea] public string dialogText; // The actual dialog text
    
    [Header("Dialog Options")]
    public List<DialogOption> options; // Possible player responses


    [Header("Card Properties")]
    public string cardID; // Unique identifier
    public int weight; // Likelihood of appearing
    public bool isRepeatable; // Can this card appear more than once?
    public int encounterCount; // Tracks how many times the card has been drawn

}

[System.Serializable]
public class DialogOption
{
    public string text; // Text shown to the player
    public int schoolEffect; // +/- to school
    public int sleepEffect; // +/- to sleep
    public int socialEffect; // +/- to social
}