using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _explosion;

    private GameObject _marker;
    private Transform _path;
    private Transform[] _points;
    private int _currentPoint;
    protected string PathName;

    private void Start()
    {
        _path = GameObject.Find(PathName).transform;

        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        _marker = Instantiate(_target, _points[_path.childCount - 1].position, Quaternion.identity);
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if(transform.position == target.position)
        {
            _currentPoint++;
        }

        if(_currentPoint >= _points.Length)
        {
            Destroy(_marker);
            Instantiate(_explosion, _points[_path.childCount - 1].position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
