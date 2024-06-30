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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("IceTile"))
        {
            collision.gameObject.GetComponentInParent<TankMovementController>().SlideIn();
        }
        else if(gameObject.layer == LayerMask.NameToLayer("LavaTile"))
        {
            collision.gameObject.GetComponentInChildren<TankStats>().IncrementLavaCounter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("IceTile"))
        {
            collision.gameObject.OnGetComponentInParent<TankMovementController>(movement => movement.SlideOut());
        }
        else if(gameObject.layer == LayerMask.NameToLayer("LavaTile"))
        {
            collision.gameObject.GetComponentInChildren<TankStats>().DecrementLavaCounter();
        }
    }
}
