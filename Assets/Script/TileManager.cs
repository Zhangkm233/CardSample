using UnityEngine;
public class TileManager : MonoBehaviour
{
    public enum TileType
    {
        EMPTY,
        EDGE,
        CORNER
    }
    public int x; // 格子的 x 坐标
    public int y; // 格子的 y 坐标
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
        // 自动调整碰撞器大小
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(1,1,0.1f); // 设置碰撞器大小
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