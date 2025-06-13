using UnityEngine;

public class BulletWithTileCollisionHandler : ICollisionHandler
{
    private BulletController bulletController;

    // ✅ This is the missing constructor
    public BulletWithTileCollisionHandler(BulletController bulletController)
    {
        this.bulletController = bulletController;
    }
    public string CollisionTag => "Tile";


    public void Execute(Collider2D collision)
    {
        var unitTile = collision.GetComponent<UnitTile>();
        if (unitTile != null)
        {
            unitTile.Hit(bulletController);

            var singleTile = collision.GetComponent<SingleTile>();
            if (singleTile != null)
            {
                switch (singleTile.ElementType)
                {
                    case TileElementType.Lava:
                        bulletController.SetBulletType(BulletTypes.Fire);
                        break;
                    case TileElementType.Ice:
                        bulletController.SetBulletType(BulletTypes.Ice);
                        break;
                    case TileElementType.Mud:
                        bulletController.SetBulletType(BulletTypes.Mud);
                        break;
                }
            }
        }

        bulletController.Deactivate();
    }
}