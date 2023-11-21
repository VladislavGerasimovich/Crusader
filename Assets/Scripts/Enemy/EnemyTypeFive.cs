using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeFive : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;

    private Transform _path;
    private Transform[] _points;
    private int _currentPoint;
    private Vector2 _direction;
    private Vector2 _shotDirection;
    private Transform _aim;

    public void Shoot()
    {
        _shotDirection = (_aim.transform.position - _shootPoint.position).normalized;
        _bullet.GetComponent<BulletTypeTwo>().MountDirection(_shotDirection);
        Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
    }

    private void Start()
    {
        _aim = transform.Find("Aim");
        _path = GameObject.Find("PathOne").transform;
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        _direction = (target.position - transform.position).normalized;

        if(_direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _turningSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if(transform.position == target.position)
        {
            _currentPoint++;

            if(_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}
