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
        //这里一个问题，就是在DeckManager中的Deck是在Start()中初始化的，而这里的Start()中的getDeck()是在DeckManager中的Start()之前调用
        Invoke("getDeck",0.1f); 
        //getDeck();
        Invoke("initializeDropdown",0.2f);
        //initializeDropdown();
    }

    void Update() {
    }

    void initializeDropdown() {
        print("初始化下拉框.");
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (Card card in Deck) {
            print("在deckchooser添加了: " + card.name);
            options.Add(card.name);
        }
        dropdown.AddOptions(options);
        // 监听 Dropdown 的选择事件
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        selectedCard = Deck[0];
        print("选择" + selectedCard.name);
    }

    public void getDeck() {
        Deck = GameManager.GetComponent<DeckManager>().Deck;
    }

    void OnDropdownValueChanged(int index) {
        if (index >= 0 && index < Deck.Count) {
            selectedCard = Deck[index]; // 设置当前选择的卡牌
            print("选择" + selectedCard.name);
        }
    }
}
