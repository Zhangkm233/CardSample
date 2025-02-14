using UnityEngine;
public class TileManager : MonoBehaviour
{
    public enum TileType
    {
        EMPTY,
        EDGE,
        CORNER
    }
    public int x; // ���ӵ� x ����
    public int y; // ���ӵ� y ����
    public TileType tileType;
    public Card CardOn = null;
    public GameObject GameManager;

    public Sprite cornerSprite;
    public Sprite edgeSprite;
    public Sprite emptySprite;
    public Sprite pointSprite;
    public Sprite clickSprite;
    public bool hasBeenClicked = false;


    void Start() {
        // �Զ�������ײ����С
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(1,1,0.1f); // ������ײ����С
        if (hasBeenClicked) return;
        if(tileType == TileType.CORNER) {
            GetComponent<SpriteRenderer>().sprite = cornerSprite;
            emptySprite = cornerSprite;
        } else if (tileType == TileType.EDGE) {
            GetComponent<SpriteRenderer>().sprite = edgeSprite;
            emptySprite = edgeSprite;
        } else {
            GetComponent<SpriteRenderer>().sprite = emptySprite;
        }
    }

    void Update() {
    }

    private void OnMouseEnter() {
        if (!hasBeenClicked) GetComponent<SpriteRenderer>().sprite = pointSprite;
    }
    private void OnMouseDown() {

    }
    private void OnMouseExit() {
        if (!hasBeenClicked) GetComponent<SpriteRenderer>().sprite = emptySprite;
    }

}