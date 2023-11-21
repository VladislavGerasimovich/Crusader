using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeOne : Enemy
{
    [SerializeField] private float _endPointY;
    [SerializeField] private float _endPointX;
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;

    private Vector3 _mainPosition;
    private Vector3 _intermediatePosition;
    private float _fireRate = 2f;
    private float _nextFire = 0.0f;

    private void Start()
    {
        _mainPosition = new Vector3() { x = transform.position.x, y = transform.position.y - _endPointY, z = transform.position.z };
        StartCoroutine(MainCoroutine());
    }

    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(Move());

        while (true)
        {
            yield return StartCoroutine(MoveBetweenPoints());
        }
    }

    private IEnumerator Move()
    {
        while (transform.position != _mainPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _mainPosition, _speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(Move());
    }

    private IEnumerator MoveBetweenPoints()
    {
        ChangeDirection();

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _intermediatePosition, _speed * Time.deltaTime);
            Shoot();

            if (transform.position == _intermediatePosition)
            {
                _endPointX = -_endPointX;
                ChangeDirection();
            }

            yield return null;
        }
    }

    private void ChangeDirection()
    {
        _intermediatePosition = new Vector3() { x = transform.position.x + _endPointX, y = transform.position.y, z = transform.position.z };
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        }
    }
}