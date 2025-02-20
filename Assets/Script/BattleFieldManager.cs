using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
[SerializeField]
public class BattleFieldManager : MonoBehaviour
{
    public static int xfieldSize = GameData.xfieldSize;
    public static int yfieldSize = GameData.yfieldSize;
    [SerializeField] public Card[,] field;
    public GameObject tilePrefab;
    public float tileSize = 1.0f;
    void Start()
    {
        initializeField();
        initializeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeField() {
        print("初始化棋盘");
        field = new Card[xfieldSize,yfieldSize];
        for (int x = 0;x < xfieldSize;x++) {
            for (int y = 0;y < yfieldSize;y++) {
                field[x,y] = null;
                Vector3 position = new Vector3(x*tileSize-6,y * tileSize-4,0);
                // 实例化格子
                GameObject tile = Instantiate(tilePrefab,position,Quaternion.identity,transform);
                tile.name = "Tile (" + x + ", " + y + ")";

                TileManager tileScript = tile.GetComponent<TileManager>();
                if ((x == 0 && y == 0) || (x == xfieldSize - 1 && y == yfieldSize - 1) || (x == 0 && y == yfieldSize - 1) || (x == xfieldSize - 1 && y == 0)) {
                    tileScript.tileType = TileManager.TileType.CORNER;
                } else if ( x == 0 || x == xfieldSize - 1 || y == 0 || y == yfieldSize -1) {
                    tileScript.tileType = TileManager.TileType.EDGE;
                } else {
                    tileScript.tileType = TileManager.TileType.EMPTY;
                }

                if (x == 0 && y <= yfieldSize - 2) {
                    tileScript.GetComponent<Transform>().Rotate(0,0,90);
                }else if (y == 0 && x <= xfieldSize - 1) {
                    tileScript.GetComponent<Transform>().Rotate(0,0,180);
                }else if (x == xfieldSize - 1 && y <= yfieldSize - 1) {
                    tileScript.GetComponent<Transform>().Rotate(0,0,270);
                }

                tileScript.x = x;
                tileScript.y = y;
                tileScript.GameManager = this.gameObject;
            }
        }
    }

    public void initializeEnemy() {
        print("初始化敌人");
        Card[] enemyDeck = GameData.initialEnemy;
        for (int i = 0;i < enemyDeck.Length;i++) {
            Card card = enemyDeck[i];
            int x = Random.Range(0,xfieldSize);
            int y = Random.Range(0,yfieldSize);
            while (field[x,y] != null) {
                x = Random.Range(0,xfieldSize);
                y = Random.Range(0,yfieldSize);
            }
            addTile(x,y,card);
            GameObject cardObj = this.GetComponent<GameManager>().spawnMonster(card.name);
            cardObj.name = "Card " + x + "," + y;
            cardObj.GetComponent<CardController>().gameManager = this.GetComponent<GameManager>().gameObject;
            cardObj.GetComponent<CardController>().card = (CardMonster)card;
            cardObj.GetComponent<CardController>().x = x;
            cardObj.GetComponent<CardController>().y = y;
            cardObj.transform.position = new Vector2(x - GameData.xOffset,y - GameData.yOffset);
        }
    }

    public void clearTile(GameObject tile) {
        TileManager tileManager = tile.GetComponent<TileManager>();
        field[tileManager.x,tileManager.y] = null;
        print("清空:" + tileManager.x + "," + tileManager.y);
        tileManager.GetComponent<SpriteRenderer>().sprite = tileManager.emptySprite;
        tileManager.hasBeenClicked = false;
    }

    public void clearTile(int tileX,int tileY) {
        GameObject tile = GameObject.Find("Tile (" + tileX + ", " + tileY + ")");
        clearTile(tile);
    }

    public void addTile(GameObject tile,Card card) {
        TileManager tileManager = tile.GetComponent<TileManager>();
        field[tileManager.x,tileManager.y] = card;
        print("添加:" + tileManager.x + "," + tileManager.y + " " + card.name);
        tileManager.GetComponent<SpriteRenderer>().sprite = tileManager.clickSprite;
        tileManager.GetComponent<TileManager>().hasBeenClicked = true;
    }

    public void addTile(int tileX,int tileY,Card card) {
        GameObject tile = GameObject.Find("Tile (" + tileX + ", " + tileY + ")");
        TileManager tileManager = tile.GetComponent<TileManager>();
        field[tileX,tileY] = card;
        print("添加:" + tileManager.x + "," + tileManager.y + " " + card.name);
        tileManager.GetComponent<SpriteRenderer>().sprite = tileManager.clickSprite;
        tileManager.GetComponent<TileManager>().hasBeenClicked = true;
    }
}
