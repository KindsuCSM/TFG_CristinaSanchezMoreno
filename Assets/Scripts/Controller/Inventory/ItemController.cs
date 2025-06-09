using UnityEngine;

[RequireComponent(typeof(ItemView))]
public class ItemController : MonoBehaviour
{
    // Variables
    public PlantsData plantsData;
    public int quantity = 1;

    private SpriteRenderer spriteRenderer;
    private InventoryController inventory;

    // Obtenemos el SpriteRenderer
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = plantsData.Icon; // Le damos el sprite del PlantsData 

        inventory = GameObject.Find("InventoryPanel").GetComponent<InventoryController>(); // Obtenemos de los objetos del juego el inventario
    }

    // En caso de que el item colisione con el jugador, se añadirá el item en el inventario y luego se eliminará el objeto del mundo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventory.AddItem(plantsData, quantity);
            Destroy(gameObject);
        }
    }
}
