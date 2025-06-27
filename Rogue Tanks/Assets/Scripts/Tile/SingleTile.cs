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

        if (gameObject.layer == LayerMask.NameToLayer("IceTile"))
        if(Tile.Length <= 0)
            return;
        if(Tile == "Ice" && gameObject.layer == LayerMask.NameToLayer("IceTile"))

        {
            collision.gameObject.GetComponentInParent<TankMovementController>().SlideIn();
            return;
        }

        var tankStats = collision.gameObject.GetComponentInChildren<TankStats>();
        if(tankStats != null)
        {
            tankStats.AddTileCollision(Tile);
        }
        // New code

        TankMovementController tank = collision.GetComponentInParent<TankMovementController>();
        GameEngine engine = GameObject.FindAnyObjectByType<GameEngine>();
        if (engine == null) return;
        if (tank == null) return;

        engine.EnvironmentEffectsHandler.HandleEnvirontmentEffect(gameObject, tank);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("IceTile"))
        if(Tile.Length <= 0)
            return;

        if(Tile == "Ice" && gameObject.layer == LayerMask.NameToLayer("IceTile"))
        {
            collision.gameObject.OnGetComponentInParent<TankMovementController>(movement => movement.SlideOut());
            return;
        }

        var tankStats = collision.gameObject.GetComponentInChildren<TankStats>();
        if(tankStats != null)
        {
            tankStats.RemoveTileCollision(Tile);
        }

        // New code

        TankMovementController tank = collision.GetComponentInParent<TankMovementController>();
        GameEngine engine = GameObject.FindAnyObjectByType<GameEngine>();
        if (engine == null) return;
        if (tank == null) return;

        engine.EnvironmentEffectsHandler.HandleEnvirontmentEffect(gameObject, tank);
    }
}
