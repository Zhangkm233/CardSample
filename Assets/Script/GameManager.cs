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
        // ��ʼ����Ϸ����
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
        print("��һ�غ�");
        GameData.gameState = GameData.GameState.PLAYING;
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject card in cards) {
            //print(card.name + "�ж�");
            card.GetComponent<CardController>().State();
        }
        print("�غϽ���");
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
        //����ʱ����׼���׶ε��¼�
        if (deckChooser.selectedCard != null) {
            if (mouseInputManager.ClickField() != null) {
                GameObject targetTile = mouseInputManager.ClickField();
                Card selectCard = deckChooser.selectedCard;
                TileManager tileManager = targetTile.GetComponent<TileManager>();

                if (tileManager.hasBeenClicked == false && selectCard.cardType == Card.CardType.MONSTER) {
                    //�������û�б����������ѡ����ǹ��޿�
                    CardMonster selectCardMonster = (CardMonster)deckChooser.selectedCard;
                    print(tileManager.x + " " + tileManager.y + " " + selectCard.name);
                    //���ö�ά�����ֵ
                    battleFieldManager.field[tileManager.x,tileManager.y] = selectCard;
                    SpriteRenderer spriteRenderer = tileManager.GetComponent<SpriteRenderer>();
                    //���ø��ӵ�ͼƬ
                    if (spriteRenderer != null) {
                        spriteRenderer.sprite = tileManager.clickSprite;
                    }
                    tileManager.hasBeenClicked = true;

                    GameData.totalCost += battleFieldManager.field[tileManager.x,tileManager.y].cost;

                    int WorldX = tileManager.x - 6;
                    int WorldY = tileManager.y - 4;
                    // ����Ԥ����
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

        print(tileManager.x + " " + tileManager.y + " " + "ɾ��");
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
