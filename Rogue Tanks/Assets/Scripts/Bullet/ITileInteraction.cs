using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITileInteraction : MonoBehaviour
{
    BulletController bulletController;
    public void Interact(BulletController bullet)
    {
        //DestructibleTile would check bullet strength and destroy itself.

       // LavaTile would call a method on the bullet to transform it into a fire bullet.
    }
}
