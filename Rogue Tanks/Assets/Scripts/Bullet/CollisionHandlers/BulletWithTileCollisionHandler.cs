using Unity.VisualScripting;
using UnityEngine;

public class BulletWithTileCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "Tile";

    public BulletWithTileCollisionHandler(BulletController bulletController) => this.bulletController = bulletController;

    public void Execute(Collider2D collision)
    {
        HandleBulletChange(collision);
      //  HandleTileChange(collision);
    }

    private void HandleBulletChange(Collider2D collision)
    {
        string layerType = LayerMask.LayerToName(collision.gameObject.layer);
        var singleTile = collision.GetComponent<SingleTile>();
        var groupTile = collision.GetComponent<GroupTile>();
        switch (bulletController.Type)
        {
            case BulletType.Standard:
                switch (layerType)
                {
                    case "IceTile":
                        bulletController.ChangeType(BulletType.Ice);
                        break;
                    case "WaterTile":
                        bulletController.ChangeType(BulletType.Water);
                        break;
                    case "MudTile":
                        bulletController.ChangeType(BulletType.Mud);
                        break;
                    case "LavaTile":
                        bulletController.ChangeType(BulletType.Fire);
                        break;
                    case "ForestTile":
                    case "BrickTile":
                    case "SteelTile":
                        break;
                }
                break;
            case BulletType.Fire:
                switch (layerType)
                {
                    case "IceTile":
                        bulletController.ChangeType(BulletType.Water);
                        break;
                    case "WaterTile":
                    case "MudTile":
                    case "ForestTile":
                        bulletController.ChangeType(BulletType.Standard);
                        break;
                    case "BrickTile":
                    case "SteelTile":
                        break;
                }
                break;
        
            case BulletType.Water:
                switch (layerType)
                {
                    case "IceTile":
                        bulletController.ChangeType(BulletType.Ice);
                        break;
                    case "MudTile":
                        bulletController.ChangeType(BulletType.Mud);
                        break;
                    case "LavaTile":
                    case "ForestTile":
                        bulletController.ChangeType(BulletType.Standard);
                        break;
                    case "BrickTile":
                    case "SteelTile":
                        break;
                }
                break;
            case BulletType.Ice:
                switch (layerType)
                {
                    case "WaterTile":
                        bulletController.ChangeType(BulletType.Standard);
                        break;
                    case "MudTile":
                        bulletController.ChangeType(BulletType.Mud);
                        break;
                    case "LavaTile":
                        bulletController.ChangeType(BulletType.Water);
                        break;
                    case "ForestTile":
                    case "BrickTile":
                    case "SteelTile":
                        break;
                }
                break;
            case BulletType.Mud:
                switch (layerType)
                {
                    case "WaterTile":
                        bulletController.ChangeType(BulletType.Water);
                        break;
                    case "IceTile":
                        bulletController.ChangeType(BulletType.Ice);
                        break;
                    case "LavaTile":
                        bulletController.ChangeType(BulletType.Standard);
                        break;
                    case "ForestTile":
                    case "BrickTile":
                    case "SteelTile":
                        break;
                }
                break;
            default:
                Debug.LogWarning($"Unhandled bullet type: {bulletController.Type}");
                break;
        }
    }
}


