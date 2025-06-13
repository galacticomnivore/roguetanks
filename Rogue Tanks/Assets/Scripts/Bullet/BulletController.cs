using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Bullet bullet;
    public Tank Tank { get; private set; }

    [SerializeField] private BulletData bulletData;
    public int Strength => bullet.Strength;

    public void SetBulletType(BulletTypes type)
    {
        bullet.BulletType = type;
    }
    public BulletController InitializeBullet(Tank tank, BulletData data)
    {
        Tank = tank;
        bullet = new Bullet(GetComponent<BulletMovement>(), GetComponentInChildren<BulletSprite>().Initialize(this), data.bulletType, data.strength);
        bullet.Speed = data.speed;
        gameObject.SetActive(false);
        return this;
    }

    public Vector3 Direction { get => bullet.Direction; }
    public void IncreaseBulletStrength() => bulletData.strength++;
    public bool BulletCanDestroyTile(int tileStrength) => bulletData.strength >= tileStrength;
    public void FaceUp() => bullet.FaceUp();
    public void FaceDown() => bullet.FaceDown();
    public void FaceLeft() => bullet.FaceLeft();
    public void FaceRight() => bullet.FaceRight();
    public void SetActiveAt(Vector3 position) => gameObject.SetActiveAt(position);
    public void Deactivate() => gameObject.SetActive(false);

    //Кога ќе којадне куршумот со некој елемент, соодветно промени го типот
}
