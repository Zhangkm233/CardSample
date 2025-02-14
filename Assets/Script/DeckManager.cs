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
        print("��ʼ������.");
        Deck.Clear();
        foreach (Card card in GameData.initialDeck) {
            print("���������������" + card.name);
            Deck.Add(card);
        }
    }

    public void shuffleDeck() {
        print("ϴ������.");
        for (int i = Deck.Count - 1;i > 0;i--) {
            int tempnum = Random.Range(0,Deck.Count);
            Card temp = Deck[i];
            Deck[i] = Deck[tempnum];
            Deck[tempnum] = temp;
        }
    }

    public void initializeGY() {
        print("��ʼ��Ĺ��");
        Graveyard.Clear();
    }
    void initializeHand() {
        print("��ʼ������.");
        Hand.Clear();
        for (int i = 0; i < 5;i++)  drawCard();
    }

    void drawCard() {
        print("ץ��.");
        if (Deck.Count > 0 && Hand.Count < 10) {
            Hand[Hand.Count] = Deck[Deck.Count - 1];
            //�ƺ������
            Deck[Deck.Count - 1] = null;
        } else if(Deck.Count <= 0){
            print("�ƿ��ѿ�.");
        } else if (Hand.Count >= 10) {
            print("��������.");
        }
    }

    void playCard(Card card) {
        if (Hand.Contains(card)) {
            print("������ʹ�ÿ���");
            Hand.Remove(card);
            card.play();
        }
    }
    
}
