using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerView : MonoBehaviour
{
    public Tilemap referenceTileMap;
    public GameObject tileSelectorPrefab;
    private GameObject tileSelectorInstance;

    private Animator animator;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }


    public void InitializeView()
    {
        referenceTileMap = GameObject.Find("Background").GetComponent<Tilemap>();
        tileSelectorInstance = Instantiate(tileSelectorPrefab);
    }

    public void UpdateAnimation(Vector2 moveInput, bool isRunning)
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("moveHorizontal", moveInput.x);
            animator.SetFloat("moveVertical", moveInput.y);
            animator.SetBool("isMoving", true);
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", false);
        }
    }

    public void ShowTileSelector(Vector3Int targetCell)
    {
        if (referenceTileMap == null || tileSelectorInstance == null) return;

        Vector3 worldPos = referenceTileMap.CellToWorld(targetCell) + referenceTileMap.tileAnchor;
        tileSelectorInstance.transform.position = worldPos;
        tileSelectorInstance.SetActive(true);
    }
}
