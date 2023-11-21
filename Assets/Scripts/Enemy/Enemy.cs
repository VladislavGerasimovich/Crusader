using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _collisionDamage;
    [SerializeField] private int _health;

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_collisionDamage);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
