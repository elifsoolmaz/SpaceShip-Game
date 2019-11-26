using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float damage = 10f;

   public void DestroyWhenHit()
    {
        Destroy(gameObject);
    }

    public float Harm()
    {
        return damage;
    }
}
