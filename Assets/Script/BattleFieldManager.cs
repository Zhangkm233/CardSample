using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour
{
    public static int xfieldSize = GameData.xfieldSize;
    public static int yfieldSize = GameData.yfieldSize;
    public Card[,] field;
    public GameObject tilePrefab;
    public float tileSize = 1.0f;
    void Start()
    {
        initializeField();
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
    public void changeFieldTile(int x, int y, Card card) {
        if (field[x,y] != null) {
            field[x,y] = card;
        }
    }
}
