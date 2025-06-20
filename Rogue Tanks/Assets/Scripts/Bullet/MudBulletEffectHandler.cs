using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudBulletEffectHandler : MonoBehaviour, IBulletEffectHandler
{
    private GameFactory gameFactory;
    private BulletController bulletType;
    private GameTiles gameTiles;
    private TileModifier tileModifier;


    //public void Initialize(GameFactory factory)
    //{
    //    this.gameFactory = factory;
    //    tileModifier = new TileModifier(factory);
    //}

    public void ApplyEffect(Collider2D collision, BulletController bullet)
    {
        var singleTile = collision.GetComponent<SingleTile>();
        var groupTile = collision.GetComponent<GroupTile>();
        var gameTiles = collision.GetComponent<GameTiles>();
        var unitTile = collision.GetComponent<UnitTile>();

        if (singleTile == null) return;
        if (groupTile == null) return;

        int tileLayer = singleTile.gameObject.layer;
        int groupTileLayer = groupTile.gameObject.layer;

        if (tileLayer == LayerMask.NameToLayer("LavaTile"))
        {
            ReplaceTileWithBricks(singleTile);
            bullet.ChangeType(BulletType.Standard);
        }
        else if (tileLayer == LayerMask.NameToLayer("WaterTile"))
        {
           bulletType.ChangeType(BulletType.Water); 
        }
        else if (tileLayer == LayerMask.NameToLayer("IceTile"))
        {
           bulletType.ChangeType(BulletType.Ice);
        }
        else if (groupTileLayer == LayerMask.NameToLayer("BrickTile"))
        {
            gameTiles.Hit(bullet, groupTile, unitTile);
        }
        else if (groupTileLayer == LayerMask.NameToLayer("Steeltile"))
        {
            //destroy bullet
            //Turn 1/4 Steel into Bricks Tile
        }
    }

    private void ReplaceTileWithBricks(SingleTile tile)
    {
        Vector3 position = tile.transform.position;
        string[] nameParts = tile.name.Split('_');
        int row = nameParts.Length > 1 ? nameParts[1].ToInt() : 0;
        int col = nameParts.Length > 2 ? nameParts[2].ToInt() : 0;

        Destroy(tile.gameObject);
        gameFactory.CreateBrick(position, row, col);
    }
}
