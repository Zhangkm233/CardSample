using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Canvas;
    public GameObject warriorPrefab;
    DeckChooser deckChooser;
    MouseInputManager mouseInputManager;
    BattleFieldManager battleFieldManager;
    //public Card[,] field;
    void Start() {
        initiallizeGameData();
        initiallizeManagers();
        goPlanning();
    }


    void Update() {
        clickEvent();
    }

    void initiallizeGameData() {
        // 初始化游戏数据
        GameData.gameState = GameData.GameState.STANDBY;
        GameData.turn = 0;
        GameData.totalCost = 0;
    }
    void initiallizeManagers() {
        mouseInputManager = MainCamera.GetComponent<MouseInputManager>();
        deckChooser = Canvas.GetComponent<DeckChooser>();
        battleFieldManager = this.GetComponent<BattleFieldManager>();
        //field = battleFieldManager.field;
    }

    void goPlanning() {
        GameData.gameState = GameData.GameState.PLANNING;
    }


    void clickEvent() {
        if (Input.GetMouseButtonDown(0)) {
            if (GameData.gameState == GameData.GameState.PLANNING) {
                spawnPrefab();
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            if (GameData.gameState == GameData.GameState.PLANNING) {
                cancelPrefab();
            }
        }
    }

    void spawnPrefab() {
        //按下时，在准备阶段的事件
        if (deckChooser.selectedCard != null) {
            if (mouseInputManager.ClickField() != null) {
                GameObject targetTile = mouseInputManager.ClickField();
                Card selectCard = deckChooser.selectedCard;
                TileManager tileManager = targetTile.GetComponent<TileManager>();

                if (tileManager.hasBeenClicked == false) {
                    print(tileManager.x + " " + tileManager.y + " " + selectCard.name);
                    //设置二维数组的值
                    battleFieldManager.field[tileManager.x,tileManager.y] = selectCard;
                    SpriteRenderer spriteRenderer = tileManager.GetComponent<SpriteRenderer>();
                    //设置格子的图片
                    if (spriteRenderer != null) {
                        spriteRenderer.sprite = tileManager.clickSprite;
                    }
                    tileManager.hasBeenClicked = true;
                    GameData.totalCost += battleFieldManager.field[tileManager.x,tileManager.y].cost;

                    int WorldX = tileManager.x - 6;
                    int WorldY = tileManager.y - 4;
                    // 生成预制体
                    GameObject cardObj = GameObject.Instantiate(warriorPrefab);
                    cardObj.transform.position = new Vector2(WorldX,WorldY);
                    cardObj.name = "warrior " + tileManager.x + "," + tileManager.y;
                    cardObj.GetComponent<Warrior>().card = selectCard;
                    cardObj.GetComponent<Warrior>().x = WorldX;
                    cardObj.GetComponent<Warrior>().y = WorldY;
                }
            }
        }
    }

    void cancelPrefab() {
        if (mouseInputManager.ClickField() == null) return;

        GameObject targetTile = mouseInputManager.ClickField();
        Card selectCard = deckChooser.selectedCard;
        TileManager tileManager = targetTile.GetComponent<TileManager>();
        SpriteRenderer spriteRenderer = tileManager.GetComponent<SpriteRenderer>();

        if (tileManager.hasBeenClicked == false) return;

        print(tileManager.x + " " + tileManager.y + " " + "删除");
        GameData.totalCost -= battleFieldManager.field[tileManager.x,tileManager.y].cost;
        battleFieldManager.field[tileManager.x,tileManager.y] = null;

        spriteRenderer.sprite = tileManager.emptySprite;
        tileManager.hasBeenClicked = false;

        GameObject cardObj = GameObject.Find("warrior " + tileManager.x + "," + tileManager.y);
        if (cardObj != null) {
            Destroy(cardObj);
        }
    }
}
