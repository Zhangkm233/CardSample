using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class CardController : MonoBehaviour
{
    public CardMonster card;
    public GameObject gameManager;
    public int x;
    public int y;
    public Sprite warriorSprite;
    public Sprite mageSprite;
    public Sprite rougeSprite;
    BattleFieldManager battleFieldManager;
    GameObject newTile = null;
    GameObject oldTile = null;
    int speed;

    private void Start() {
        initiallizeSprite();
        initiallizeManager();
    }

    void initiallizeSprite() {
        GetComponent<SpriteRenderer>().sprite = card.name switch {
            "warrior" => warriorSprite,
            "mage" => mageSprite,
            "rouge" => rougeSprite,
            _ => null
        };
    }

    public void initiallizeManager() {
        battleFieldManager = gameManager.GetComponent<BattleFieldManager>();
        newTile = GameObject.Find("Tile (" + x + ", " + y + ")");
        speed = card.speed;
    }

    public void movement() {
        newTile = GameObject.Find("Tile (" + x + ", " + y + ")");
        Card[,] field = battleFieldManager.field;
        
        if (IsReachAble(x+speed,y)) {
            moveTo(x + speed,y);
        }
    }
    bool IsReachAble(int goX,int goY) {
        Card[,] field = battleFieldManager.field;
        if (goX < 0) return false; 
        if (goY < 0) return false;
        if (goX > GameData.xfieldSize-1) return false;
        if (goY > GameData.yfieldSize-1) return false;
        if (field[goX,goY] != null) return false;
        return true;
    }

    void moveTo(int x,int y) {
        Card[,] field = battleFieldManager.field;
        battleFieldManager.changeFieldTile(x,y,null);
        field[x,y] = null;
        print(this.name + "ÒÆ¶¯µ½" + (x + speed) + "," + y);
        x += speed;
        oldTile = newTile;
        newTile = GameObject.Find("Tile (" + x + ", " + y + ")");

        TileManager oldTileManager = oldTile.GetComponent<TileManager>();
        print("oldTile:" + oldTileManager.x + "," + oldTileManager.y);
        oldTileManager.GetComponent<SpriteRenderer>().sprite = oldTileManager.emptySprite;
        oldTileManager.GetComponent<TileManager>().hasBeenClicked = false;

        TileManager newTileManager = newTile.GetComponent<TileManager>();
        newTileManager.GetComponent<SpriteRenderer>().sprite = newTileManager.clickSprite;
        oldTileManager.GetComponent<TileManager>().hasBeenClicked = true;

        transform.position = new Vector2(x - GameData.xOffset,y - GameData.yOffset);
        battleFieldManager.changeFieldTile(x,y,this.card);

        this.name = "Card " + x + "," + y;
        field[x,y] = this.card;
    }
}
