using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletEffectHandler
{
    void ApplyEffect(Collider2D collision, BulletController bullet);
}

