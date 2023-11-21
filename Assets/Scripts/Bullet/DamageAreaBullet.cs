using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaBullet : Bullet
{
    [SerializeField] private float _lifeTime;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        
        if(_lifeTime <= 0)
        {
            Die();
        }
    }
}
