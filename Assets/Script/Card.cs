using System.Data;
using UnityEngine;

public class Card
{
    public enum CardType
    {
        MONSTER,SPELL,TRAP
    }
    public CardType cardType;
    public string name;
    public int id;
    public int cost;

    public Card(int id, string name,CardType type,int cost) {
        this.id = id;
        this.name = name;
        this.cost = cost;
        this.cardType = type;
    }

    public virtual void play() {
        Debug.Log(" π”√ø®∆¨.");
    }
}
