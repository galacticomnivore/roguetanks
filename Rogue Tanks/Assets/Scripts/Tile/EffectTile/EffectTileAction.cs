using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTileAction : ScriptableObject
{
    protected TankStats tankStats;

    public virtual void Init(TankStats _tankStats)
    {
        tankStats = _tankStats;
    }

    public virtual void Execute() { }
}
