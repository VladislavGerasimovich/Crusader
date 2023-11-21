using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeThree : Enemy
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _speed = 2;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _timeForSuicide = 5;

    private void Start()
    {
        _endPosition = new Vector3() { x = Random.Range(-1, 1), y = Random.Range(-0.5f, -2), z =0 };
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPosition, _speed * Time.deltaTime);

        if(transform.position == _endPosition)
        {
            _timeForSuicide -= Time.deltaTime;

            if(_timeForSuicide <= 0)
            {
                Instantiate(_bullet, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
