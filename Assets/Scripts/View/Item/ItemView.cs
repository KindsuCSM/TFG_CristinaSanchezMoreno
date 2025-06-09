using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemView : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
    }
}
