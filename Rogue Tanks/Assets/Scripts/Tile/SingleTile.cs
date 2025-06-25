using System.Collections.Generic;
using UnityEngine;

public class SingleTile : MonoBehaviour
{
    public List<StatEffect> StatEffects;


    public void Initialize(Sprite sprite, string collisionLayer)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = sprite;
        gameObject.layer = LayerMask.NameToLayer(collisionLayer);

        Debug.Log($"[TILE] {name} set to layer {collisionLayer} ({gameObject.layer})"); // novo dodadeno - funkcioniraat site tiles
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("IceTile"))
        {
            var movement = collision.gameObject.GetComponentInParent<TankMovementController>();
            
            if (movement != null)
            {
                movement.SlideIn();
            }
        }
        else if(gameObject.layer == LayerMask.NameToLayer("LavaTile"))
        {
            var stats = collision.gameObject.GetComponentInChildren<TankStats>(); if (stats != null)
            {
                stats.IncrementLavaCounter();
            }
        }
        else if(gameObject.layer == LayerMask.NameToLayer("MudTile"))
        {
            var tankStats = collision.gameObject.GetComponentInChildren<TankStats>();
            if(tankStats != null)
            {
                StatEffects.ForEach(x => tankStats.AddStatEffect(x, false));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("IceTile"))
        {
            var movement = collision.gameObject.GetComponentInParent<TankMovementController>();
            if (movement != null)
            {
                movement.SlideOut();
            }
        }
        else if(gameObject.layer == LayerMask.NameToLayer("LavaTile"))
        {
            var stats = collision.gameObject.GetComponentInChildren<TankStats>();
            if (stats != null)
            {
                stats.DecrementLavaCounter();
            }
        }
        else if(gameObject.layer == LayerMask.NameToLayer("MudTile"))
        {
            var tankStats = collision.gameObject.GetComponentInChildren<TankStats>();
            if(tankStats != null)
            {
                StatEffects.ForEach(x => tankStats.RemoveStatEffect(x.Tag));
            }
        }
    }
}
