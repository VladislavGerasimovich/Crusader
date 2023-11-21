using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _speed;

    private float _intermediatePointX;
    private float _intermediatePointY;
    private int _points = 3;
    private float _firstPoint;
    private float _direction = 1;
    private int _currentPoint = 0;
    private Vector3[] _targetPositions;
    private Coroutine _move;

    private void Start()
    {
        _targetPositions = new Vector3[_points];
        _intermediatePointY = (Mathf.Abs(transform.position.y) + Mathf.Abs(_endPosition.y)) / _points;
        _intermediatePointX = (Mathf.Abs(transform.position.x) + Mathf.Abs(_endPosition.x)) / _points;

        for (int i = 0; i < _points; i++)
        {
            _targetPositions[i] = new Vector3() { x = transform.position.x + _direction, y = transform.position.y - _intermediatePointY * (i+1), z = transform.position.z };
            _direction = -_direction * 2;

            if(i == _points - 1)
            {
                _targetPositions[i] = new Vector3() { x = _endPosition.x, y = _endPosition.y, z = _endPosition.z };
            }

            Debug.Log(_targetPositions[i]);
        }
    }

    private void Update()
    {
        if(transform.position != _targetPositions[_currentPoint])
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPositions[_currentPoint], _speed * Time.deltaTime);
        }
        else if(transform.position == _targetPositions[_currentPoint])
        {
            _currentPoint++;
        }

        if (_currentPoint == _points)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Move(Vector3 position)
    {
        if (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(_move);
    }
}
