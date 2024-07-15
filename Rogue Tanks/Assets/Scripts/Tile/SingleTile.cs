using System.Collections.Generic;
using UnityEngine;

public class SingleTile : MonoBehaviour
{
    public string Tile;
    private UnitTile[] unitTiles;

    private void Awake()
    {
        unitTiles = GetComponentsInChildren<UnitTile>();
    }

    public void Initialize(string tile, Sprite[] sprites, string collisionLayer)
    {
        Tile = tile;
        unitTiles.For((index, unitTile) => unitTile.Initialize(sprites[index], collisionLayer));
        gameObject.layer = LayerMask.NameToLayer(collisionLayer);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Tile.Length <= 0)
            return;

        var tankStats = collision.gameObject.GetComponentInChildren<TankStats>();
        if(tankStats != null)
        {
            tankStats.AddTileCollision(Tile);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Tile.Length <= 0)
            return;

        var tankStats = collision.gameObject.GetComponentInChildren<TankStats>();
        if(tankStats != null)
        {
            tankStats.RemoveTileCollision(Tile);
        }
    }
}
