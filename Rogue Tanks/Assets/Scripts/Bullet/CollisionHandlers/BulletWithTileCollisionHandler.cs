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
        Debug.Log($"[BULLET DEBUG] Collided with: {tileGameObject.name}, Layer: {tileLayer} ({layerName})");

        if (tileLayer == LayerMask.NameToLayer("LavaTile"))
        {
            bulletController.ChangeType(BulletType.Fire);
            Debug.Log($"[BULLET] Hit object '{tileGameObject.name}' on layer '{LayerMask.LayerToName(tileLayer)}'"); // doesn't work - it's unit tile
        }
        else if (tileLayer == LayerMask.NameToLayer("WaterTile"))
        {
            bulletController.ChangeType(BulletType.Water);
            Debug.Log($"[BULLET] Hit object '{tileGameObject.name}' on layer '{LayerMask.LayerToName(tileLayer)}'"); // doesn't work - it's unit tile
        }
        else if (tileLayer == LayerMask.NameToLayer("MudTile"))
        {
            bulletController.ChangeType(BulletType.Mud);
            // ja prepoznava
        }
        else if (tileLayer == LayerMask.NameToLayer("IceTile"))
        {
            bulletController.ChangeType(BulletType.Ice);
            Debug.Log($"[BULLET] Hit object '{tileGameObject.name}' on layer '{LayerMask.LayerToName(tileLayer)}'"); // doesn't work - it's unit tile

        }
        else
        {
            bulletController.Deactivate();
            // ja prepoznava
        }

        var unitTile = tileGameObject.GetComponent<UnitTile>();
        if (unitTile != null)
        {
            unitTile.Hit(bulletController);
        }
     }
}
