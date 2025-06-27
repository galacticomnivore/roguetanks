using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class TankStats : MonoBehaviour
{
    public Tank Tank { get; private set; }
    private Dictionary<int, int> tankPoints = new Dictionary<int, int>();
    public Dictionary<int, int> TankPoints { get => tankPoints; }
    public int TankType { get; private set; }
    public int Points { get; private set; }
    public int Lives { get; private set; } = 1;

    public List<SerializableEffectTile> PossibleEffectTiles = new List<SerializableEffectTile>();
    public List<StatEffect> StatEffects = new List<StatEffect>();

    [ShowInInspector]
    private Dictionary<string, EffectTile> effectTiles = new Dictionary<string, EffectTile>();

    public void Initialize(int tankType, int lives)
    {
        TankType = tankType;
        Lives = lives;
        Tank = GetComponentInParent<Tank>();
        effectTiles = new Dictionary<string, EffectTile>();
        PossibleEffectTiles.ForEach(x => {
            EffectTileAction action = x.EffectTileAction != null ? x.EffectTileAction.Clone() : null;
            if(action != null) action.Init(this);
            effectTiles.Add(x.Tile, new EffectTile(x.Effects, x.Duration, action));
        });
    }

    void Update()
    {
        foreach(var tile in effectTiles.Keys)
        {
            if(effectTiles[tile].IsCompleted(Time.time))
            {
                effectTiles[tile].Complete();
            }
        }
    }

    public void HitTank()
    {
        Tank.BulletHit(null);
    }

    public void AddLive(int numberOfLives) => Lives += numberOfLives;

    public void AddLive(int numberOfLives, Action<int> onLiveAdd)
    {
        AddLive(numberOfLives);
        onLiveAdd(Lives);
    }

    public TankStats DecreaseLife()
    {
        Lives -= 1;
        ResetEffects();
        return this;
    }

    public TankStats DecreaseLife(Action onDecreaseLife)
    {
        Lives -= 1;
        onDecreaseLife();
        ResetEffects();
        return this;
    }

    public TankStats DecreaseLife(Action<int> onLiveDecreased)
    {
        Lives -= 1;
        onLiveDecreased(Lives);
        ResetEffects();
        return this;
    }

    public TankStats TankStillHaveLivesLeft(Action onLivesLeft)
    {
        Lives.IsGreaterThanOrEqualTo(1, onLivesLeft);
        return this;
    }

    public TankStats TankHasNoMoreLivesLeft(Action onNoMoreLivesLeft)
    {
        Lives.Equals(0, onNoMoreLivesLeft);
        return this;
    }

    public void AddPoint(int destroyedTankType) => tankPoints.AddValue(destroyedTankType, 1);

    public void AddStatEffect(StatEffect statEffect)
    {
        if(!StatEffects.Exists(x => x.Tag == statEffect.Tag))
        {
            StatEffects.Add(statEffect);
        }
    }

    public void RemoveStatEffect(string tag)
    {
        StatEffects.RemoveAll(x => x.Tag == tag);
    }

    public List<StatEffect> GetStatEffects(StatType type)
    {
        return StatEffects.Where(x => x.Type == type).ToList();
    }

    private void UpdateTileEffects()
    {
        foreach(var tile in effectTiles.Values)
        {
            if(tile.CanBeAdded())
            {
                tile.Effects.ForEach(x => AddStatEffect(x));
            }
            else if(tile.CanBeRemoved())
            {
                tile.Effects.ForEach(x => RemoveStatEffect(x.Tag));
            }
        }
    }

    private void ResetEffects()
    {
        foreach(var tile in effectTiles.Values)
        {
            tile.Reset();
        }
    }

    public void AddTileCollision(string tile)
    {
        if(effectTiles.ContainsKey(tile))
        {
            effectTiles[tile].Increment();
        }
        UpdateTileEffects();
    }

    public void RemoveTileCollision(string tile)
    {
        if(effectTiles.ContainsKey(tile))
        {
            effectTiles[tile].Decrement();
        }
        UpdateTileEffects();
    }
}