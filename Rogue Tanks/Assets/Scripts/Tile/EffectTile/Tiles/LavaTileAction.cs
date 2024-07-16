using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LavaAction", menuName = "Rogue Tanks/Lava Action")]
public class LavaTileAction : EffectTileAction
{
    public override void Execute()
    {
        if(tankStats.Tank.IsPlayerTank())
        {
            tankStats.Tank.BulletHit(null);
        }
        else
        {
            // TO-DO: Implement logic to destroy enemy tank
        }
    }
}
