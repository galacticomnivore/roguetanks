using System.Collections.Generic;
using UnityEngine;

public class BulletWithTileCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    private GameTiles gameTiles;

    private Dictionary<BulletTilePair, BulletTilePair> bulletTileStateMap = new()
    {
        {new BulletTilePair(BulletType.Standard, "IceTile"), new BulletTilePair(BulletType.Ice, null)},
        {new BulletTilePair(BulletType.Standard, "WaterTile"), new BulletTilePair(BulletType.Water, null)},
        {new BulletTilePair(BulletType.Standard, "MudTile"), new BulletTilePair(BulletType.Mud, null)},
        {new BulletTilePair(BulletType.Standard, "LavaTile"), new BulletTilePair(BulletType.Fire, null)},
        {new BulletTilePair(BulletType.Standard, "BrickTile"), new BulletTilePair(BulletType.None, null)},
        {new BulletTilePair(BulletType.Standard, "SteelTile"), new BulletTilePair(BulletType.None, null)},
        {new BulletTilePair(BulletType.Fire, "IceTile"), new BulletTilePair(BulletType.Standard, "WaterTile")},
        {new BulletTilePair(BulletType.Fire, "WaterTile"), new BulletTilePair(BulletType.Standard, "MudTile")},
        {new BulletTilePair(BulletType.Fire, "MudTile"), new BulletTilePair(BulletType.Standard, "BrickTile")},
        {new BulletTilePair(BulletType.Fire, "ForestTile"), new BulletTilePair(BulletType.Standard, null)},
        {new BulletTilePair(BulletType.Water, "IceTile"), new BulletTilePair(BulletType.Ice, "BrickTile")}, // But Whyy
        {new BulletTilePair(BulletType.Water, "LavaTile"), new BulletTilePair(BulletType.Standard, "BrickTile")},
        {new BulletTilePair(BulletType.Water, "MudTile"), new BulletTilePair(BulletType.Mud, "WaterTile")},
        {new BulletTilePair(BulletType.Water, "ForestTile"), new BulletTilePair(BulletType.Standard, null)}, // TODO: check which tile should be spawned 
        {new BulletTilePair(BulletType.Ice, "WaterTile"), new BulletTilePair(BulletType.Standard, "IceTile")},
        {new BulletTilePair(BulletType.Ice, "LavaTile"), new BulletTilePair(BulletType.Water, "BrickTile")},
        {new BulletTilePair(BulletType.Ice, "MudTile"), new BulletTilePair(BulletType.Mud, "BrickTile")},
        {new BulletTilePair(BulletType.Mud, "WaterTile"), new BulletTilePair(BulletType.Water, null)},
        {new BulletTilePair(BulletType.Mud, "LavaTile"), new BulletTilePair(BulletType.Standard, "BrickTile")},
        {new BulletTilePair(BulletType.Mud, "IceTile"), new BulletTilePair(BulletType.Ice, null)},
        {new BulletTilePair(BulletType.Mud, "SteelTile"), new BulletTilePair(BulletType.None, "BrickTile")},
        {new BulletTilePair(BulletType.Water, "SteelTile"), new BulletTilePair(BulletType.None, "BrickTile")},
        {new BulletTilePair(BulletType.Ice, "SteelTile"), new BulletTilePair(BulletType.None, "BrickTile")},
    };
    public string CollisionTag => "Tile";

    public BulletWithTileCollisionHandler(BulletController bulletController)
    {
        this.bulletController = bulletController;
        this.gameTiles = GameObject.FindObjectOfType<GameTiles>();
    }
    public void Execute(Collider2D collision)
    {
        HandleBulletChange(collision);
    }

    private void HandleBulletChange(Collider2D collision)
    {
        string layerType = LayerMask.LayerToName(collision.gameObject.layer);
        var singleTile = collision.GetComponent<SingleTile>();
        var groupTile = collision.GetComponent<GroupTile>();
        BulletTilePair collisionData = new BulletTilePair(bulletController.Type, layerType);
        if (!bulletTileStateMap.ContainsKey(collisionData))
        {
          //  Debug.LogWarning($"No collision data found for bullet type {bulletController.Type} and tile layer {layerType}");
            return;
        }
        BulletTilePair collisionOutcomeData = bulletTileStateMap[collisionData];
        if (collisionOutcomeData.type != BulletType.None)
        {
            bulletController.ChangeType(collisionOutcomeData.type);
        }
        if (collisionOutcomeData.tileName != null)
        {
            gameTiles.ChangeTileType(collision.gameObject, collisionOutcomeData.tileName);
        }
    }
}

struct BulletTilePair
{
    public BulletType type;
    public string tileName;
    public BulletTilePair(BulletType type, string tileName)
    {
        this.type = type;
        this.tileName = tileName;
    }
}


