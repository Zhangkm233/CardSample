using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using static UnityEngine.InputManagerEntry;
using UnityEngine.UIElements;

public class CardController : MonoBehaviour
{
    public CardMonster card;
    public GameObject gameManager;
    public int x;
    public int y;
    public int sight;
    //public Sprite warriorSprite;
    //public Sprite mageSprite;
    //public Sprite rougeSprite;
    public bool isFriendly;
    protected BattleFieldManager battleFieldManager;
    protected GameObject newTile = null;
    protected GameObject oldTile = null;
    protected int speed;

    protected void Start() {
        initiallizeData();
        initiallizeManager();
    }

    protected void initiallizeData() {
        /*GetComponent<SpriteRenderer>().sprite = card.name switch {
            "warrior" => warriorSprite,
            "mage" => mageSprite,
            "rouge" => rougeSprite,
            _ => null
        };*/
        sight = card.sight;
        isFriendly = card.isFriendly;
    }

    public void initiallizeManager() {
        battleFieldManager = gameManager.GetComponent<BattleFieldManager>();
        newTile = GameObject.Find("Tile (" + x + ", " + y + ")");
        speed = card.speed;
    }

    public void State() {
        print(name + "开始行动");
        Card[,] field = battleFieldManager.field;
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
                        //这里写的有隐患，如果超出了边界，会报错，所以先写成这样
                        CardMonster cardMonster = field[tempX,tempY] as CardMonster;
                        if (cardMonster.isFriendly == false && hasFindEnemy == false) {
                            minX = tempX;
                            minY = tempY;
                            hasFindEnemy = true;
                            continue;
                        }
                        if (cardMonster.isFriendly == false) {
                            print("在" + (tempX) + "," + (tempY) + "发现敌人");
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
                print(name + "攻击" + minX + "," + minY);
                Attack(minX,minY);
            } else {
                print(name + "试图移动");
                Move();
            }
        }
    }
    
    public virtual void Die() {
            
    }

    public virtual void Attack(int targetX,int targetY) {
        
    }

    public virtual void Move() {
        if (IsReachAble(x + speed,y) == false) return;
        
        if (IsReachAble(x + speed,y)) {
            MoveTo(x + speed,y);
        } else {
            print(name + "无法移动");
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
        Card[,] field = battleFieldManager.field;
        x += speed;
        print(this.name + "移动到" + goX + "," + goY);

        oldTile = newTile;
        newTile = GameObject.Find("Tile (" + goX + ", " + goY + ")");

        battleFieldManager.clearTile(oldTile);
        battleFieldManager.addTile(newTile,card);
        
        transform.position = new Vector2(goX - GameData.xOffset,goY - GameData.yOffset);
        this.name = "Card " + goX + "," + goY;
    }
}
