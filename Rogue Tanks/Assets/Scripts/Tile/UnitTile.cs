using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitTile : MonoBehaviour
{
    public List<StatEffect> StatEffects;
    public int Row { get; private set; }
    public int Column { get; private set; }
    public event Action<BulletController, UnitTile> OnHit;
    private void Start()
    {
        Row = name.Split('_')[1].ToInt();
        Column = name.Split('_')[2].ToInt();
    }

    public void Initialize(Sprite sprite, string collisionLayer)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = sprite;
        gameObject.layer = LayerMask.NameToLayer(collisionLayer);
    }
    public void Hit(BulletController bulletController) => OnHit?.Invoke(bulletController,this);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TankMovementController tank = collision.GetComponentInParent<TankMovementController>();
        GameEngine engine = GameObject.FindAnyObjectByType<GameEngine>();
        if (engine == null) return;
        if (tank == null) return;

        engine.EnvironmentEffectsHandler.HandleEnvirontmentEffect(gameObject, tank);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        TankMovementController tank = collision.GetComponentInParent<TankMovementController>();
        GameEngine engine = GameObject.FindAnyObjectByType<GameEngine>();
        if (engine == null) return;
        if (tank == null) return;

        engine.EnvironmentEffectsHandler.HandleEnvirontmentEffect(gameObject, tank);
    }
}
