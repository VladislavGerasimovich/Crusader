using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTypeFour: Enemy
{
    [SerializeField] private int _minRange;
    [SerializeField] private int _maxRange;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;

    private int _endPosition;
    private AimMovement _aim;
    private float _fireRate = 0.2f;
    private float _nextFire = 0.0f;
    private Vector2 _attackDirection;
    private int _direction = 1;
    private float _timeToAttack = 5;

    private void OnEnable()
    {
        MountEndPosition();
    }

    private void Start()
    {
        _aim = gameObject.GetComponentInChildren<AimMovement>();
        StartCoroutine(MainCoroutine());
    }

    private IEnumerator MainCoroutine()
    {
        while(true)
        {
            yield return StartCoroutine(Move());
            yield return StartCoroutine(Attack());
            MountEndPosition();
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        while (transform.position.y != _endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _endPosition, transform.position.z), _speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        while (_timeToAttack > 0)
        {
            _timeToAttack -= Time.deltaTime;
            _aim.Move();
            Shoot();
            yield return null;
        }
    }

    private void MountEndPosition()
    {
        _endPosition = UnityEngine.Random.Range(_minRange, _maxRange) * _direction;
        _timeToAttack = 5;

        if (_endPosition > 0)
        {
            _direction = -_direction;
        }
        else if (_endPosition < 0)
        {
            _direction = -_direction;
        }
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            _attackDirection = (_aim.transform.position - _shootPoint.position).normalized;
            _bullet.GetComponent<BulletTypeTwo>().MountDirection(_attackDirection);
            Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        }
    }
}
