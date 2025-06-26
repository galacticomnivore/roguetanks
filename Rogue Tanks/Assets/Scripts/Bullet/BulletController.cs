using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Bullet bullet;
    public Tank Tank { get; private set; }
    private int bulletStrength = 1;
    private GameFactory gameFactory;
    private SpriteRenderer spriteRenderer;
    public BulletType Type { get; private set; }


    public BulletController InitializeBullet(Tank tank, float bulletSpeed, GameFactory gameFactory)
    {
        this.gameFactory = gameFactory;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Tank = tank;
        bullet = new Bullet(GetComponent<BulletMovement>(), GetComponentInChildren<BulletSprite>().Initialize(this));
        bullet.Speed = bulletSpeed;
        Type = BulletType.Standard;
        gameObject.SetActive(false);
        return this;
    }
    public Vector3 Direction { get => bullet.Direction; }
    public int Strength { get => bulletStrength; }
    public void IncreaseBulletStrength() => bulletStrength++;
    public bool BulletCanDestroyTile(int tileStrength) => bulletStrength >= tileStrength;
    public void FaceUp() => bullet.FaceUp();
    public void FaceDown() => bullet.FaceDown();
    public void FaceLeft() => bullet.FaceLeft();
    public void FaceRight() => bullet.FaceRight();
    public void SetActiveAt(Vector3 position) => gameObject.SetActiveAt(position);
    public void Deactivate() => gameObject.SetActive(false);
 
   public void ChangeType(BulletType newType)
   {
        Type = newType;
        this.Type = newType;

        if (spriteRenderer == null || gameFactory == null)
            return;

        switch (newType)
        {
            case BulletType.Fire:
                spriteRenderer.sprite = gameFactory.FireBulletSprite;
                break;
            case BulletType.Water:
                spriteRenderer.sprite = gameFactory.WaterBulletSprite;
                break;
            case BulletType.Ice:
                spriteRenderer.sprite = gameFactory.IceBulletSprite;
                break;
            case BulletType.Mud:
                spriteRenderer.sprite = gameFactory.MudBulletSprite;
                break;
            case BulletType.Standard:
                spriteRenderer.sprite = gameFactory.StandardBulletSprite;
                break;
            default:
                Debug.LogWarning($"Unhandled bullet type: {Type}");
                break;
        }
   }
}


