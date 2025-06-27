using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LavaAction", menuName = "Rogue Tanks/Lava Action")]
public class LavaTileAction : EffectTileAction
{
    public override void Execute()
    {
        tankStats.Tank.BulletHit(null);
    }
}
