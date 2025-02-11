using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    public Camera mainCamera;

    void Update() {
        //
    }

    public GameObject ClickField() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit)) {
            GameObject targetTile = hit.collider.gameObject;
            return targetTile;
            //.GetComponent<TileManager>()
        }
        return null;
    }
}