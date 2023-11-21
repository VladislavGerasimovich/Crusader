using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _delay;
    [SerializeField] private EnemyTypeFour _enemyTypeFour;

    private float _timeToChangeDirection;
    private int _number;

    public void Move()
    {
        _timeToChangeDirection += Time.deltaTime;
        transform.Translate(0, _speed, 0);

        if (_timeToChangeDirection >= _delay)
        { 
            _speed = -_speed;
            _timeToChangeDirection = 0;
        }
    }
}
