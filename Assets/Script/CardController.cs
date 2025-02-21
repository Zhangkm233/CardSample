using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using static UnityEngine.InputManagerEntry;
using UnityEngine.UIElements;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class CardController : MonoBehaviour
{
    public CardMonster card;
    public GameObject gameManager;
    public Text healthText;
    public int x;
    public int y;
    public int sight;
    public int health;
    //public Sprite warriorSprite;
    //public Sprite mageSprite;
    //public Sprite rougeSprite;
    public bool isFriendly;
    protected BattleFieldManager battleFieldManager;
    protected GameObject newTile = null;
    protected GameObject oldTile = null;
    protected int speed;
    Card[,] field;
    protected void Start() {
        initiallizeData();
        initiallizeManager();
    }

    protected void initiallizeData() {
        sight = card.sight;
        isFriendly = card.isFriendly;
        health = card.health;
        speed = card.speed;
    }

    public void initiallizeManager() {
        battleFieldManager = gameManager.GetComponent<BattleFieldManager>();
        newTile = GameObject.Find("Tile (" + x + ", " + y + ")");
        healthText = this.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        healthText.text = health.ToString();
    }

    public void UpdateData() {
        healthText.text = health.ToString();
    }

    public void State() {
        UpdateData();
        print(name + "��ʼ�ж�");
        field = battleFieldManager.field;
        if (sight > 0) {
            int minX = 999, minY = 999;
            bool hasFindEnemy = false;
            for (int i = -sight;i <= sight;i++) {
                for (int j = - sight;j <= sight;j++) {
                    if (x + i > GameData.xfieldSize - 1 || y + j > GameData.yfieldSize - 1 || x + i < 0 || y + i < 0) continue;
                    int tempX = Mathf.Min(Mathf.Max(x + i,0),GameData.xfieldSize - 1);
                    int tempY = Mathf.Min(Mathf.Max(y + j,0),GameData.yfieldSize - 1);
                    if (field[tempX,tempY] != null) {
                        if (field[tempX,tempY].cardType != Card.CardType.MONSTER) continue;
                        //����д������������������˱߽磬�ᱨ��������д������
                        CardMonster cardMonster = field[tempX,tempY] as CardMonster;
                        if (cardMonster.isFriendly == false && hasFindEnemy == false) {
                            minX = tempX;
                            minY = tempY;
                            hasFindEnemy = true;
                            continue;
                        }
                        if (cardMonster.isFriendly == false) {
                            print("��" + (tempX) + "," + (tempY) + "���ֵ���");
                            hasFindEnemy = true;
                            if (Mathf.Abs(i) + Mathf.Abs(j) < Mathf.Abs(minX - x) + Mathf.Abs(minY - y)) {
                                minX = tempX;
                                minY = tempY;
                            }
                        }
                    }
                }
            }
            if (hasFindEnemy) {
                print(name + "����" + minX + "," + minY);
                Attack(minX,minY);
            } else {
                print(name + "��ͼ�ƶ�");
                Move();
            }
        }
    }
    
    public virtual void Die() {
            Destroy(this.gameObject);
    }

    public virtual void Attack(int targetX,int targetY) {
        //print(name + "��ʼ����" + targetX + " " + targetY);
        field = battleFieldManager.field;
        //CardMonster targetMonster = (CardMonster)field[targetX,targetY];
        GameObject targetMonsterGameobject = GameObject.Find("Card " + targetX + "," + targetY);
        //targetMonster.health -= card.attack;
        targetMonsterGameobject.GetComponent<CardController>().health -= card.attack;
        print(name + "��" + targetX + "," + targetY + "���" + card.attack + "���˺���ʣ��Ѫ����" + targetMonsterGameobject.GetComponent<CardController>().health);
        targetMonsterGameobject.GetComponent<CardController>().UpdateData();
        //targetMonsterGameobject.GetComponent<CardController>().UpdateData();
        if (targetMonsterGameobject.GetComponent<CardController>().health <= 0) {
            battleFieldManager.clearTile(targetX,targetY);
            targetMonsterGameobject.GetComponent<CardController>().Die();
        }
    }

    public virtual void Move() {
        if (IsReachAble(x + speed,y) == false) return;
        
        if (IsReachAble(x + speed,y)) {
            MoveTo(x + speed,y);
        } else {
            print(name + "�޷��ƶ�");
        }
    }
    protected bool IsReachAble(int goX,int goY) {
        Card[,] field = battleFieldManager.field;
        if (goX < 0) return false; 
        if (goY < 0) return false;
        if (goX > GameData.xfieldSize-1) return false;
        if (goY > GameData.yfieldSize-1) return false;
        if (field[goX,goY] != null) return false;
        return true;
    }

    protected void MoveTo(int goX,int goY) {
        field = battleFieldManager.field;
        x += speed;
        print(this.name + "�ƶ���" + goX + "," + goY);

        oldTile = newTile;
        newTile = GameObject.Find("Tile (" + goX + ", " + goY + ")");

        battleFieldManager.clearTile(oldTile);
        battleFieldManager.addTile(newTile,card);
        
        transform.position = new Vector2(goX - GameData.xOffset,goY - GameData.yOffset);
        this.name = "Card " + goX + "," + goY;
    }
}
