using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SerializableEffectTile
{
    public string Tile;
    public List<StatEffect> Effects;
    public bool IsDurable;
    public float Duration;
    public EffectTileAction EffectTileAction;
}

[Serializable]
public class EffectTile
{
    public List<StatEffect> Effects;
    public EffectTileAction EffectTileAction;

    private int tileCount = 0;
    private int previousTileCount = 0;

    public int TileCount
    {
        get => tileCount;
        set
        {
            previousTileCount = tileCount;
            tileCount = Math.Max(0, value);
            UpdateCount();
        }
    }

    public float StartTime => startTime;
    public float Duration => duration;

    private bool isDurable => startTime > 0;
    private float startTime = float.MinValue;
    private float duration = 0f;

    public UnityEvent EffectStart = new UnityEvent();
    public UnityEvent EffectEnd = new UnityEvent();

    public EffectTile()
    {
        Effects = new List<StatEffect>();
    }

    public EffectTile(List<StatEffect> effects, float _duration = 0f, EffectTileAction effectTileAction = null)
    {
        Effects = new List<StatEffect>(effects);
        duration = _duration;
        EffectTileAction = effectTileAction;
    }

    public void UpdateCount()
    {
        if(previousTileCount == 0 && tileCount > 0)
        {
            startTime = Time.time;
            EffectStart?.Invoke();
        }
        else if(previousTileCount > 0 && tileCount == 0)
        {
            startTime = float.MinValue;
            EffectEnd?.Invoke();
        }
        Debug.Log($"[Effect Tile] {previousTileCount} -> {tileCount}, {startTime}");
    }

    public bool CanBeAdded() => tileCount > 0 && previousTileCount == 0;
    public bool CanBeRemoved() => tileCount == 0 && previousTileCount > 0;

    public bool IsCompleted(float newTime) => isDurable && newTime - startTime > duration;

    public void Complete()
    {
        if(EffectTileAction != null)
        {
            EffectTileAction.Execute();
        }
        startTime = float.MinValue;
    }

    public void Increment() => TileCount++;
    public void Decrement() => TileCount--;

    public void Reset()
    {
        Debug.Log($"[Effect Tile] Resetting now... {TileCount}, {startTime}");
        TileCount = 0;
        startTime = float.MinValue;
    }
}