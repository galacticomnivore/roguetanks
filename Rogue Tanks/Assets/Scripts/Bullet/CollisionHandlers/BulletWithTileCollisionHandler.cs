using UnityEngine;

public class BulletWithTileCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "Tile";

    public BulletWithTileCollisionHandler(BulletController bulletController) => this.bulletController = bulletController;

     public void Execute(Collider2D collision)
     {

        var tileGameObject = collision.gameObject;
        int tileLayer = tileGameObject.layer;
        string layerName = LayerMask.LayerToName(tileLayer);

        if (tileLayer == LayerMask.NameToLayer("LavaTile"))
        {
            bulletController.ChangeType(BulletType.Fire);
        }
        else if (tileLayer == LayerMask.NameToLayer("WaterTile"))
        {
            bulletController.ChangeType(BulletType.Water);
        }
        else if (tileLayer == LayerMask.NameToLayer("MudTile"))
        {
            bulletController.ChangeType(BulletType.Mud);
        }
        else if (tileLayer == LayerMask.NameToLayer("IceTile"))
        {
            bulletController.ChangeType(BulletType.Ice);
        }
        else
        {
            bulletController.Deactivate();
        }

        var unitTile = tileGameObject.GetComponent<UnitTile>();
        if (unitTile != null)
        {
            unitTile.Hit(bulletController);
        }
     }
}
