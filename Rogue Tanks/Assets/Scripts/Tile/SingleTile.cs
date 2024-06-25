using UnityEngine;

public class SingleTile : MonoBehaviour
{
    public void Initialize(Sprite sprite, string collisionLayer)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = sprite;
        gameObject.layer = LayerMask.NameToLayer(collisionLayer);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) =>
        collision.gameObject.GetComponentInParent<TankMovementController>().SlideIn();

    private void OnTriggerExit2D(Collider2D collision) =>
        collision.gameObject.OnGetComponentInParent<TankMovementController>(movement => movement.SlideOut());
}
