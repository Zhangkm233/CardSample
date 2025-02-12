using Unity;
using UnityEngine;

public class CardMonster : Card
{
    public int attack;
    public int health;
    public int speed;
    public Sprite sprite;

    public CardMonster(int id,string name,Card.CardType type,int cost):base(id,name, CardType.MONSTER,cost){
        this.attack = 1;
        this.health = 1;
        this.speed = 1;
        this.sprite = null;
    }
    public CardMonster(int id,string name,Card.CardType type,int cost, int attack,int health, int speed, Sprite sprite):base(id,name, CardType.MONSTER,cost){
        this.attack = attack;
        this.health = health;
        this.speed = speed;
        this.sprite = sprite;
    }

    public CardMonster(int id,string name,Card.CardType type,int cost,int attack,int health,int speed) : base(id,name,CardType.MONSTER,cost) {
        this.attack = attack;
        this.health = health;
        this.speed = speed;
        this.sprite = null;
    }

    public override void play() {
        base.play();
        Debug.Log("สนำร" + name + ".");
    }
}