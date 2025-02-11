using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckChooser : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject GameManager;
    public Card selectedCard;
    public List<Card> Deck = new List<Card>();
    void Start()
    {
        //����һ�����⣬������DeckManager�е�Deck����Start()�г�ʼ���ģ��������Start()�е�getDeck()����DeckManager�е�Start()֮ǰ����
        Invoke("getDeck",0.1f); 
        //getDeck();
        Invoke("initializeDropdown",0.2f);
        //initializeDropdown();
    }

    void Update() {
    }

    void initializeDropdown() {
        print("��ʼ��������.");
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (Card card in Deck) {
            print("��deckchooser�����: " + card.name);
            options.Add(card.name);
        }
        dropdown.AddOptions(options);
        // ���� Dropdown ��ѡ���¼�
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        selectedCard = Deck[0];
        print("ѡ��" + selectedCard.name);
    }

    public void getDeck() {
        Deck = GameManager.GetComponent<DeckManager>().Deck;
    }

    void OnDropdownValueChanged(int index) {
        if (index >= 0 && index < Deck.Count) {
            selectedCard = Deck[index]; // ���õ�ǰѡ��Ŀ���
            print("ѡ��" + selectedCard.name);
        }
    }
}
