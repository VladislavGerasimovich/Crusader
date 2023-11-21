using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float[] _intermediatePositions;
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _shootPoints;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private List<GameObject> _bombs; 

    private Vector3 _targetPosition;
    private int _tick = 0;
    private int _maxCountOfTick = 3;
    private float _fireRate = 0.4f;
    private float _nextFire = 0.0f;

    private void Start()
    {
        StartCoroutine(MainCoroutine());
    }

    private IEnumerator Move()
    {
        while(transform.position != _endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(Move());

        while (true)
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(MoveBetweenPoints());
            yield return StartCoroutine(BombShooting());
            yield return null;
        }
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (_tick != _maxCountOfTick)
        {
            _targetPosition = new Vector3() { x = _intermediatePositions[_tick], y = transform.position.y, z = transform.position.z };
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            LaserGunShooting();

            if (transform.position == _targetPosition)
            {
                _tick++;
            }

            yield return null;
        }

        _tick = 0;
    }

    private IEnumerator BombShooting()
    {
        for (int i = 0; i < _shootPoints.Length; i++)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(_bombs[i], transform.position, Quaternion.identity);
        }
    }

    private void LaserGunShooting()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            for (int i = 0; i < _shootPoints.Length; i++)
            {
                Instantiate(_bullet, _shootPoints[i].position, Quaternion.identity);
            }
        }
    }
}
