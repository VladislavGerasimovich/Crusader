using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;

    private int _health = 5;
    private Vector3 _startPosition = new Vector3(-0, -4, 0);
    private int _standardValueHealth = 5;
    private float _fireRate = 0.3f;
    private float _nextFire = 0.0f;

    public event UnityAction GameOver;
    public event UnityAction<int> HealthChanged;

    public void Reset()
    {
        transform.position = _startPosition;
        _health = _standardValueHealth;
        HealthChanged?.Invoke(_health);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
        {
            _health = 0;
        }

        HealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
        HealthChanged?.Invoke(_health);
    }

    private void Update()
    {
        if(Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
    }

    private void Die()
    {
        GameOver?.Invoke();
    }
}
