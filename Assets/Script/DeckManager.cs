using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<Card> Deck = new List<Card>();
    public List<Card> Hand = new List<Card>();
    public List<Card> Graveyard = new List<Card>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeInitialDeck();
        initializeDeck();
        //shuffleDeck();
        //initializeHand();
        //initializeGY();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeInitialDeck() {
        GameData.initialDeck.Clear();
        GameData.initialDeck.Add(new CardMonster(0001,"warrior",Card.CardType.MONSTER,1,3,4,1,1));
        GameData.initialDeck.Add(new CardMonster(0002,"mage",Card.CardType.MONSTER,1,4,1,2,2));
        GameData.initialDeck.Add(new CardMonster(0003,"rouge",Card.CardType.MONSTER,1,2,2,2,1));
        GameData.initialDeck.Add(new CardMonster(0004,"goblin",Card.CardType.MONSTER,0,0,4,0,0,false));
    }

    void initializeDeck() {
        print("初始化牌组.");
        Deck.Clear();
        foreach (Card card in GameData.initialDeck) {
            print("在主卡组里添加了" + card.name);
            Deck.Add(card);
        }
    }

    public void shuffleDeck() {
        print("洗切牌组.");
        for (int i = Deck.Count - 1;i > 0;i--) {
            int tempnum = Random.Range(0,Deck.Count);
            Card temp = Deck[i];
            Deck[i] = Deck[tempnum];
            Deck[tempnum] = temp;
        }
    }

    public void initializeGY() {
        print("初始化墓地");
        Graveyard.Clear();
    }
    void initializeHand() {
        print("初始化手牌.");
        Hand.Clear();
        for (int i = 0; i < 5;i++)  drawCard();
    }

    void drawCard() {
        print("抓牌.");
        if (Deck.Count > 0 && Hand.Count < 10) {
            Hand[Hand.Count] = Deck[Deck.Count - 1];
            //似乎会溢出
            Deck[Deck.Count - 1] = null;
        } else if(Deck.Count <= 0){
            print("牌库已空.");
        } else if (Hand.Count >= 10) {
            print("手牌已满.");
        }
    }

    void playCard(Card card) {
        if (Hand.Contains(card)) {
            print("从手牌使用卡牌");
            Hand.Remove(card);
            card.play();
        }
    }
    
}
