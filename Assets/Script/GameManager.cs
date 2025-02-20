using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Canvas;
    public GameObject CardPrefab;
    public GameObject GoplayButton;
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

    public void goPlanning() {
        GameData.gameState = GameData.GameState.PLANNING;
    }
    public void goPlay() {
        print("下一回合");
        GameData.gameState = GameData.GameState.PLAYING;
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject card in cards) {
            //print(card.name + "行动");
            card.GetComponent<CardController>().State();
        }
        print("回合结束");
        goPlanning();
        GoplayButton.SetActive(true);
    }

    void clickEvent() {
        if (Input.GetMouseButtonDown(0)) {
            if (GameData.gameState == GameData.GameState.PLANNING) {
                deployPrefab();
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            if (GameData.gameState == GameData.GameState.PLANNING) {
                deletePrefab();
            }
        }
    }

    public void deployPrefab() {
        //按下时，在准备阶段的事件
        if (deckChooser.selectedCard != null) {
            if (mouseInputManager.ClickField() != null) {
                GameObject targetTile = mouseInputManager.ClickField();
                Card selectCard = deckChooser.selectedCard;
                TileManager tileManager = targetTile.GetComponent<TileManager>();

                if (tileManager.hasBeenClicked == false && selectCard.cardType == Card.CardType.MONSTER) {
                    //如果格子没有被点击过，且选择的是怪兽卡
                    CardMonster selectCardMonster = (CardMonster)deckChooser.selectedCard;
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
                    GameObject cardObj = spawnMonster(selectCard.name);
                    cardObj.transform.position = new Vector2(WorldX,WorldY);
                    cardObj.name = "Card " + tileManager.x + "," + tileManager.y;
                    cardObj.GetComponent<CardController>().card = (CardMonster)battleFieldManager.field[tileManager.x,tileManager.y];
                    cardObj.GetComponent<CardController>().gameManager = this.gameObject;
                    cardObj.GetComponent<CardController>().x = tileManager.x;
                    cardObj.GetComponent<CardController>().y = tileManager.y;
                }
            }
        }
    }

    public GameObject spawnMonster(string monsterName) {
        if (monsterName == null) return null;
        CardPrefab = monsterName switch {
            "warrior" => Resources.Load<GameObject>("WarriorPrefab"),
            "mage" => Resources.Load<GameObject>("MagePrefab"),
            "rouge" => Resources.Load<GameObject>("RougePrefab"),
            "goblin" => Resources.Load<GameObject>("GoblinPrefab"),
            _ => null
        };
        return GameObject.Instantiate(CardPrefab);
    }
    public void deletePrefab() {
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

        GameObject cardObj = GameObject.Find("Card " + tileManager.x + "," + tileManager.y);
        if (cardObj != null) {
            Destroy(cardObj);
        }
    }
}
