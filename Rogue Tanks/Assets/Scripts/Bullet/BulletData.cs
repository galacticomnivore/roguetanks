using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObjects/BulletData", fileName = "BulletType")]
public class BulletData : ScriptableObject
{
    public BulletTypes bulletType;
    public float speed;
    public int strength;
    public BulletSprite sprite;
}
