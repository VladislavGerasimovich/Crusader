using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] protected int Damage;
    [SerializeField] protected Vector3 Direction;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Border border))
        {
            Die();
        }
        else if (collision.TryGetComponent(out Player player))
        {
            player.ApplyDamage(Damage);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Direction * _speed * Time.deltaTime, Space.World);
    }
}
