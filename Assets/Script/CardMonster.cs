using Unity;
using UnityEngine;

public class CardMonster : Card
{
    public int attack;
    public int health;
    public int speed;
    public int sight;
    public Sprite sprite;
    public bool isFriendly;

    public CardMonster(int id,string name,Card.CardType type,int cost):base(id,name, CardType.MONSTER,cost){
        this.attack = 1;
        this.health = 1;
        this.speed = 1;
        this.sight = 1;
        this.sprite = null;
        this.isFriendly = true;
    }
    public CardMonster(int id,string name,Card.CardType type,int cost, int attack,int health, int speed, int sight,Sprite sprite):base(id,name, CardType.MONSTER,cost){
        this.attack = attack;
        this.health = health;
        this.speed = speed;
        this.sprite = sprite;
        this.sight = sight;
    }

    public CardMonster(int id,string name,Card.CardType type,int cost,int attack,int health,int speed,int sight,bool isFriendly) : base(id,name,CardType.MONSTER,cost) {
        this.attack = attack;
        this.health = health;
        this.speed = speed;
        this.sprite = null;
        this.sight = sight;
        this.isFriendly = isFriendly;
    }
    public CardMonster(int id,string name,Card.CardType type,int cost,int attack,int health,int speed,int sight) : base(id,name,CardType.MONSTER,cost) {
        this.attack = attack;
        this.health = health;
        this.speed = speed;
        this.sprite = null;
        this.sight = sight;
        this.isFriendly = true;
    }

    public override void play() {
        base.play();
        Debug.Log("สนำร" + name + ".");
    }
}